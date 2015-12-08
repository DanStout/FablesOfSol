using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInventory inventory;

    void Start()
    {
        inventory = GetComponentInParent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            inventory.UseItem();
        }
    }
}
