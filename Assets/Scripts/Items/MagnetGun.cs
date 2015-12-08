using UnityEngine;
using System.Collections;


public class MagnetGun : BaseItem
{
    private GameObject owner;
    private bool isOn;
    private ParticleSystem particleSys;


    // Use this for initialization
    override void Awake()
    {
        owner = GameObject.FindGameObjectWithTag("Player");
        isOn = false;

        ParticleSystem[] systems = owner.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in systems)
        {
            if (p.gameObject.name == "MagnetGunParticle")
                particleSys = p;
        }
    }
	
	// Update is called once per frame
	override void Update () {
		
		//Check if the gun is on
		if(isOn)
		{
			RaycastHit hit;
			Vector3 fwd = owner.transform.TransformDirection(Vector3.forward);
			Vector3 pos = new Vector3(0, 1, 0);

			if(Physics.Raycast(owner.transform.position + pos, fwd, out hit, 20))
			{

				Titan titan = isParentTitan(hit.collider.gameObject);
				if(titan != null)
				{
					print ("found titan!");
					titan.pullThrum();
					return;
				}

				IMagnetic mag = hit.collider.gameObject.GetComponentInChildren<IMagnetic>();
				if(mag != null)
				{
					Vector3 cur = hit.collider.transform.position;
					if(Vector3.Distance(cur, owner.transform.position) > 3)
					{

                        if (!mag.isHeavierThanPlayer())
                        {
                            hit.collider.transform.position =
                                Vector3.MoveTowards(cur, owner.transform.position, .15f);
                        }
                        else
                        {
                            owner.transform.position = Vector3.MoveTowards(owner.transform.position, cur, .15f);
                        }
                    }
                }
            }
        }
    }

    public override void Use()
    {
        print("Using magnet gun");
        //Spawn particle system
        if (particleSys != null && !isOn)
        {
            isOn = true;
            particleSys.enableEmission = true;
        }
        else if (isOn)
        {
            isOn = false;
            particleSys.enableEmission = false;
        }
    }

	//Used during raycast hit to determine if we have hit a child of a magnetic object
	private Titan isParentTitan(GameObject g)
	{
		if (g.name == "ThrumTitan")
			return g.GetComponent<Titan>();
		else if (g.transform.parent != null)
			return isParentTitan (g.transform.parent.gameObject);
		else
			return null;
	}

	public string getName()
	{
		return "Magnet Gun";
	}
}
