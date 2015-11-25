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
        if (Input.GetButton("Attack"))
        {
            anim.SetBool("attacking", true);
        }
        else
        {
            anim.SetBool("attacking", false);
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (Input.GetButton("Attack"))
        {
            var enemy = col.GetComponent<IEnemy>();
            if (enemy != null)
            {
                print("Hit an enemy!");
                enemy.TakeDamage(damage);
            }
        }
    }
}
