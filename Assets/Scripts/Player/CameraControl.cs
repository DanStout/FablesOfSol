using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerMovement playerMove;
    private Vector3 initialOffset;
    private float speed;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerMove = player.GetComponent<PlayerMovement>();
        speed = playerMove.moveSpeed;
        initialOffset = transform.position - playerTransform.position;
    }

    void Update()
    {
		if (playerTransform.parent.tag != "IceSurface") {
			//viewPos stores the position of the player relative to the viewscreens edges        
			Vector3 viewPos = Camera.main.WorldToViewportPoint (playerTransform.position);

			//If the player is within the specified distance to any edge
			//move the camera towards the player.
			if (viewPos.x > 0.7F || viewPos.x < 0.3F || viewPos.y < 0.3F || viewPos.y > 0.7f) {
				var current = transform.position;
				var target = playerTransform.position + initialOffset;
				var increment = speed * Time.deltaTime;

				transform.position = Vector3.MoveTowards (current, target, increment);
			}
		} else {
			Vector3 iceViewPos = Camera.main.WorldToViewportPoint(playerTransform.parent.transform.position);
		}

    }

    public void RecenterOnPlayer()
    {
        transform.position = playerTransform.position + initialOffset;
    }
}
