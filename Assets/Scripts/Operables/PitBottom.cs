using UnityEngine;
using System.Collections;

public class PitBottom : BaseOperable{

	private GameObject pitLedge;


	//This method will move the player and camera back outside the hole
	public override void Operate()
	{
		pitLedge = GameObject.Find ("pitLedge");
		player.transform.position = pitLedge.transform.position;
	}
}
