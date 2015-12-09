using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerMovement playerMove;
    private Vector3 initialOffset;
    private float speed;
	private Quaternion initialRot;
    //private Vector3 initialPos;
	private Transform iceSurfPos;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        playerMove = player.GetComponent<PlayerMovement>();
        speed = playerMove.moveSpeed;
		initialRot = transform.rotation;
        //initialPos = transform.position;
        initialOffset = transform.position - playerTransform.position;
    }

    void Update()
    {
		if (playerTransform.GetComponent<PlayerMovement> ().enabled == true) {
			//viewPos stores the position of the player relative to the viewscreens edges
			var cameraSpd = speed;
			Vector3 viewPos = Camera.main.WorldToViewportPoint (playerTransform.position);
			transform.rotation = initialRot;
			if(Vector3.Distance(Camera.main.transform.position, playerTransform.position) > initialOffset.y + 10f){
				cameraSpd = cameraSpd*10;
			}
			//If the player is within the specified distance to any edge
			//move the camera towards the player.
			if (viewPos.x > 0.7F || viewPos.x < 0.3F || viewPos.y < 0.3F || viewPos.y > 0.7f) {
				var current = transform.position;
				var target = playerTransform.position + initialOffset;
				var increment = cameraSpd * Time.deltaTime;

				transform.position = Vector3.MoveTowards (current, target, increment);
			}
		} else {
			iceSurfPos = GameObject.FindGameObjectWithTag ("IceSurface").transform;
			var current = transform.position;
			var target = iceSurfPos.localPosition + Vector3.up * 50;
            //var increment = speed * Time.deltaTime;
			transform.rotation = Quaternion.Euler(90, 0, 0);
			transform.position = Vector3.MoveTowards (current, target, 5);
		}

    }

    public void RecenterOnPlayer()
    {
        transform.position = playerTransform.position + initialOffset;
    }
}
