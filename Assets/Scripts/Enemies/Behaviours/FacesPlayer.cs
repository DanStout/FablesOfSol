using UnityEngine;
using System.Collections;

public class FacesPlayer : MonoBehaviour
{
    private GameObject player;
    public float sightRange = 20;
    public float rotationSpeed = 5;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        var playerPos = player.transform.position;
        var dist = Vector3.Distance(transform.position, playerPos);

        if (dist <= sightRange)
        {
            var posDiff = playerPos - transform.position;
            posDiff.y = 0;
            var rotVec = Quaternion.LookRotation(posDiff, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, Time.deltaTime * rotationSpeed);
        }
    }

    public void Die()
    {
        enabled = false;
    }
}
