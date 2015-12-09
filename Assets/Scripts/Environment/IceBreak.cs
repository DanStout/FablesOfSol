using UnityEngine;
using System.Collections;

public class IceBreak : MonoBehaviour {

	private Animator parentAnim;
	public ParticleSystem particle;

	public void breakIce()
	{
		particle.Play ();
		gameObject.SetActive (false);
	}

}
