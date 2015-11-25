using UnityEngine;
using System.Collections;

public class DogAI : MonoBehaviour, IEnemy
{
    public float speed = 5;
    public float attackDamage = 5;
    public float maxHealth = 10;
    public float attackDelay = 1; //seconds between attacks
    public float rotationSpeed = 5;
    public float sightRange = 15;
    public float attackRange = 1;
    public float downwardForce = -1.5f;
    public float chaseDelay = 1; //seconds between raycasts
    public LayerMask seeThroughLayers;

    private float currHealth;
    private GameObject player;
    private PlayerLife playerLife;
    private float lastAttackTime;
    private CharacterController charControl;
    private bool isChasing = false;

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
        var movement = Vector3.zero;

        if (playerDist <= sightRange)
        {
            var posDiff = playerLoc - transform.position;
            posDiff.y = 0;
            var rotVec = Quaternion.LookRotation(posDiff, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, Time.deltaTime * rotationSpeed);


            if (!isChasing)
            {
                isChasing = CanSeePlayer();
            }

            if (isChasing)
            {
                movement = (playerLoc - transform.position).normalized * speed * Time.deltaTime;
                movement = Vector3.ClampMagnitude(movement, speed);
            }

        }
        else
        {
            isChasing = false;
        }

        movement.y = downwardForce;
        charControl.Move(movement);
    }

    private bool CanSeePlayer()
    {
        var rayEndpt = player.transform.position;
        rayEndpt.y += 1;
        var ray = new Ray(transform.position, rayEndpt - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, sightRange, seeThroughLayers))
        {
            return hit.collider.gameObject.CompareTag("Player");
        }
        return false;
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

    public void TakeDamage(float amount)
    {
        currHealth -= amount;
        if (currHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
