using UnityEngine;
using System.Collections;

public class Dog : MonoBehaviour, IEnemy, IRunAnimationTransition
{
    public float maxHealth = 10;
    public float damageFlashSeconds = 0.5f;

    public AudioClip soundDie;
    public AudioClip soundHowl;
    public AudioClip soundAttack;
    public AudioClip soundHurt;

    public AudioSource runAudioSource;

    private Animator anim;

    private DropsItems dropper;
    private ChasesPlayer chaser;
    private FacesPlayer faces;
    private Hurtable hurt;
    private AttacksPlayer attacks;
    private AudioSource audioSrc;

    void Start()
    {
        anim = GetComponent<Animator>();
        dropper = GetComponent<DropsItems>();
        chaser = GetComponent<ChasesPlayer>();
        faces = GetComponent<FacesPlayer>();
        hurt = GetComponent<Hurtable>();
        hurt.onDeath += hurt_onDeath;
        hurt.onHurt += hurt_onHurt;
        audioSrc = GetComponent<AudioSource>();

        attacks = GetComponent<AttacksPlayer>();
        attacks.onPlayerDeath += attacks_onPlayerDeath;

        tag = "enemy"; //For items to detect and deliver damage
    }

    void hurt_onHurt()
    {
        audioSrc.PlayOneShot(soundHurt);
    }

    void Update()
    {
        chaser.MoveTowardsPlayer();
    }

    void attacks_onPlayerDeath()
    {
        anim.SetTrigger("howl");
        audioSrc.PlayOneShot(soundHowl);
    }

    void OnDisable()
    {
        hurt.onDeath -= hurt_onDeath;
        attacks.onPlayerDeath -= attacks_onPlayerDeath;
    }

    void hurt_onDeath()
    {
        anim.SetTrigger("die");
        audioSrc.PlayOneShot(soundDie);

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

    public void OnAttackAnimation()
    {
        audioSrc.PlayOneShot(soundAttack);
    }

    public void OnRunStateEnter()
    {
        runAudioSource.Play();
    }

    public void OnRunStateExit()
    {
        runAudioSource.Stop();
    }
}
