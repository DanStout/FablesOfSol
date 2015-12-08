﻿using UnityEngine;
using System.Collections;

public class SonicResonator : Weapon
{
    private GameObject owner;
    private bool isOn;
    private ParticleSystem particleSys;

    void Awake()
    {
        owner = GameObject.FindGameObjectWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        isOn = false;

        ParticleSystem[] systems = owner.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem p in systems)
        {
            if (p.gameObject.name == "SonicParticle")
                particleSys = p;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Check if the gun is on
        if (isOn)
        {
            RaycastHit hit;
            Vector3 fwd = owner.transform.TransformDirection(Vector3.forward);
            Vector3 pos = new Vector3(0, 1, 0);

            if (Physics.Raycast(owner.transform.position + pos, fwd, out hit, 10))
            {
              	if(hit.collider.gameObject.name == "Ice")
				{
					print ("HIT THE ICE");
					hit.collider.gameObject.GetComponent<BreakIce>().breakIce();
				}
            }
        }
    }

    public override void Use()
    {
        print("Using resonator");
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


    public override void Equip()
    {
        owner.GetComponent<PlayerInventory>().Equip(PlayerInventory.WeaponType.SonicResonator);
    }
}
