using UnityEngine;
using System.Collections;

public class Teleport : BaseOperable
{
	public GameObject locationObject;
	public GameObject teleportee;
	
	public override void Operate()
	{
		teleportee.transform.position = locationObject.transform.position;
	}
}
