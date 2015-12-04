using UnityEngine;
using System.Collections;

public class MagnetGun : MonoBehaviour, IItem {

	private GameObject owner;
	private bool isOn;
	// Use this for initialization
	void Start(){
		owner = GameObject.FindGameObjectWithTag("Player");
		isOn = false;
	}
	
	// Update is called once per frame
	void Update () {



	}

	public void Use()
	{
		print ("Use!");
		//Spawn particle system
		ParticleSystem[] systems = owner.GetComponentsInChildren<ParticleSystem>();
		foreach(ParticleSystem part in systems)
		{
			if( part != null && part.name == "MagnetGunParticle" && !isOn)
			{
				isOn = true;
				part.enableEmission = true;
			}
			else if(isOn)
			{
				isOn = false;
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
