using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    private bool facingRight = true;
    [SerializeField] private Vector2 respawnPoint = new Vector2(0, 0);
    [SerializeField] private float fallThreshold = -120f;

    public Transform groundCheckPos;
    private Vector2 groundCheckSize = new Vector2(1.8f, 0.3f);
    public LayerMask groundLayer;
    public bool isJumping;

    private bool jumpRequest = false;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float jumpMultiplier = 1.5f;
    private float jumpCounter;
    [SerializeField] private float jumpTime;

    private FadeInOut fade; // Reference to FadeInOut script
    Animator animator;
    private AudioManager audioManager;
    bool isPlaying = false;
    bool hasJumped = false;
    bool hasFallen = false;
    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        body.freezeRotation = true;
        fade = FindObjectOfType<FadeInOut>(); // Find FadeInOut in the scene
    }

    void Start() {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * moveInput * speed * Time.deltaTime);

        if (Math.Abs(moveInput) > 0) {
            
            animator.SetFloat("XVelocity", Math.Abs(moveInput));
            if (!isPlaying) {
                audioManager.PlaySFXLoop(audioManager.walking);
                isPlaying = true;
            }
        }
        else {
            isPlaying = false;
            audioManager.StopSFX(audioManager.walking);
        }

        // Flip character direction
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        // Check if player is grounded
        isJumping = !isGrounded();

        if (isJumping) {
            if (!hasJumped) {
                audioManager.PlaySFX(audioManager.jumping);
                hasJumped = true;
            }   
        }
        
        else {
            hasJumped = false;
        }

        // Detect jump input
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            jumpRequest = true;
            jumpCounter = 0;
        }

        // **Trigger fade effect when player falls**
        if (transform.position.y < fallThreshold)
        {
            StartCoroutine(FallAndRespawn());
        }

        // Apply faster falling when descending
        if (body.linearVelocity.y < 0)
        {
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Apply extra force for faster upward movement during initial jump
        if (body.linearVelocity.y > 0 && jumpRequest)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) jumpRequest = false;
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

   private IEnumerator FallAndRespawn()
    {
        if (!hasFallen) {
            audioManager.PlaySFX(audioManager.falling);
            hasFallen = true;
        }
        Debug.Log("⚡ Player falling! Starting FadeIn...");

        if (fade != null)
        {
            fade.StartFadeSequence(0.3f); // Fades in, waits 2 seconds, then fades out
            yield return new WaitForSeconds(fade.fadeDuration + 0.3f); // Wait for fade sequence to complete
        }

        Respawn();
    }

    private void Respawn()
    {
        hasFallen = false;
        transform.position = respawnPoint;
        body.linearVelocity = Vector2.zero; // Reset velocity to prevent weird movement after respawn
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheckPos.position, groundCheckSize, CapsuleDirection2D.Horizontal, 0f, groundLayer);
    }
}






/*
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 8f;
    private bool facingRight = true;
    [SerializeField] private Vector2 respawnPoint = new Vector2(0, 0);
    [SerializeField] private float fallThreshold = -5f;

    public Transform groundCheckPos;
    private Vector2 groundCheckSize = new Vector2(1.8f, 0.3f);
    public LayerMask groundLayer;
    public bool isJumping;

    private bool jumpRequest = false;

    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float jumpMultiplier = 1.5f;
    private float jumpCounter;
    [SerializeField] private float jumpTime;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        body.freezeRotation = true;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * moveInput * speed * Time.deltaTime);

        // Flip character direction
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();

        // Check if player is grounded
        isJumping = !isGrounded();

        // Detect jump input
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
            jumpRequest = true;
            jumpCounter = 0;
        }

        // Respawn if player falls below threshold
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }

        // Apply faster falling when descending
        if (body.linearVelocity.y < 0)
        {
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        // Apply extra force for faster upward movement during initial jump
        if (body.linearVelocity.y > 0 && jumpRequest)
        {
            jumpCounter += Time.deltaTime;
            if (jumpCounter > jumpTime) jumpRequest = false;
            body.linearVelocity += Vector2.up * Physics2D.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheckPos.position, groundCheckSize);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheckPos.position, groundCheckSize, CapsuleDirection2D.Horizontal, 0f, groundLayer);
    }
}

*/