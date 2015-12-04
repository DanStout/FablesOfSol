using UnityEngine;
using System.Collections;

public class Titan : MonoBehaviour
{
    public float minFire = 1;
    public float maxFire = 2;
    private DamagingParticleSystem fireBreath;
    private Transform playerTrans;
    private Animator anim;

	void Start()
	{
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        fireBreath = GetComponentInChildren<DamagingParticleSystem>();
        anim = GetComponent<Animator>();
	}
	
	void Update()
	{
        var playerPos = playerTrans.position;
        var dist = Vector3.Distance(transform.position, playerPos);

        if (dist >= minFire && dist <= maxFire)
        {
            anim.SetBool("isBreathingFire", true);
            fireBreath.Activate();
        }
        else
        {
            fireBreath.Deactivate();
            anim.SetBool("isBreathingFire", false);
        }
	}
}
