using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu; 
    private bool menuActivated; 
    public ItemSlot[] itemSlot;
    public bool messageShown;
    public int messageDuration;

    [SerializeField]
    private TMP_Text notificationText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(notificationText);
    }

    // Update is called once per frame
    void Update()
    {
        if (messageShown || (messageDuration != 180 && messageDuration > -1)) {
            messageDuration--;
            messageShown = false;
            if (messageDuration <= 0) {
//                notificationText.text = "";
            }
        }

        if (Input.GetButtonDown("Inventory") && menuActivated) {
            InventoryMenu.SetActive(false);
            menuActivated = false;
        }

        else if (Input.GetButtonDown("Inventory") && !menuActivated) {
            InventoryMenu.SetActive(true);
            menuActivated = true;
        }
    }

    public void AddItem(string itemName, int amount, Sprite itemSprite, string itemDescription) {
       for (int i = 0; i < itemSlot.Length; i++)
       {
            if(itemSlot[i].isFull == false) {
                itemSlot[i].addItem(itemName, amount, itemSprite, itemDescription);
                return;
            }
       }
    }

    public void DeselectAllSlots() {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

    public void showMessage() {
        messageShown = true;
        messageDuration = 180;
        //notificationText.text = "You collected an item. Press E to view it";
    }
}
