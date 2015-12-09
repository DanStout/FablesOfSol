using UnityEngine;
using System.Collections;

public class BreakIce : MonoBehaviour {

	private Animator parentAnim;
	public ParticleSystem particle;
	public GameObject damageSphere; 
	
	void Start(){
		parentAnim = gameObject.GetComponentInParent<Animator>();
	}



	public void breakIce()
	{
		damageSphere.SetActive (true);
		particle.Play ();
		gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
		gameObject.GetComponent<BoxCollider>().enabled = false;
		parentAnim.SetTrigger("hit");
	}

	public void replaceIce()
	{
		damageSphere.SetActive (false);
		gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
		gameObject.GetComponent<BoxCollider>().enabled = true;
	}

}
