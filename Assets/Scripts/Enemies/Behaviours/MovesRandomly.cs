using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class MovesRandomly : MonoBehaviour
{
    public float moveRadius = 10;
    public float arrivedDistance = 1f;

    private NavMeshAgent agent;

	void Start()
	{
        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
	}

	void Update()
	{
        Debug.DrawLine(transform.position, agent.destination);

        var distToDest = Vector3.Distance(transform.position, agent.destination);
	    if (distToDest<= arrivedDistance)
        {
            Wait(2);
            MoveToNextPoint();
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
    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void Die()
    {
        Destroy(this);
    }
}
