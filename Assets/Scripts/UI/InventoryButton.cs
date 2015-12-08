using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    private BaseItem ItemScript { get; set; }

    [SerializeField]
    private Image spriteImage;

    [SerializeField]
    private Text quantityText;

    private PlayerInventory invent;

    void Start()
	{
        invent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}

    void OnDisable()
    {
        ItemScript.onQuantityChanged -= ItemScript_onQuantityChanged;
    }

    public void AssignItem(BaseItem item)
    {
        ItemScript = item;
        spriteImage.sprite = ItemScript.inventoryTile;
        UpdateQuantity(item.Quantity);
        ItemScript.onQuantityChanged += ItemScript_onQuantityChanged;
    }

    private void UpdateQuantity(int newQuantity)
    {
        quantityText.text = newQuantity.ToString();

        if (newQuantity <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void ItemScript_onQuantityChanged(BaseItem item)
    {
        var newQ = item.Quantity;
        UpdateQuantity(newQ);
    }

    public void OnClicked()
    {
        invent.ItemButtonClicked(ItemScript);
    }
}
