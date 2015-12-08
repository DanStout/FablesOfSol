using UnityEngine;
using System.Collections;

public class Hammer : Weapon
{
    public float radius = 1;
    public float damage = 5;

    private GameObject owner;
    private Animator anim;
    private AudioSource playerAudSrc;

    public AudioClip[] swooshes;
    public AudioClip[] thumps;

    protected override void InitialSetup()
    {
        if (owner == null) owner = GameObject.FindGameObjectWithTag("Player");
        if (playerAudSrc == null) playerAudSrc = owner.GetComponent<AudioSource>();
        if (anim == null) anim = owner.GetComponent<Animator>();
    }

    public override void Use()
    {
        anim.SetTrigger("attack");
        playerAudSrc.PlayOneShot(swooshes[Random.Range(0, swooshes.Length)]);
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
                    playerAudSrc.PlayOneShot(thumps[Random.Range(0, thumps.Length)]);
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
