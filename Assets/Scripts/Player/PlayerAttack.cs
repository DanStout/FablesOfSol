using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    //private PlayerInventory inventory;
    public float damage = 5;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        //inventory = GetComponent<PlayerInventory>();
    }

    void Update()
    {
        //If the attack button is down, use the equipped item
        if (Input.GetButtonDown("Attack"))
        {
            anim.SetBool("attacking", true);
            //inventory.UseItem();

        }
        else anim.SetBool("attacking", false);
    }

    void OnTriggerEnter(Collider col)
    {
        if (Input.GetButton("Attack"))
        {
            var enemy = col.GetComponent<IEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
