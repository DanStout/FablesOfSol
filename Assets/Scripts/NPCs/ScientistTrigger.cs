using UnityEngine;
using System.Collections;

public class ScientistTrigger : BaseOperable
{
    private Dialog dialog;
    public string DisplayName;

    void Start()
    {
        dialog = GameObject.FindGameObjectWithTag("Dialog").GetComponent<Dialog>();
    }

    public override void Operate()
    {
        dialog.ActiveNpcName = DisplayName;
        dialog.AddLine("Thank god you're here! I was beginning to worry I would have to go to Floe and Cronon to get those samples myself!");
        dialog.AddLine("Oh. You've already got a hammer.. Looks like I won't be very useful after all..");
        dialog.AddLine("Good luck!");
    }
}
