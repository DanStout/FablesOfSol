using UnityEngine;
using System.Collections;

public class Titan : MonoBehaviour
{
    public GameObject activateOnDeath;
    public float punchDamage = 3;
    public float minFire = 1;
    public float maxFire = 2;

    private DamagingParticleSystem fireBreath;
    private Transform playerTrans;
    private DropsItems drops;
    private Animator anim;
    private Hurtable hurt;

    private bool isInPunchRange = false;
    private bool wasInPunchRange = false;
    private bool leftHandLast = true;
    private bool isDead = false;

	void Start()
	{
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        fireBreath = GetComponentInChildren<DamagingParticleSystem>();
        anim = GetComponent<Animator>();

        drops = GetComponent<DropsItems>();

        hurt = GetComponent<Hurtable>();
        hurt.onDeath += hurt_onDeath;
	}

    void hurt_onDeath()
    {
        hurt.Die();
        anim.SetTrigger("die");
        isDead = true;
    }

    public void DeathAnimationDone()
    {
        drops.Die();
        activateOnDeath.SetActive(true);
    }
	
	void Update()
	{
        if (isDead) return;

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
