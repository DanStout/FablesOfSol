using UnityEngine;
using System.Collections;

public class MagnetGun : MonoBehaviour, IItem {

	private GameObject owner;

	// Use this for initialization
	void Start(){
		owner = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {



	}

	public void Use()
	{
		//Spawn particle system
		ParticleSystem[] systems = (ParticleSystem[])owner.GetComponentsInChildren (typeof(ParticleSystem));
		foreach(ParticleSystem part in systems)
		{
			if( part != null && part.name == "MagnetGunParticle")
			{
				while(Input.GetButtonDown("Attack"))
				{
					part.enableEmission = true;
				}
				part.enableEmission = false;
			}
		}

		//If player is heavier, move item
		
		//If item is heavier, move player
	}

	public string getName()
	{
		return "Magnet Gun";
	}
}
