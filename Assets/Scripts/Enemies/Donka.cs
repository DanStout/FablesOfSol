using UnityEngine;
using System.Collections;

public class Donka : MonoBehaviour, IRunAnimationTransition
{
    public GameObject activateOnDeath;
    public AudioClip soundHurt;
    public AudioClip soundDie;
    public AudioClip soundAttack;
    public AudioSource oneShotSource;
    public AudioSource loopingSource;

    private ChasesPlayer chaser;
    private Hurtable hurt;
    private FacesPlayer faces;
    private DropsItems drops;
    private Animator anim;
    private AttacksPlayer attacks;

    void Start()
    {
        chaser = GetComponent<ChasesPlayer>();
        hurt = GetComponent<Hurtable>();
        faces = GetComponent<FacesPlayer>();
        drops = GetComponent<DropsItems>();
        attacks = GetComponent<AttacksPlayer>();
        anim = GetComponent<Animator>();

        hurt.onDeath += hurt_onDeath;
        hurt.onHurt += hurt_onHurt;
    }

    void hurt_onHurt()
    {
        oneShotSource.PlayOneShot(soundHurt);
    }

    void hurt_onDeath()
    {
        oneShotSource.PlayOneShot(soundDie);
        anim.SetTrigger("die");

        attacks.Die();
        chaser.Die();
        hurt.Die();
        faces.Die();
    }

    void OnDisable()
    {
        hurt.onDeath -= hurt_onDeath;
        hurt.onHurt -= hurt_onHurt;
    }

    void Update()
    {
        chaser.MoveTowardsPlayer();
    }

    public void DeathAnimationDone()
    {
        drops.Die();

        if (activateOnDeath != null)
            activateOnDeath.SetActive(true);
    }

    public void OnAttackAnimation()
    {
        oneShotSource.PlayOneShot(soundAttack);
    }

    public void OnRunStateEnter()
    {
        loopingSource.Play();
    }

    public void OnRunStateExit()
    {
        loopingSource.Stop();
    }

}
