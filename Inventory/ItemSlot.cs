using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    // Item Data 
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite; 

    //Item Slot
    [SerializeField]
    private TMP_Text quantityText;

    [SerializeField]
    private Image itemImage;

    public GameObject selectedShader;
    public bool thisItemSelected;

    private InventoryManager inventoryManager;


    //Item Descriptions
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    private void Start() {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public void addItem (string itemName, int amount, Sprite itemSprite, string itemDescription) 
    {
        this.itemName = itemName;
        this.quantity = amount;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        isFull = true;

        quantityText.text = amount.ToString();
        quantityText.enabled = true;
        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData) 
    {
        if(eventData.button == PointerEventData.InputButton.Left) 
        {
            onLeftClick();
        }

        if(eventData.button == PointerEventData.InputButton.Right) 
        {
            onRightClick();
        }
    }

    public void onLeftClick() {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemSprite;
        if(itemDescriptionImage == null) {
            itemDescriptionImage.sprite = emptySprite;
        }

    }

    public void onRightClick() {

    }
}
