using UnityEngine;
using System.Collections;

public class Titan : MonoBehaviour
{
    public float punchDamage = 3;
    public float minFire = 1;
    public float maxFire = 2;
    private DamagingParticleSystem fireBreath;
    private Transform playerTrans;
    private Animator anim;

    private bool isInPunchRange = false;
    private bool wasInPunchRange = false;
    private bool leftHandLast = true;

	void Start()
	{
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        fireBreath = GetComponentInChildren<DamagingParticleSystem>();
        anim = GetComponent<Animator>();
	}
	
	void Update()
	{
        var playerPos = playerTrans.position;
        var playerDist = Vector3.Distance(transform.position, playerPos);

        if (playerDist >= minFire && playerDist <= maxFire)
        {
            anim.SetBool("isBreathingFire", true);
            fireBreath.Activate();
        }
        else
        {
            fireBreath.Deactivate();
            anim.SetBool("isBreathingFire", false);

            isInPunchRange = (playerDist < minFire);

            if (isInPunchRange && !wasInPunchRange)
            {
                wasInPunchRange = true;

                if (leftHandLast)
                {
                    anim.SetTrigger("rightPunch");
                }
                else
                {
                    anim.SetTrigger("leftPunch");
                }
            }
        }

        wasInPunchRange = isInPunchRange;
	}

    public void LeftHandAnimationDone()
    {
        leftHandLast = true;
        if (isInPunchRange)
        {
            anim.SetTrigger("rightPunch");
        }
    }

    public void RightHandAnimationDone()
    {
        leftHandLast = false;
        if (isInPunchRange)
        {
            anim.SetTrigger("leftPunch");
        }
    }
}
