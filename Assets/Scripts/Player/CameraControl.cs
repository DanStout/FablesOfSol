using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Transform playerTransform;
    private PlayerControl playerControl;
    private Vector3 initialOffset;
    private float speed;

    void Start()
    {
        playerTransform = player.transform;
        playerControl = player.GetComponent<PlayerControl>();
        speed = playerControl.moveSpeed;
        initialOffset = transform.position - playerTransform.position;
    }

    void Update()
    {
        //viewPos stores the position of the player relative to the viewscreens edges        
        Vector3 viewPos = Camera.main.WorldToViewportPoint(playerTransform.position);

        //If the player is within the specified distance to any edge
        //move the camera towards the player.
        if (viewPos.x > 0.7F || viewPos.x < 0.3F || viewPos.y < 0.3F || viewPos.y > 0.7f)
        {
            var current = transform.position;
            var target = playerTransform.position + initialOffset;
            var increment = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(current, target, increment);
        }
    }

    public void RecenterOnPlayer()
    {
        transform.position = playerTransform.position + initialOffset;
    }
}
