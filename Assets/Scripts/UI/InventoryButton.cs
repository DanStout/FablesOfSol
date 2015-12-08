using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public string ItemName { get; set; }

    [SerializeField]
    private Image spriteImage;

    private PlayerInventory invent;

    void Start()
	{
        invent = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
	}

    public void SetSprite(Sprite sprite)
    {
        spriteImage.sprite = sprite;
    }

    public void OnClicked()
    {
        invent.ItemButtonClicked(ItemName);
    }
}
