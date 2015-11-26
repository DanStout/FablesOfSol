using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public float damage = 5;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        //If the attack button is down, use the equipped item
        if (Input.GetButtonDown("Attack"))
        {
            GetComponentInParent<PlayerMovement>().useItem();
            anim.SetBool("attacking", true);

        }
        else anim.SetBool("attacking", false);
    }

    //    void OnTriggerEnter(Collider col)
    //    {
    //        if (Input.GetButton("Attack"))
    //        {
    //            var enemy = col.GetComponent<IEnemy>();
    //            if (enemy != null)
    //            {
    //                print("Hit an enemy!");
    //                enemy.TakeDamage(damage);
    //            }
    //        }
    //    }
}
