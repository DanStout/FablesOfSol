using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class MovesRandomly : MonoBehaviour
{
    public float moveRadius = 10;
    public float arrivedDistance = 1f;
    private float distToDest;
    public float secondsToStayAtDestination = 1;
    public float secondsToCheckForSameDestination = 10;

    private bool isChoosing = false;
    private NavMeshAgent agent;
    private Vector3 lastPosition;

	void Start()
	{
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
        StartCoroutine(DestinationTakingTooLong());
	}

	void Update()
	{
        Debug.DrawLine(transform.position, agent.destination);

        distToDest = Vector3.Distance(transform.position, agent.destination);
	    if (!isChoosing && distToDest <= arrivedDistance)
        {
            StartCoroutine(WaitBeforeMoving(secondsToStayAtDestination));
        }
	}

    private void MoveToNextPoint()
    {
        var randomDir = Random.insideUnitSphere * moveRadius + Vector3.one;
        randomDir += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, moveRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    private IEnumerator WaitBeforeMoving(float seconds)
    {
        isChoosing = true;
        yield return new WaitForSeconds(seconds);
        MoveToNextPoint();
        isChoosing = false;
    }

    public void Die()
    {
        Destroy(this);
    }

    private IEnumerator DestinationTakingTooLong()
    {
        while(true)
        {
            var oldDest = agent.destination;
            yield return new WaitForSeconds(secondsToCheckForSameDestination);
            var newDest = agent.destination;

            if (oldDest == newDest)
                StartCoroutine(WaitBeforeMoving(0));
        }
    }
}
