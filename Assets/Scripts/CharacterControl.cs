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
		if (Input.GetMouseButtonDown) {
			//use right click assigned item.
		}
    }

	void OnCollisionEnter(Collision collisionInfo){
		if(collisionInfo.collider.tag == "PlayerDamageSource"){
			TakeDamage(collisionInfo.collider.gameObject.Damage/*Damage a constant on each ememy*/);
		}
		if (collisionInfo.collider.tag == "Item") {
			//pick up item
		}
	}

	private void TakeDamage(float damage){
		if (health - damage > 0) {
			health = health - damage;
		} else {
			//Kill player.
			//Go to death screen.
		}
	}
}
