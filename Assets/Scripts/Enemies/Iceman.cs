using UnityEngine;
using System.Collections;

public class Iceman : MonoBehaviour, IEnemy
{
	public float maxHealth = 10;
	public float damageFlashSeconds = 0.5f;
	
	private Animator anim;
	
	private DropsItems dropper;
	private ChasesPlayer chaser;
	private FacesPlayer faces;
	private Hurtable hurt;
    private AudioSource audSrc;
    private AttacksPlayer attacks;

    public AudioClip soundDeath;
    public AudioClip soundAttack;
    public AudioClip soundHurt;
    public AudioClip[] soundShatter;
	
	void Start()
	{
		anim = GetComponent<Animator>();
		dropper = GetComponent<DropsItems>();
		chaser = GetComponent<ChasesPlayer>();
		faces = GetComponent<FacesPlayer>();
        audSrc = GetComponent<AudioSource>();
        attacks = GetComponent<AttacksPlayer>();

        attacks.onPlayerHit += attacks_onPlayerHit;
		
		hurt = GetComponent<Hurtable>();
		hurt.onDeath += hurt_onDeath;

        hurt.onHurt += hurt_onHurt;

		tag = "enemy"; //For items to detect and deliver damage
	}

    public void IceShatterSound()
    {
        audSrc.PlayOneShot(soundShatter[Random.Range(0, soundShatter.Length)]);
    }

    void hurt_onHurt()
    {
        audSrc.PlayOneShot(soundHurt);
    }

    void attacks_onPlayerHit()
    {
        audSrc.PlayOneShot(soundAttack);
    }

	void OnDisable()
	{
		hurt.onDeath -= hurt_onDeath;
	}
	
	void hurt_onDeath()
	{
		anim.SetTrigger("die");
        audSrc.PlayOneShot(soundDeath);

		chaser.Die();
		dropper.Die();
		faces.Die();
		hurt.Die();
        attacks.Die();
    }
	
	void Update()
	{
		chaser.MoveTowardsPlayer();
	}
	
	// Magnet gun uses this to determine how to interact with other gameobjects
	public string getMaterial()
	{
		return "flesh";
	}
}

