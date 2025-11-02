using UnityEngine;
using UnityEngine.UIElements;

public class LeverFlip : MonoBehaviour
{
    private AudioManager audioManager;
    private bool isNearPlayer = false; // Track if player is near
    private string leverToggleName = "isToggled";
    Animator animator;
    private bool isToggled; 
    private bool hasPlayed;


    private void Awake()
    {
        // Safely find AudioManager with null check
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
        animator = GetComponent<Animator>();


        if (audioManager == null)
        {
            Debug.LogWarning("AudioManager not found! Make sure an object with the 'Audio' tag exists.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = true; // Player is close enough to interact
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false; // Player moved away
        }
    }

    private void Update()
    {
        // If near the lever and "W" is pressed, flip the lever
        if ((isNearPlayer && Input.GetKey(KeyCode.W)) || (isNearPlayer &&  Input.GetKey(KeyCode.UpArrow)))
        {   
            isToggled = true;
            animator.SetBool(leverToggleName, true);

            if (!hasPlayed) {
                Debug.Log("Sound FX played!");
                audioManager.PlaySFX(audioManager.flipLever);
                hasPlayed = true;
            }
        }
        else {
            isToggled = false;
            hasPlayed = false;
            animator.SetBool(leverToggleName, false);
        }


        if (isToggled && !hasPlayed) {
            hasPlayed = true;
            Debug.Log("IS playing");
        }

        Debug.Log("HasPlayed:" + hasPlayed + " isToggled: " + isToggled);
    }

}
