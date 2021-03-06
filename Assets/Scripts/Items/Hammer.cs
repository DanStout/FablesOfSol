﻿using UnityEngine;
using System.Collections;

public class Hammer : Weapon
{
    public float radius = 1;
    public float damage = 5;
    public float attackDelaySeconds = 0.5f;
    public AudioClip[] swooshes;
    public AudioClip[] thumps;

    private GameObject owner;
    private Animator anim;
    private AudioSource playerAudSrc;
    private float lastAttackTime;

    protected override void InitialSetup()
    {
        if (owner == null) owner = GameObject.FindGameObjectWithTag("Player");
        if (playerAudSrc == null) playerAudSrc = owner.GetComponent<AudioSource>();
        if (anim == null) anim = owner.GetComponent<Animator>();
    }

    public override void Use()
    {
        if (Time.time - lastAttackTime < attackDelaySeconds) return;
        else lastAttackTime = Time.time;

        var didHit = false;

        anim.SetTrigger("attack");

        //Find all colliders in a given radius of the player
        Collider[] cols = Physics.OverlapSphere(owner.transform.position + new Vector3(0, 1, 0), radius);
        foreach (Collider col in cols)
        {
            //If we are in range of an enemy
            if (col.tag == "enemy" || col.tag == "destroyable")
            {
				print ("COLLIDED WITH " + col.gameObject.name);
                //Find dot product of the vectors of player and enemy to determine direction
                Vector3 forward = owner.transform.TransformDirection(Vector3.forward);
                Vector3 toOther = col.transform.position - owner.transform.position;

                //If enemy is in front of player, deal damage
                if (Vector3.Dot(forward, toOther) > 0)
                {
                    didHit = true;
                    if (col.tag == "enemy")
                    {
                        Hurtable hurtable;
                        var childCol = col.GetComponent<CollisionChild>();
                        if (childCol != null)
                        {
                            print(childCol);
                            hurtable = childCol.parent.GetComponent<Hurtable>();
                        }
                        else
                        {
                            hurtable = col.GetComponent<Hurtable>();
                        }


                        if (hurtable != null)
                        {
                            if (col.transform.childCount < 2 || col.transform.GetChild(1).tag != "Ice")
                            {
                                hurtable.TakeDamage(damage);
                            }
                            else
                            {
                                var iceman = col.GetComponent<Iceman>();
                                if (iceman != null)
                                    iceman.IceShatterSound();

                                col.transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                                Destroy(col.transform.GetChild(1).gameObject);
                            }
                        }
                    }
                    else if (col.tag == "destroyable")
                    {
                        var dest = col.gameObject.GetComponent<Destroyable>();
                        if (dest != null)
                        {
                            dest.TakeHit();
                            return;
                        }
                        else
                            print("Object with destroyable tag does not have destroyable component");
                    }

                }
            }
        }

        var soundSrc = didHit ? thumps : swooshes;
        playerAudSrc.PlayOneShot(soundSrc[Random.Range(0, soundSrc.Length)]);
    }

    public override void Equip()
    {
        owner.GetComponent<PlayerInventory>().Equip(PlayerInventory.WeaponType.Hammer);
    }

    public override string Name
    {
        get { return "Hammer"; }
    }
}
