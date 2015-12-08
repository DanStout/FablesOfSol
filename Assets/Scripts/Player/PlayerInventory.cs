using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    //Variables to store current items and active items
    //public List<IItem> inventory;
    //private IItem activeItem;
    public GameObject buttonPrefab;

    private BaseItem activeItem;
    private GameObject UI;
    private List<BaseItem> inventory;

    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("InventoryUI");
        inventory = new List<BaseItem>();
        Hammer hammer = gameObject.AddComponent<Hammer>();
        PickupItem(hammer);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        var item = hit.gameObject.GetComponent<BaseItem>();
        if (item != null)
        {
            if (item.IsDropped)
            {
                PickupItem(item);
                Destroy(hit.gameObject);
            }
        }
    }

    public void PickupItem(BaseItem item)
    {
        var button = Instantiate<GameObject>(buttonPrefab);
        var buttonScript = button.GetComponent<Button>();
        buttonScript.onClick.AddListener(() =>
        {
            activeItem = buttonScript.GetComponent<BaseItem>();
        });

        button.AddComponent(item.GetType());
        button.transform.SetParent(UI.transform);
        inventory.Add(item);

        if (activeItem == null)
        {
            activeItem = item;
        }
    }

    //Used by attack script to trigger the active item
    public void useItem()
    {
        if (activeItem != null)
            activeItem.Use();
        else
            print("Nothing equipped!");
    }
}
