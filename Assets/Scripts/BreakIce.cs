using UnityEngine;
using System.Collections;

public class BreakIce : MonoBehaviour {

	private int hammerHits;
	private Animator parentAnim;
	public GameObject damageSphere; 
	
	void Start(){
		parentAnim = gameObject.GetComponentInParent<Animator>();
	}

	void OnCollisonEnter(Collision col){
		if(col.collider.tag == "Sound"){
			StartCoroutine(brokenIce());
		}
	}

	IEnumerator brokenIce(){
		damageSphere.SetActive (true);
		gameObject.SetActive(false);
		parentAnim.SetTrigger("hit");
			yield return new WaitForSeconds (5);
		damageSphere.SetActive (false);
		gameObject.SetActive(true);

	}

}
