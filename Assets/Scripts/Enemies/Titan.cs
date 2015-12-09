using UnityEngine;
using System.Collections;

public class Titan : MonoBehaviour, IMagnetic
{
    public GameObject activateOnDeath;
    public float punchDamage = 3; //used by the scripts attached to hands
    public float minFire = 1; //minimum range to use fire breath
    public float maxFire = 2; //max range

    public int numThrumsToSpawn;

    private DamagingParticleSystem fireBreath;
    private Transform playerTrans;
    private DropsItems drops;
    private Animator anim;
    private Hurtable hurt;

    private bool isInPunchRange = false;
    private bool wasInPunchRange = false;
    private bool leftHandLast = true;
    private bool isDead = false;

	private bool isThrumSpawned = false;
	private int numOfSpawns;

    public GameObject thrumPrefab;
	private GameObject curThrum;

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
        if (hurt != null)
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
		if (isDead) return;

		//Check if the spawned thrum has been initiated
		if (isThrumSpawned && curThrum == null) 
		{
			isThrumSpawned = false;
			if(numOfSpawns == numThrumsToSpawn)
			{
                hurt.SetHealthToLevel(1);
			}
		}

			//Check if the spawned thrum still exists
				//If not, resume fighting
		if(!isThrumSpawned)
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

	//Method used by magnet gunto pull a thrum
	public void pullThrum()
	{
		print ("PULL THRUM");

		//instantiate a thrum and store this in a variable
		if(!isThrumSpawned && numOfSpawns != numThrumsToSpawn)
		{
			fireBreath.Deactivate();
			anim.SetBool("isBreathingFire", false);

			numOfSpawns ++;
			isThrumSpawned = true;

            curThrum = (GameObject) Instantiate(thrumPrefab, 
			                                    this.transform.FindChild("SpawnPoint").transform.position, 
			                                    Quaternion.identity);

		}
	}

	public bool isHeavierThanPlayer()
	{
		return true;
	}
}
