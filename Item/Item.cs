using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int amount;

    [SerializeField]
    private Sprite sprite;
    
    [TextArea]
    [SerializeField]
    private string itemDescription;

    [SerializeField]
    private TextMeshProUGUI notificationText;

    private InventoryManager inventoryManager;

    private bool isNearPlayer = false; 

    private bool showMessage = false;
    private float messageTimer = 0;
    private AudioManager audioManager;

    void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        Application.targetFrameRate = 60; 
        audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            isNearPlayer = true;
            showMessage = true;
            Debug.Log("HELLO");
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
    {
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") 
        {
            inventoryManager.showMessage();
            inventoryManager.AddItem(itemName, amount, sprite, itemDescription);
            audioManager.PlaySFX(audioManager.collectItem);
            Destroy(gameObject);
        }
    }

}
