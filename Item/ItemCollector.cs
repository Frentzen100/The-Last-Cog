using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ItemCollector : MonoBehaviour
{
    public static ItemCollector instance; // Singleton reference

    public int collectedItems = 0;  // Tracks collected items
    public int totalItems = 4;      // Total number of items to collect

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object when changing scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    private void Start()
    {
        StartCoroutine(WaitAndFindUI()); // Find UI after delay
    }

    private IEnumerator WaitAndFindUI()
    {
        yield return new WaitForSeconds(0.2f); // Small delay to allow UI to load

        Debug.Log("🔍 Searching for UI elements in the new scene...");

        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            Debug.Log("🆔 Found GameObject: " + obj.name + " (Tag: " + obj.tag + ")");
        }

    }

    void UpdateItemDisplay()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            collectedItems++;
            Debug.Log("✅ Item Collected! Total: " + collectedItems);
            UpdateItemDisplay();
            Destroy(other.gameObject);
        }
        else
        {
            Debug.LogWarning("⚠️ Object collided but is NOT an item: " + other.gameObject.name);
        }
    }

    public static void ResetProgress()
    {
        if (instance != null)
        {
            instance.collectedItems = 0;
            Debug.Log("🔄 Game Restarted: Progress Reset");
        }
    }
}
