using UnityEngine;
using System.Collections;

public class FloeFinalScientistTrigger : ScientistTrigger {

	public override void Operate()
	{
		dialog.ActiveNpcName = DisplayName;
		dialog.AddLine("You've saved man kind!");

		didOperate = true;
	}
}
