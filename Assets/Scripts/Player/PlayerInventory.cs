using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    private GameObject Hammer;

    [SerializeField]
    private GameObject MagnetGun;

    [SerializeField]
    private GameObject SonicResonator;

    private GameObject buttonGrid;
    private static BaseItem activeItem;
    private static List<BaseItem> inventory;

    public class ItemEntry
    {
        public BaseItem Item { get; set; }
        public int Quantity { get; set; }


        public override string ToString()
        {
            return "({0} x {1})".FormatWith(Quantity, Item);
        }
    }

    public Dictionary<string, ItemEntry> Items { get; set; }

    public enum WeaponType
    {
        Hammer,
        MagnetGun,
        SonicResonator
    }

    void Start()
    {
        buttonGrid = GameObject.FindGameObjectWithTag("InventoryUI");
        if (inventory == null)
            inventory = new List<BaseItem>();

        foreach (var item in inventory)
            print(item);
    }

    /// <summary>
    /// Called from a DroppedItem when the character collides with it.
    /// </summary>
    /// <param name="item">The script to pick up.</param>
    public void PickupItem(BaseItem item)
    {
        print(item);
        //if (Items.ContainsKey(item.Name))
        //{
        //    Items[item.Name].Quantity++;
        //}
        //else
        //{
        //    Items[item.Name] = new ItemEntry{Item = item.}
        //}
        
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

    public void Equip(WeaponType weap)
    {
        Hammer.SetActive(weap == WeaponType.Hammer);
        MagnetGun.SetActive(weap == WeaponType.MagnetGun);
        SonicResonator.SetActive(weap == WeaponType.SonicResonator);
    }

}
