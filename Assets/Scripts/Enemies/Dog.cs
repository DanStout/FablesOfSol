using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour, IEnemy
{
    public float maxHealth = 10;
    public float damageFlashSeconds = 0.5f;

    private Animator anim;
    
    private DropsItems dropper;
    private ChasesPlayer chaser;
    private FacesPlayer faces;
    private Hurtable hurt;
    private AttacksPlayer attacks;

    void Start()
    {
        anim = GetComponent<Animator>();
        dropper = GetComponent<DropsItems>();
        chaser = GetComponent<ChasesPlayer>();
        faces = GetComponent<FacesPlayer>();

        hurt = GetComponent<Hurtable>();
        hurt.onDeath += hurt_onDeath;

        attacks = GetComponent<AttacksPlayer>();
        attacks.onPlayerDeath += attacks_onPlayerDeath;
        
        tag = "enemy"; //For items to detect and deliver damage
    }

    void attacks_onPlayerDeath()
    {
        anim.SetTrigger("howl");
    }

    void OnDisable()
    {
        hurt.onDeath -= hurt_onDeath;
    }

    void hurt_onDeath()
    {
        anim.SetTrigger("die");

        chaser.Die();
        dropper.Die();
        faces.Die();
        hurt.Die();
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
