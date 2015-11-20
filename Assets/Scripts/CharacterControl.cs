using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;
    public float jumpSpeed = 15f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10f;
    public float minimumFallSpeed = -1.5f;
    public float fallSpeedMultiplier = 5f;

    private CharacterController _charController;
    private float _vertSpeed;

    void Start()
    {
        _vertSpeed = minimumFallSpeed;
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        var horiInput = Input.GetAxis("Horizontal");
        var vertInput = Input.GetAxis("Vertical");
        var movement = Vector3.zero;

        if (horiInput != 0 || vertInput != 0)
        {
            movement.x = horiInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            var dir = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, dir, rotationSpeed * Time.deltaTime);
        };

        if (_charController.isGrounded)
        {
            _vertSpeed = Input.GetButton("Jump") ? jumpSpeed : minimumFallSpeed;
        }
        else
        {
            _vertSpeed += gravity * fallSpeedMultiplier * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
                _vertSpeed = terminalVelocity;
        }

        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }
}
