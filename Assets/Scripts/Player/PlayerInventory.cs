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

    private BaseItem activeItem;
    private GameObject buttonGrid;
    private List<BaseItem> inventory;

    public enum WeaponType
    {
        Hammer,
        MagnetGun,
        SonicResonator
    }

    void Start()
    {
        buttonGrid = GameObject.FindGameObjectWithTag("InventoryUI");
        inventory = new List<BaseItem>();
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

    public void Equip(WeaponType weap)
    {
        Hammer.SetActive(weap == WeaponType.Hammer);
        MagnetGun.SetActive(weap == WeaponType.MagnetGun);
        SonicResonator.SetActive(weap == WeaponType.SonicResonator);
    }

}
