using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour
{
    public float speed = 5;
    public float jumpSpeed = 10;
    public float gravity = 10;

    private CharacterController control;

    void Start()
    {
        control = GetComponent<CharacterController>();
    }

    void Update()
    {
        var xMov = Input.GetAxis("Horizontal");
        var yMov = Input.GetAxis("Vertical");
        var isJumping = Input.GetButton("Jump");
        
        var vec = new Vector3(xMov, 0, yMov);
        vec = transform.TransformDirection(vec);

        if (control.isGrounded && isJumping)
            vec.y = jumpSpeed;

        vec.y -= gravity * Time.deltaTime;

        control.Move(vec * speed * Time.deltaTime);
    }
}
