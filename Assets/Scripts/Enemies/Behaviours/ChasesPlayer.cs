using UnityEngine;
using System.Collections;

public class ChasesPlayer : MonoBehaviour
{
    public float sightRange = 15;
    public float speed = 5;
    public float downwardForce = -1.5f;
    public float rotationSpeed = 5;

    private CharacterController charControl;
    private LooksForPlayer looks;
    private GameObject player;
    private Animator anim;
    private bool isDead = false;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        looks = GetComponent<LooksForPlayer>();
        anim = GetComponent<Animator>();
        charControl = GetComponent<CharacterController>();
    }

    public void Die()
    {
        isDead = true;
    }

    public void MoveTowardsPlayer()
    {
        var playerLoc = player.transform.position;
        var playerDist = Vector3.Distance(transform.position, playerLoc);
        var movement = Vector3.zero;

        if (!isDead && playerDist <= sightRange)
        {
            var posDiff = playerLoc - transform.position;
            posDiff.y = 0;
            var rotVec = Quaternion.LookRotation(posDiff, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, Time.deltaTime * rotationSpeed);

            if (!isChasing)
            {
                isChasing = looks.CanSeePlayer();
            }

            if (isChasing)
            {
                movement = (playerLoc - transform.position).normalized * speed;
                movement = Vector3.ClampMagnitude(movement, speed);
            }

        }
        else
        {
            isChasing = false;
        }

        anim.SetFloat("speed", movement.sqrMagnitude);
        movement.y = downwardForce;
        movement *= Time.deltaTime;
        charControl.Move(movement);
    }
}
