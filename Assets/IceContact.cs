using UnityEngine;
using System.Collections;

public class IceContact : MonoBehaviour {

	void OnCollisionStay(Collision col){
		if (col.collider.tag == "Player") {
			col.collider.transform.SetParent(transform);
			col.collider.GetComponent<PlayerMovement>().enabled = false;
			col.collider.GetComponent<IceMovement>().enabled = true;
		}
	}
}
