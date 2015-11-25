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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        charControl = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var playerPos = player.transform.position;
        var playerDist = Vector3.Distance(playerPos, transform.position);

        if (playerDist <= sightRange)
        {
            var posDiff = playerPos - transform.position;
            posDiff.y = 0;
            var rotVec = Quaternion.LookRotation(posDiff, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotVec, Time.deltaTime * rotationSpeed);

            var movement = Vector3.zero;
            if (playerDist >= minPlayerDist)
            {
                movement = (playerPos - transform.position).normalized * speed;
            }

            anim.SetFloat("speed", movement.sqrMagnitude);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = Vector3.ClampMagnitude(movement, speed);
            charControl.Move(movement);
        }
    }
}
