using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class ChasesPlayer : MonoBehaviour
{
    public float sightRange = 15;
    public float speed = 5;
    public float downwardForce = -1.5f;
    public LayerMask visibleLayers; //Cannot see objects on trigger layer

    private CharacterController charControl;
    private GameObject player;
    private Animator anim;
    private bool isDead = false;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        charControl = GetComponent<CharacterController>();
    }

    public void Die()
    {
        isDead = true;
    }

    private bool CanSeePlayer()
    {
        var rayEndpt = player.transform.position;
        rayEndpt.y += 1; //otherwise it's too low..
        var ray = new Ray(transform.position, rayEndpt - transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, sightRange, visibleLayers))
        {
            return hit.collider.gameObject.CompareTag("Player");
        }
        return false;
    }

    public void MoveTowardsPlayer()
    {
        var playerLoc = player.transform.position;
        var playerDist = Vector3.Distance(transform.position, playerLoc);
        var movement = Vector3.zero;

        if (!isDead && playerDist <= sightRange)
        {
            var posDiff = playerLoc - transform.position;

            if (!isChasing)
            {
                isChasing = CanSeePlayer();
            }

            if (isChasing)
            {
                movement = posDiff.normalized * speed;
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
