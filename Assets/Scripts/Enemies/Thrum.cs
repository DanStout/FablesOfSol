using UnityEngine;
using System.Collections;

public class Thrum : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private GameObject player;
    private Animator anim;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        var playerDist = Vector3.Distance(transform.position, player.transform.position);
        if (playerDist > 1)
            navAgent.SetDestination(player.transform.position);

        var speed = Vector3.SqrMagnitude(navAgent.velocity);
        anim.SetFloat("speed", speed);
    }

}
