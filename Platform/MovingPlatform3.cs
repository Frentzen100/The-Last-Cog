using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required for UI

public class MovingPlatform3 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 4f;

    private Vector3 nextPosition;
    private Vector3 initialPosition;
    public Transform lever;
    public float interactionDistance = 1f;
    public float moveTimer = 0;
    public bool isMoving;
    public bool continueMoving;

    public Image progressFill; // Reference to the fill image

    void Start()
    {
        initialPosition = transform.position;
        nextPosition = pointB.position;
        Application.targetFrameRate = 60;

        if (progressFill != null)
        {
            progressFill.fillAmount = 1f; // Start at full
        }
        else
        {
            Debug.LogError("ProgressFill image is NOT assigned in the Inspector!");
        }
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);

        if ((IsPlayerNearLever() && Input.GetKey(KeyCode.W)) || (IsPlayerNearLever() && Input.GetKey(KeyCode.UpArrow)))
        {
            continueMoving = true;
            if (moveTimer < 6)
                moveTimer++;
        }
        else
        {
            if (moveTimer >= -1)
                moveTimer--;
            if (moveTimer == -2)
            {
                nextPosition = pointB.position;
            }
        }

        if (moveTimer > 5)
        {
            nextPosition = pointA.position;
        }

        if (transform.position == pointA.position && continueMoving == true)
        {
            moveTimer = 153;
            continueMoving = false;
        }

        // Update progress bar
        UpdateProgressBar();
    }

   void UpdateProgressBar()
{
    if (progressFill != null)
    {
        float totalDistance = Vector3.Distance(pointA.position, pointB.position);
        float currentDistance = Vector3.Distance(transform.position, pointA.position);

        // Reverse the logic: 0 when at Point A, 1 when at Point B
        float progress = currentDistance / totalDistance;
        progressFill.fillAmount = 1f - progress; // Invert the fill amount

    }
}


    bool IsPlayerNearLever()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, lever.position);
            return distance <= interactionDistance;
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.gameObject.transform.parent = null;
    }
}
