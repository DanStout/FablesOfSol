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

    private static Dictionary<string, BaseItem> Items { get; set; }

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
        if (Items == null)
            Items = new Dictionary<string, BaseItem>();

        foreach (var item in Items)
        {
            AddButtonForItem(item.Value);
        }
    }

    /// <summary>
    /// Called from a DroppedItem when the character collides with it.
    /// </summary>
    /// <param name="item">The script to pick up.</param>
    public void PickupItem(BaseItem item)
    {
        if (Items.ContainsKey(item.Name))
        {
            Items[item.Name].Quantity++;
        }
        else
        {
            var savedItem = GameManager.GetInstance().SaveItem(item);
            AddButtonForItem(savedItem);

            Items[savedItem.Name] = savedItem;
            savedItem.Quantity = 1;

            if (activeItem == null)
                EquipIfWeapon(savedItem);
        }

    }

    public void ItemButtonClicked(string itemName)
    {
        var storedItem = Items[itemName];
        if (!EquipIfWeapon(storedItem))
            storedItem.Use();
    }

    private void AddButtonForItem(BaseItem item)
    {
        var buttonObj = Instantiate<GameObject>(buttonPrefab);
        buttonObj.transform.SetParent(buttonGrid.transform);
        var invBtn = buttonObj.GetComponent<InventoryButton>();
        invBtn.SetSprite(item.inventoryTile);
        invBtn.ItemName = item.Name;
    }

    /// <summary>
    /// Equips a weapon if it is an item
    /// </summary>
    /// <param name="item">The item</param>
    /// <returns>Whether this item was equipped</returns>
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

	public WeaponType returnCurrent(){

		WeaponType w = WeaponType.Hammer;

		if (SonicResonator.activeInHierarchy == true) {
			w = WeaponType.SonicResonator;
		}else if(MagnetGun.activeInHierarchy == true){
			w = WeaponType.MagnetGun;
		}
		return w;

	}

}
