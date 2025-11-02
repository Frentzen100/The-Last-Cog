using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    public Notification notificationText; // Assign this in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            if (notificationText != null)
            {
                notificationText.ShowMessage("You found an item. Press 'E' to learn more!");
            }

           Destroy(other.gameObject); // Destroy the item after collision
        }
    }
}
