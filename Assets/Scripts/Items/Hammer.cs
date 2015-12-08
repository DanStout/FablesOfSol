using UnityEngine;
using System.Collections;

public class Hammer : Weapon
{
    public float radius = 1;
    public float damage = 5;

    private GameObject owner;
    private GameObject hammer;
    private Animator anim;

    void Awake()
    {
        owner = GameObject.FindGameObjectWithTag("Player");
        anim = owner.GetComponent<Animator>();
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
        }
    }

    public override void Use()
    {
        anim.SetTrigger("attack");
        //Find all colliders in a given radius of the player
        Collider[] cols = Physics.OverlapSphere(owner.transform.position, radius);
        foreach (Collider col in cols)
        {
			print(col.name);
            //If we are in range of an enemy
            if (col && col.tag == "enemy")
            {
				print ("COLLIDED WITH " + col.gameObject.name);
                //Find dot product of the vectors of player and enemy to determine direction
                Vector3 forward = owner.transform.TransformDirection(Vector3.forward);
                Vector3 toOther = col.transform.position - owner.transform.position;

                //If enemy is in front of player, deal damage
                if (Vector3.Dot(forward, toOther) > 0)
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

                    //GameObject titan = isParentTitan(col.gameObject);
                    //if(titan != null)
                    //{
                    //    var hurt = titan.GetComponent<Hurtable>();
                    //    hurt.TakeDamage(damage);
                    //    return;
                    //}

                    //print(titan);

                    
                }
            }
        }

    }

	//Used during hit to destroy thrum titan
	private GameObject isParentTitan(GameObject g)
	{
		if (g.name == "ThrumTitan")
			return g;
		else if (g.transform.parent != null)
			return isParentTitan(g.transform.parent.gameObject);
		else
			return null;
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
