using UnityEngine;
using System.Collections;

public class ScientistTrigger : BaseOperable
{
    private Dialog dialog;
    public string DisplayName;

    protected override void Start()
    {
        base.Start();
        dialog = GameObject.FindGameObjectWithTag("Dialog").GetComponent<Dialog>();
    }

    public override void Operate()
    {
        dialog.ActiveNpcName = DisplayName;
        dialog.AddLine("Thank god you're here! I was beginning to worry I would have to go to Floe and Cronon to get those samples myself!");
        dialog.AddLine("Oh. You've already got a hammer.. Looks like I won't be very useful after all..");
        dialog.AddLine("Good luck!");
    }

    protected override void ReactLeftRange()
    {
        dialog.CloseDialog();
        dialog.AddLine("Hey! What's the hurry?");
        StartCoroutine(CloseDialogAfterSeconds((2)));
    }

    private IEnumerator CloseDialogAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        dialog.CloseDialog();
    }
}
