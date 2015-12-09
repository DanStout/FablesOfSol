using UnityEngine;
using System.Collections;

public class EndStateDetector : MonoBehaviour {

	// Use this for initialization
	public GameObject iceMother;
	public GameObject scientist;
	public GameObject scientistSpawn;

	private bool endStateReached = false;
	// Update is called once per frame
	void Update () {

		if (iceMother == null && !endStateReached) {
			endStateReached = true;
			Instantiate(scientist, scientistSpawn.transform.position, Quaternion.identity);
		}
	}
}
