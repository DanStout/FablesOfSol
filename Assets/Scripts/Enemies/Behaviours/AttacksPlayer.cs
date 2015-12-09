using UnityEngine;
using System.Collections;

public class AttacksPlayer : MonoBehaviour
{
    public float attackDamage = 5;
    public float secondsBetweenAttacks = 1;
    public float attacksWithinRange = 2f;
    
    private Animator anim;
    private float lastAttackTime;
    private Transform playerTransform;

    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler onPlayerDeath;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        var dist = Vector3.Distance(transform.position, playerTransform.position);
        if (dist <= attacksWithinRange)
        {
            anim.SetTrigger("attack");
        }
    }

    public void Die()
    {
        Destroy(this);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var playerLife = hit.gameObject.GetComponent<PlayerLife>();
        if (playerLife != null && Time.time - lastAttackTime > secondsBetweenAttacks)
        {
            var playerDied = playerLife.TakeDamage(attackDamage);
            if (playerDied)
            {
                if (onPlayerDeath != null)
                    onPlayerDeath();
            }
            lastAttackTime = Time.time;
        }
    }

}
