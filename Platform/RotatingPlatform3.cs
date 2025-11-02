using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 50f; // Speed of rotation
    [SerializeField] private float angleA = 0f; // Default starting angle
    [SerializeField] private float angleB = 90f; // Target angle when not interacting

    [SerializeField] private Transform lever;
    [SerializeField] private float interactionDistance = 2f; // Distance within which player can activate the lever

    private float targetAngle; // Current target rotation
    private bool continueRotating = false;
    private float rotationTimer = 0;
    private float rotationSpeed2 = 50f; // Speed of rotation


    void Start()
    {
        targetAngle = angleB; // Start by rotating towards B
    }

    void Update()
    {
        // Smoothly rotate towards the target angle
        float currentRotationSpeed = (targetAngle == angleA) ? rotationSpeed2 : rotationSpeed;
        float step = currentRotationSpeed * Time.deltaTime;
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, targetAngle), step);

        // Check if player is near the lever and presses interaction keys
        if (IsPlayerNearLever() && (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)))
        {
            continueRotating = false;
            rotationTimer = 60;
        }

        if (IsPlayerNearLever() && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)))
        {
            continueRotating = true;
            if (rotationTimer < 16)
                rotationTimer++;
        }
        else
        {
            if (rotationTimer >= -1)
                rotationTimer--;
            if (rotationTimer == -2)
            {
                targetAngle = angleB; // Rotate back to angle B when no interaction
            }
        }

        if (rotationTimer > 15 && continueRotating)
        {
            targetAngle = angleA;
        }
    }

    private bool IsPlayerNearLever()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        if (player != null)
        {
            float distance = Vector3.Distance(player.transform.position, lever.position);
            return distance <= interactionDistance;
        }
        return false;
    }
}
