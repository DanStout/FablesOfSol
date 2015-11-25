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
    public float downwardForce = -1.5f;
    private float currHealth;
    private GameObject player;
    private PlayerLife playerLife;
    private float lastAttackTime;
    private CharacterController charControl;

    void Start()
    {
        currHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        charControl = GetComponent<CharacterController>();
    }

    void Update()
    {
        var playerLoc = player.transform.position;
        var playerDist = Vector3.Distance(transform.position, playerLoc);

        if (playerDist <= sightRange)
        {
            var posDiff = playerLoc - transform.position;
            posDiff.y = 0;
            var rotVec = Quaternion.LookRotation(posDiff, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, Time.deltaTime * rotationSpeed);

            var movement = (playerLoc - transform.position).normalized * speed * Time.deltaTime;
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = downwardForce;
            charControl.Move(movement);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var playerLife = hit.gameObject.GetComponent<PlayerLife>();
        if (playerLife != null && Time.time - lastAttackTime > attackDelay)
        {
            playerLife.TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }
}
