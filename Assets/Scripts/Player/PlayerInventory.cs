using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject Hammer;
    public GameObject MagnetGun;
    public GameObject SonicResonator;

    private BaseItem activeItem;
    private GameObject buttonGrid;
    private List<BaseItem> inventory;

    void Start()
    {
        buttonGrid = GameObject.FindGameObjectWithTag("InventoryUI");
        inventory = new List<BaseItem>();
<<<<<<< HEAD

=======
>>>>>>> 5c874fbcd44fe2e8a0e88c9c8658823f3cc96609
    }

    public void PickupItem(BaseItem item)
    {
        var buttonObj = Instantiate<GameObject>(buttonPrefab);
        var addedItem = buttonObj.AddComponent(item.GetType()) as BaseItem;
        buttonObj.transform.SetParent(buttonGrid.transform);

        var image = buttonObj.transform.FindChild("Image").GetComponent<Image>();
        image.sprite = item.inventoryTile;

        var buttonScript = buttonObj.GetComponent<Button>();
        buttonScript.onClick.AddListener(() =>
        {
            var clickedItem = buttonScript.GetComponent<BaseItem>();

            if (!EquipIfWeapon(clickedItem))
                clickedItem.Use();
        });

        inventory.Add(addedItem);
        EquipIfWeapon(addedItem);
    }

    private bool EquipIfWeapon(BaseItem item)
    {
        var pickedupWeapon = item as Weapon;
        if (pickedupWeapon != null)
        {
            pickedupWeapon.Equip();
            activeItem = pickedupWeapon;
            return true;
        }
        return false;
    }

    public bool UseItem()
    {
        if (activeItem != null)
        {
            activeItem.Use();
            return true;
        }
        else
        {
            print("Nothing equipped!");
            return false;
        }
    }
}
