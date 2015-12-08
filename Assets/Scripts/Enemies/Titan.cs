using UnityEngine;
using System.Collections;

public class Titan : MonoBehaviour, IMagnetic
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

	private bool thrumSpawned = false;
	private bool spawnedThrumDestroyed = false;
	private GameObject spawnedThrum;

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

    void OnDisable()
    {
        hurt.onDeath -= hurt_onDeath;
    }

    public void DeathAnimationDone()
    {
        drops.Die();
        if (activateOnDeath != null)
            activateOnDeath.SetActive(true);
    }
	
	void Update()
	{
		//Check if the spawned thrum has been initiated
			//Check if the spawned thrum still exists
				//If not, resume fighting
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

	//Method used by sonic resonator to pull a thrum
	public void pullThrum()
	{
		print ("PULL THRUM");
		//instantiate a thrum and store this in a variable
		spawnedThrum = (GameObject)Instantiate (Resources.Load("Thrum"));
		spawnedThrum.transform.position = this.transform.position;

		//pause titan attacking

	}

	//Called from update when the spawned thrum has been destroyed
	private void resumeAttack()
	{

	}

	public bool isHeavierThanPlayer()
	{
		return true;
	}
}
