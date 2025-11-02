using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Notification : MonoBehaviour
{
     private TMP_Text notificationText;
    public float displayDuration = 3f; // How long the message stays visible

    private void Start()
    {
        notificationText = GetComponent<TMP_Text>();
        notificationText.enabled = false; 
    }

    public void ShowMessage(string message)
    {
        StopAllCoroutines(); // Stop any ongoing fade-out
        notificationText.text = message;
        notificationText.enabled = true;
        StartCoroutine(HideMessageAfterDelay());
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        notificationText.enabled = false;
    }
}
