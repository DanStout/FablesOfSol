using UnityEngine;
using System.Collections;

public class ScientistTrigger2 : BaseOperable
{
    private Dialog dialog;
    public string DisplayName;
	public GameObject sonicRes;
    private bool didOperate;

    protected override void Start()
    {
        base.Start();
        dialog = GameObject.FindGameObjectWithTag("Dialog").GetComponent<Dialog>();
    }

    public override void Operate()
    {
        dialog.ActiveNpcName = DisplayName;
        dialog.AddLine("Haha! I'm saved!");
        dialog.AddLine("I've been stuck here for hours. I can't figure out how to get back to the entrance of this maze.");
		dialog.AddLine("I was trying to find that cave, but it must be back on that other island...");
        dialog.AddLine("Tell you what. You can have this sonic resonator and then you can help me out of this damn m...");
        dialog.AddLine("Wait! Where are you going?! Come back!");
		if(GameObject.FindGameObjectWithTag("SonicResonator") == null){
			var item = Instantiate<GameObject> (sonicRes);
			item.transform.position = transform.position + Vector3.forward;
		}
		didOperate = true;
    }

    protected override void ReactLeftRange()
    {
        var done = dialog.IsDoneDisplaying();

        dialog.CloseDialog();

        if (didOperate)
        {
            didOperate = false;

            if (!done)
            {
				dialog.AddLine("Wait! Where are you going?! Come back!");
                StartCoroutine(CloseDialogAfterSeconds((2)));
            }
        }
    }

    private IEnumerator CloseDialogAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dialog.CloseDialog();
    }
}
