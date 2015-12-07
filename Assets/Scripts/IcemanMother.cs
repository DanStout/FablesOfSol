using UnityEngine;
using System.Collections;

public class IcemanMother : MonoBehaviour {

	public float maxHealth = 10;
	public float damageFlashSeconds = 0.5f;


	private FacesPlayer faces;
	private DropsItems dropper;
	private Hurtable hurt;
	private Animator anim;
	private GameObject player;
	private GameObject ice;

	//Private variables for attack phases.
	private bool routineRunning;

	// Use this for initialization
	void Start () {
		ice = transform.FindChild("Ice").gameObject;
		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator>();
		faces = GetComponent<FacesPlayer>();
		dropper = GetComponent<DropsItems>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ice.gameObject.activeInHierarchy == true && !routineRunning){
			StartCoroutine(attackPhase());
		}
	}


	IEnumerator attackPhase(){
		routineRunning = true;
		anim.SetTrigger("attack");
		var bossLoc = transform.position;
		var playerLoc = player.transform.localPosition;
		var abovePlayerLoc = player.transform.localPosition + Vector3.up * 10;
		Vector3 velo = Vector2.zero;
		var dist = Vector3.Distance (transform.position, abovePlayerLoc);
		gameObject.GetComponent<FacesPlayer> ().enabled = false;
		while(dist > 2){
			transform.position = Vector3.SmoothDamp (transform.position, abovePlayerLoc, ref velo, 0.3F);
			dist = Vector3.Distance (transform.position, abovePlayerLoc);
			yield return null;
		}
		dist = Vector3.Distance (transform.position, playerLoc);
		while(dist > 2){
			transform.position = Vector3.SmoothDamp (transform.position, playerLoc, ref velo, 0.3F);
			dist = Vector3.Distance (transform.position, playerLoc);
			yield return null;
		}
		StartCoroutine(afterAttackPhase());

	}

	IEnumerator afterAttackPhase(){

		yield return new WaitForSeconds (3);
		gameObject.GetComponent<FacesPlayer> ().enabled = true;
		yield return new WaitForSeconds (5);
		routineRunning = false;
		
	}
	// Magnet gun uses this to determine how to interact with other gameobjects
	public string getMaterial()
	{
		return "flesh";
	}
}
