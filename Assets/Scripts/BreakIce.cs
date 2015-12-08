using UnityEngine;
using System.Collections;

public class BreakIce : MonoBehaviour {

	private int hammerHits;
	private Animator parentAnim;
	public GameObject damageSphere; 
	
	void Start(){
		parentAnim = gameObject.GetComponentInParent<Animator>();
	}


	IEnumerator brokenIce(){
		damageSphere.SetActive (true);
		gameObject.SetActive(false);
		parentAnim.SetTrigger("hit");
			yield return new WaitForSeconds (5);
		damageSphere.SetActive (false);
		gameObject.SetActive(true);

	}

	public void breakIce()
	{
		StartCoroutine(brokenIce());
	}

}
