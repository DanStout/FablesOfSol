using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public float damage = 5;
    private PlayerInventory inventory;
	private bool attacking;


    void Start()
    {
        anim = GetComponentInParent<Animator>();
        inventory = GetComponentInParent<PlayerInventory>();
		attacking = false;
    }

    void Update()
    {
        //If the attack button is down, use the equipped item
        if (Input.GetButtonDown("Attack") && attacking == false)
        {
			attacking = true;
			print ("Player attack");
            inventory.useItem();
            anim.SetBool("attacking", true);
			attacking = false;
        }
        else anim.SetBool("attacking", false);
    }
}
