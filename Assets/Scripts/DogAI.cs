using UnityEngine;
using System.Collections;

public class DogAI : MonoBehaviour
{
    public float speed = 5;
    public float attackDamage = 5;
    public float maxHealth = 10;
    public float attackDelay = 1; //seconds between attacks
    public float rotationSpeed = 5;
    public float sightRange = 15;
    public float attackRange = 1;
    private float currHealth;
    private GameObject player;
    private PlayerLife playerLife;
    private float lastAttackTime;

    void Start()
    {
        currHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        playerLife = player.GetComponent<PlayerLife>();
    }

    void Update()
    {
        var playerLoc = player.transform.position;
        var playerDist = Vector3.Distance(transform.position, playerLoc);

        if (playerDist <= attackRange && Time.time - lastAttackTime > attackDelay)
        {
            playerLife.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
        else if (playerDist <= sightRange)
        {
            var rotDir = playerLoc - transform.position;
            rotDir.y = 0;
            var rotVec = Quaternion.LookRotation(rotDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, Time.deltaTime * rotationSpeed);

            var movPos = playerLoc;
            movPos.y = transform.position.y;
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, movPos, step);
        }
    }
}
