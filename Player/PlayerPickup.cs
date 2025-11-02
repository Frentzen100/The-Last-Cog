using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool isNearPlayer = false; // Track if the player is near

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            isNearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    private void Update()
    {
        // If player is near and presses "W", destroy the item
        if (isNearPlayer && Input.GetKeyDown(KeyCode.W))
        {
        }
    }
}
