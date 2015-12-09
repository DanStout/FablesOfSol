using UnityEngine;
using System.Collections;

public class Thrum : MonoBehaviour, IRunAnimationTransition
{
    public float chaseRange = 4;
    public float attackDamage = 5;
    public float secondsBetweenAttacks = 1;
    public float attacksWithinRange = 2f;
    public AudioClip soundDeath;
    public AudioClip soundMove;

    private float lastAttackTime;
    private MovesRandomly randMov;
    private Transform playerTrans;
    private NavMeshAgent agent;
    private Animator anim;
    private DropsItems drops;
    private Hurtable hurt;
    private bool isDead = false;
    private AudioSource audSrc;

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        randMov = GetComponent<MovesRandomly>();
        drops = GetComponent<DropsItems>();
        audSrc = GetComponent<AudioSource>();

        hurt = GetComponent<Hurtable>();
        hurt.onDeath += hurt_onDeath;
    }

    void OnDisable()
    {
        hurt.onDeath -= hurt_onDeath;
    }

    void hurt_onDeath()
    {
        isDead = true;
        drops.Die();
        randMov.Die();
        audSrc.PlayOneShot(soundDeath);
    }

    void Update()
    {
        if (isDead) return;

        var playerPos = playerTrans.position;
        var playerDist = Vector3.Distance(playerPos, transform.position);
        if (playerDist <= chaseRange)
        {
            agent.destination = playerPos;
            randMov.enabled = false;
        }
        else
        {
            randMov.enabled = true;
        }

        anim.SetFloat("speed", agent.desiredVelocity.sqrMagnitude);
    }

    void OnCollisionStay(Collision col)
    {
        var playerLife = col.gameObject.GetComponent<PlayerLife>();
        if (playerLife != null && Time.time - lastAttackTime > secondsBetweenAttacks)
        {
            playerLife.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }


    public void OnRunStateEnter()
    {
        audSrc.clip = soundMove;
        audSrc.loop = true;
        audSrc.Play();
    }

    public void OnRunStateExit()
    {
        audSrc.loop = false;
        audSrc.Stop();
    }
}
