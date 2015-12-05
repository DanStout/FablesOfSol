﻿using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour, IItem
{
    public float radius = 1;
    public float damage = 5;

    private GameObject owner;
    private bool attacking = false;

    // Use this for initialization
    void Start()
    {
        owner = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider col)
    {
        if (Input.GetButton("Attack"))
        {
            var dest = col.gameObject.GetComponent<Destroyable>();
            if (dest != null)
            {
                dest.TakeHit();
                return;
            }

            //var hurt = col.GetComponent<Hurtable>();
            //if (hurt != null)
            //{
            //    hurt.TakeDamage(damage);
            //}
        }
    }

    public void Use()
    {
        //Trigger player animation

        //Find all colliders in a given radius of the player
        Collider[] cols = Physics.OverlapSphere(owner.transform.position, radius);
        foreach (Collider col in cols)
        {
            //If we are in range of an enemy
            if (col && col.tag == "enemy")
            {
                //Find dot product of the vectors of player and enemy to determine direction
                Vector3 forward = owner.transform.TransformDirection(Vector3.forward);
                Vector3 toOther = col.transform.position - owner.transform.position;

                //If enemy is in front of player, deal damage
                if (Vector3.Dot(forward, toOther) > 0)
                {
                    var hurtable = col.GetComponent<Hurtable>();
                    if (hurtable != null)
                    {
                        hurtable.TakeDamage(damage);
                    }
                }
            }
        }

    }

    public string getName()
    {
        return "Hammer";
    }
}
