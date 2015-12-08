using UnityEngine;
using System.Collections;

public class IceMovement : MonoBehaviour {

	public float moveSpeed = 6f;
	public float rotationSpeed = 10f;
	
	private CharacterController _charController;
	private PlayerLife _playerLife;
	private Animator _animator;
	private Vector3 movement;
	
	void Start()
	{
		movement = Vector3.zero;
		_charController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		_playerLife = GetComponent<PlayerLife>();
	}

	// Update is called once per frame
	void Update () {

		var horiInput = Input.GetAxis("Horizontal");
		var vertInput = Input.GetAxis("Vertical");

		if(movement.x > movement.z){
			movement.x = 0;
		}else{
			movement.z = 0;
		}
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		Vector3 pos = new Vector3(0, 1, 0);
		
		
		Physics.Raycast (transform.position + pos, fwd, out hit, 1000);
		if (horiInput != 0 && hit.distance < 0.5 && _charController.velocity.x < 1) {

			if (Input.GetKeyDown (KeyCode.D)){
				transform.rotation = Quaternion.Euler(0, 90, 0);
			}else if(Input.GetKeyDown (KeyCode.A)){
				transform.rotation = Quaternion.Euler(0, 270, 0);
			}

		} else if (vertInput != 0 && hit.distance < 0.5 && _charController.velocity.z < 1) {

			if (Input.GetKeyDown(KeyCode.W)){
				transform.rotation = Quaternion.Euler(0, 0, 0);
				}else if(Input.GetKeyDown (KeyCode.S)){
				transform.rotation = Quaternion.Euler(0, 180, 0);
			}
		}
		if (hit.distance > 3) {
			_charController.transform.position += _charController.transform.forward * 0.5f;
		} else {
			_charController.transform.position += _charController.transform.forward * 0.3f;
		}

	
	}
}
