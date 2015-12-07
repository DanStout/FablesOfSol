using UnityEngine;
using System.Collections;

public class PlayerDeathTrigger : MonoBehaviour {

	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onTriggerEnter(Collider other)
	{
		print ("Collision");
		if (other.gameObject.CompareTag("Player")) {
			player.GetComponent<PlayerLife>().TakeDamage(1000);
			print ("take damage");
		}
	}
}
