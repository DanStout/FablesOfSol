using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    //Variables to store current items and active items
    private IItem activeItem;
    public List<IItem> inventory;
	private GameObject UI;

    void Start()
    {
		//Find inventory UI
		UI = GameObject.FindGameObjectWithTag ("InventoryUI");

        //Initialize inventory with hammer
        inventory = new List<IItem>();
        Hammer hammer = this.gameObject.AddComponent<Hammer>();

        inventory.Add(hammer);
        activeItem = hammer;
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            if (activeItem != null)
                activeItem.Use();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //If we have collided with an item, add to inventory
        if (hit.gameObject.tag == "item")
        {
			//Add item script to inventory
			IItem newItem = (IItem)hit.gameObject.GetComponent(typeof(IItem));
			pickup(newItem);
			Destroy(hit.gameObject);

			//Create inventory UI element
			try
			{
				GameObject button = (GameObject)Instantiate(Resources.Load("ButtonPrefab"));
				Button b = (Button) button.GetComponent(typeof(Button));
				if(b != null)
				{
					b.onClick.AddListener(() => {
						print ("setActive");
						setActive((IItem)b.GetComponent(typeof(IItem)));
					});
				}

				Text text = (Text)button.GetComponentInChildren(typeof(Text));
				text.text = "";
		

				button.AddComponent(newItem.GetType());
				button.transform.SetParent(UI.transform);
			}
			catch(UnityException e)
			{
				print (e.GetBaseException());
			}



        }
    }

    //When we collide with an item, pick it up
    void pickup(IItem item)
    {

        //Update inventory UI

        //Add item to inventory
        inventory.Add(item);

        //If we have no item equiped, equip item
        if (activeItem == null)
        {
            print("equipped " + item.getName());
            activeItem = item;
        }
    }

	public void setActive(IItem item)
	{
		activeItem = item;
	}

    public void PickupItem(BaseItem item)
    {
        item.Use();
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
