using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerInventory inventory;
    private bool attacking = false;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        inventory = GetComponentInParent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Attack"))// && !attacking)
        {
            if (inventory.UseItem())
            {
                anim.SetTrigger("attack");
                //attacking = true;
            }
        }
    }

    public void AttackAnimationDone()
    {
        attacking = false;
    }
}
