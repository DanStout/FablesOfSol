using UnityEngine;
using System.Collections;

public class FloeFinalScientistTrigger : ScientistTrigger {
	
	public override void Operate()
	{
		dialog.ActiveNpcName = DisplayName;
		dialog.AddLine("I was hoping I'd find you here..");
		dialog.AddLine ("What's that? You've collected all three strains of resistent DNA?? You have proven yourself to be a true adventurer!");
		dialog.AddLine("Through your efforts, the human population on Sol shall be restored to its former glory!");

		didOperate = true;

		StartCoroutine(showGameOverScreen ());
	}

	protected IEnumerator showGameOverScreen()
	{
		while(!dialog.IsDoneDisplaying())
	    {
			yield return new WaitForSeconds(10);
		}

		var gameScreen = GameObject.FindGameObjectWithTag("GameOverScreen").GetComponent<GameOverScreen>();
		gameScreen.Show();
	}
}
