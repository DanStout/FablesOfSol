using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform;
	public float speed; //speed that the camera will move at when moving
    private Vector3 initialOffset;
	private Camera cam;

    void Start()
    {
		cam = GetComponent<Camera>();
        initialOffset = transform.position - playerTransform.position;
    }

    void Update()
    {
		//viewPos stores the position of the player relative to the viewscreens edges        
		Vector3 viewPos = cam.WorldToViewportPoint (playerTransform.position);


		//If the player is within the specified distance to any edge
		//move the camera towards the player.
		if (viewPos.x > 0.7F || viewPos.x < 0.3F || viewPos.y < 0.3F || viewPos.y > 0.7f) {
			transform.position = Vector3.MoveTowards(transform.position, 
			                                         playerTransform.position + initialOffset, 
			                                         speed * Time.deltaTime);
		}
    }	
}
