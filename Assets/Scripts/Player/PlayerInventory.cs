using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    //Variables to store current items and active items
    private IItem activeItem;
    public List<IItem> inventory;
    
    void Start()
    {
        //Initialize inventory with hammer
        inventory = new List<IItem>();
        Hammer hammer = this.gameObject.AddComponent<Hammer>();

        inventory.Add(hammer);
        activeItem = hammer;

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //If we have collided with an item, add to inventory
        if (hit.gameObject.tag == "item")
        {
            IItem newItem = hit.gameObject.GetComponent<IItem>();
            Pickup(newItem);
            Destroy(hit.gameObject);
        }
    }

    //When we collide with an item, pick it up
    void Pickup(IItem item)
    {
        //Update inventory UI

        //Add item to inventory
        inventory.Add(item);

        //If we have no item equiped, equip item
        if (activeItem == null)
        {
            print("equipped " + item.Name);
            activeItem = item;
        }
    }

    //Used by attack script to trigger the active item
    public void UseItem()
    {
        if (activeItem != null)
            activeItem.Use();
        else
            print("Nothing equipped!");
    }
}
