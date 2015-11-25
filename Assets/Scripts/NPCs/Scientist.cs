using UnityEngine;
using System.Collections;

public class Scientist : MonoBehaviour
{
    public float speed = 2;
    public float sightRange = 5;
    public float minPlayerDist = 2;
    public float rotationSpeed = 1.5f;
    public float gravity = -9.8f;
    private GameObject player;
    private CharacterController charControl;
    private Animator anim;
    private NavMeshAgent agent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        var playerPos = player.transform.position;
        var playerDist = Vector3.Distance(playerPos, transform.position);

        if (playerDist <= sightRange)
        {
            agent.destination = playerPos;
        }
        else
        {
            agent.destination = transform.position;
        }

        var speed = Vector3.SqrMagnitude(agent.velocity);
        anim.SetFloat("speed", speed);
    }

}
