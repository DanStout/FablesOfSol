using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour, IEnemy, IRunAnimationTransition
{
    public float maxHealth = 10;
    public float damageFlashSeconds = 0.5f;

    public AudioClip soundDie;
    public AudioClip soundHowl;
    public AudioClip soundRun;
    public AudioClip soundAttack;

    private Animator anim;

    private DropsItems dropper;
    private ChasesPlayer chaser;
    private FacesPlayer faces;
    private Hurtable hurt;
    private AttacksPlayer attacks;
    private AudioSource audioSrc;
    private bool playingRunAnim = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        dropper = GetComponent<DropsItems>();
        chaser = GetComponent<ChasesPlayer>();
        faces = GetComponent<FacesPlayer>();
        hurt = GetComponent<Hurtable>();
        hurt.onDeath += hurt_onDeath;
        audioSrc = GetComponent<AudioSource>();

        attacks = GetComponent<AttacksPlayer>();
        attacks.onPlayerDeath += attacks_onPlayerDeath;

        tag = "enemy"; //For items to detect and deliver damage
    }

    void Update()
    {
        chaser.MoveTowardsPlayer();
    }

    void attacks_onPlayerDeath()
    {
        anim.SetTrigger("howl");
    }

    void OnDisable()
    {
        hurt.onDeath -= hurt_onDeath;
        attacks.onPlayerDeath -= attacks_onPlayerDeath;
    }

    void hurt_onDeath()
    {
        anim.SetTrigger("die");

        chaser.Die();
        faces.Die();
        hurt.Die();
        dropper.Die();
    }

    // Magnet gun uses this to determine how to interact with other gameobjects
    public string getMaterial()
    {
        return "flesh";
    }

    public void OnDieAnimation()
    {
        audioSrc.PlayOneShot(soundDie);
    }

    public void OnHowlAnimation()
    {
        audioSrc.PlayOneShot(soundHowl);
    }

    public void OnAttackAnimation()
    {
        audioSrc.PlayOneShot(soundAttack);
    }

    public void OnRunStateEnter()
    {
        audioSrc.clip = soundRun;
        audioSrc.Play();
        audioSrc.loop = true;
    }

    public void OnRunStateExit()
    {
        audioSrc.Stop();
    }
}
