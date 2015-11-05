using UnityEngine;
using System.Collections;

public class CharacterControl : MonoBehaviour
{
    public float speed = 5;
    public float jumpSpeed = 10;
    public float gravity = 10;
    public float health = 100;
    public GameObject leftClickItem = null;

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

        if (Input.GetMouseButtonDown(1)) //0 = left, 1 = right, 2 = middle
        {
            //use right click assigned item.
        }
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        switch(collisionInfo.collider.tag)
        {
            case "PlayerDamageSource":
                var enemyScript = collisionInfo.collider.gameObject.GetComponent<Enemy>();
                TakeDamage(enemyScript.damage);
                break;
            case "Item":
                break;
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        if (health < 0)
            ; //kill
    }
}
