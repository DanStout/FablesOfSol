using UnityEngine;
using System.Collections;


public class Hammer : MonoBehaviour, IItem {

	private GameObject owner;
	private bool attacking = false;
	private float radius = 1;
	private float damage = 5;


	// Use this for initialization
	void Start () {
		owner = GameObject.FindGameObjectWithTag("Player");
	}

	

	public void Use()
	{
		//Trigger player animation

		//Find all colliders in a given radius of the player
		Collider[] cols = Physics.OverlapSphere(owner.transform.position, radius);
		foreach (Collider col in cols){
			//If we are in range of an enemy
			if (col && col.tag == "enemy"){

				//Find dot product of the vectors of player and enemy to determine direction
				Vector3 forward = owner.transform.TransformDirection(Vector3.forward);
				Vector3 toOther = col.transform.position - owner.transform.position;

				//If enemy is in front of player, deal damage
				if (Vector3.Dot(forward, toOther) > 0)
				{

					var enemy = col.GetComponent<IEnemy>();
					if (enemy != null)
					{
						enemy.TakeDamage(damage);
					}
				}
			}
		}

	}

	public string getName()
	{
		return "Hammer";
	}
}
