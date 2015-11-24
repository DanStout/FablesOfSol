﻿using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;
    public float jumpSpeed = 15f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10f; //Fall speed will increase to this
    public float minimumFallSpeed = -1.5f; // The usually applied downward force
    public float fallSpeedMultiplier = 5f; //What gravity will be multiplied by when the player is falling
    public float minimumFallDamageDistance = 10; //If the player falls above this distance, they will take damage
    public float fallDamageMultiplier = 1; //How much to multiply the fall distance by to get the damage amount

    private CharacterController _charController;
    private PlayerLife _playerLife;
    private Animator _animator;
    private float _vertSpeed;

    private bool wasOnGround = true;
    private bool feetOnGround = true;
    private Vector3 fallOrigin;

    void Start()
    {
        _vertSpeed = minimumFallSpeed;
        _charController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _playerLife = GetComponent<PlayerLife>();
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

        _animator.SetFloat("speed", movement.sqrMagnitude);

        feetOnGround = false;
        RaycastHit hit;
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            feetOnGround = hit.distance <= 0.1;
        }

        if (feetOnGround)
        {
            _vertSpeed = Input.GetButton("Jump") ? jumpSpeed : minimumFallSpeed;
            if (!wasOnGround)
            {
                var fallDistance = Vector3.Distance(fallOrigin, transform.position);
                if (fallDistance > minimumFallDamageDistance)
                {
                    _playerLife.TakeDamage(fallDistance * fallDamageMultiplier);
                }
            }
        }
        else
        {
            if (wasOnGround)
            {
                fallOrigin = transform.position;
            }
            _vertSpeed += gravity * fallSpeedMultiplier * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
                _vertSpeed = terminalVelocity;
        }

        wasOnGround = feetOnGround;

        _animator.SetFloat("vertSpeed", _vertSpeed);
        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }
}
