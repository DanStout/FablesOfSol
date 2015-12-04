using UnityEngine;
using System.Collections;

public class LooksForPlayer : MonoBehaviour
{
    public float sightRange = 10;
    public LayerMask visibleLayers; //Cannot see objects on trigger layer
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool CanSeePlayer()
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
}
