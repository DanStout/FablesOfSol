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

        if (Items == null)
            Items = new Dictionary<string, BaseItem>();
        else
        {
            foreach (var item in Items)
            {
                AddButtonForItem(item.Value);
            }
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
            savedItem.onQuantityChanged += savedItem_onQuantityChanged;
            savedItem.Quantity = 1;
            AddButtonForItem(savedItem);

            Items[savedItem.Name] = savedItem;

            if (activeItem == null)
                EquipIfWeapon(savedItem);
        }

    }

    void savedItem_onQuantityChanged(BaseItem item)
    {
        if (item.Quantity <= 0)
        {
            Items.Remove(item.Name);
            Destroy(item);
        }
    }

    public void ItemButtonClicked(BaseItem item)
    {
        if (!EquipIfWeapon(item))
            item.Use();
    }


    /// <summary>
    /// Add a button for a BaseItem. The BaseItem MUST already be persisted.
    /// </summary>
    /// <param name="item"></param>
    private void AddButtonForItem(BaseItem item)
    {
        var buttonObj = Instantiate<GameObject>(buttonPrefab);
        buttonObj.transform.SetParent(buttonGrid.transform);
        var invBtn = buttonObj.GetComponent<InventoryButton>();
        invBtn.AssignItem(item);
    }

    /// <summary>
    /// Equips an item if it is a weapon
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

}
