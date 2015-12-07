using UnityEngine;
using System.Collections;

public class SonicResonator : MonoBehaviour, IItem {
	
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
			if(p.gameObject.name == "SonicParticle")
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
				ISonic sonic = hit.collider.gameObject.GetComponentInChildren<ISonic>();
				if(sonic != null)
				{
					sonic.resonate();
				}
			}
		}
	}
	
	public void Use()
	{
		print ("Use!");
		//Spawn particle system
		if( particleSys != null && !isOn)
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
		return "Sonic Resonator";
	}
}
