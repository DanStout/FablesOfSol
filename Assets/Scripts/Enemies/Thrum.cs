using UnityEngine;
using System.Collections;

public class Thrum : MonoBehaviour
{
    public float chaseRange = 4;
    public float attackDamage = 5;
    public float secondsBetweenAttacks = 1;
    public float attacksWithinRange = 2f;

    private float lastAttackTime;
    private MovesRandomly randMov;
    private Transform playerTrans;
    private NavMeshAgent agent;
    private Animator anim;
    private DropsItems drops;
    private Hurtable hurt;

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        randMov = GetComponent<MovesRandomly>();
        drops = GetComponent<DropsItems>();
        hurt = GetComponent<Hurtable>();
        hurt.onDeath += hurt_onDeath;
    }

    void OnDisable()
    {
        hurt.onDeath -= hurt_onDeath;
    }

    void hurt_onDeath()
    {
        randMov.Die();
        drops.Die();
		Destroy (this.transform.gameObject);
    }

    void Update()
    {
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

}
