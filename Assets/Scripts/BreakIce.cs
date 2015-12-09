using UnityEngine;
using System.Collections;

public class BreakIce : MonoBehaviour {

	private Animator parentAnim;
	public ParticleSystem particle;
	public GameObject damageSphere; 
	
	void Start(){
		parentAnim = gameObject.GetComponentInParent<Animator>();
	}


	IEnumerator brokenIce(){
		damageSphere.SetActive (true);
		gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
		gameObject.GetComponent<BoxCollider>().enabled = false;
		parentAnim.SetTrigger("hit");
		yield return new WaitForSeconds (8);
		damageSphere.SetActive (false);
		gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
		gameObject.GetComponent<BoxCollider>().enabled = true;

	}

	public void breakIce()
	{
		StartCoroutine(brokenIce());
	}

}
