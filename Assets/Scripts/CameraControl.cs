using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
    public Transform playerTransform;
	public float speed;
    private Vector3 initialOffset;
	private Camera cam;

    void Start()
    {
		cam = GetComponent<Camera>();
        initialOffset = transform.position - playerTransform.position;
    }

    void Update()
    {
        
		Vector3 viewPos = cam.WorldToViewportPoint (playerTransform.position);
		if (viewPos.x > 0.7F || viewPos.x < 0.3F || viewPos.y < 0.3F || viewPos.y > 0.7f) {
			transform.position = Vector3.MoveTowards(transform.position, 
			                                         playerTransform.position + initialOffset, 
			                                         speed * Time.deltaTime);
		}
    }	
}
