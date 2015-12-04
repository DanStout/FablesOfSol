using UnityEngine;
using System.Collections;

public class MagnetGun : MonoBehaviour, IItem {

	private GameObject owner;
	private bool isOn;
	private ParticleSystem particleSys; 


	// Use this for initialization
	void Start(){

		owner = GameObject.FindGameObjectWithTag("Player");
		isOn = false;

		ParticleSystem[] systems = owner.GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem p in systems)
		{
			if(p.gameObject.name == "MagnetGunParticle")
				particleSys = p;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//Check if the gun is on
		if(isOn)
		{
			RaycastHit hit;
			Vector3 fwd = owner.transform.TransformDirection(Vector3.forward);
			Vector3 pos = new Vector3(0, 1, 0);

			if(Physics.Raycast(owner.transform.position + pos, fwd, out hit, 10))
			{
				if(hit.collider.gameObject.GetComponentInChildren<IMagnetic>() != null)
				{
					Vector3 cur = hit.collider.transform.position;
					if(Vector3.Distance(cur, owner.transform.position) > 3)
					{
						hit.collider.transform.position =
							Vector3.MoveTowards(cur, owner.transform.position, .15f);
					}
				}
			}
		}
	}

	public void Use()
	{
		print ("Use!");
		//Spawn particle system
		if( particleSys != null && particleSys.name == "MagnetGunParticle" && !isOn)
		{
			isOn = true;
			particleSys.enableEmission = true;
		}
		else if(isOn)
		{
			isOn = false;
			particleSys.enableEmission = false;
		}
	}

	public string getName()
	{
		return "Magnet Gun";
	}
}
