using UnityEngine;
using System.Collections;

public class IceMovement : MonoBehaviour {

	public float moveSpeed = 6f;
	
	private CharacterController _charController;
	private PlayerLife _playerLife;
	private Animator _animator;
	private float _vertSpeed;
	
	void Start()
	{
		_charController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
		_playerLife = GetComponent<PlayerLife>();
	}

	// Update is called once per frame
	void Update () {
		var horiInput = Input.GetAxis("Horizontal");
		var vertInput = Input.GetAxis("Vertical");
		var movement = Vector3.zero;
		
		if (horiInput != 0 || vertInput != 0)
		{
			movement.x = horiInput * moveSpeed;
			movement.z = vertInput * moveSpeed;
			movement = Vector3.ClampMagnitude(movement, moveSpeed);
			
			var dir = Quaternion.LookRotation(movement);
		};
	}
}
