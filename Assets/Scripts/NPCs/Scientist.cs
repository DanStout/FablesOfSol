using UnityEngine;
using System.Collections;

public class Scientist : MonoBehaviour
{
    public float sightRange = 5;
    private GameObject player;
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
