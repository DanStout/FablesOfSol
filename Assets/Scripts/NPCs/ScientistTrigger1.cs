using UnityEngine;
using System.Collections;

public class ScientistTrigger1 : BaseOperable
{
    private Dialog dialog;
    public string DisplayName;
    public GameObject thrumPack;
    public GameObject magGun;
    private bool didOperate;

    protected override void Start()
    {
        base.Start();
        dialog = GameObject.FindGameObjectWithTag("Dialog").GetComponent<Dialog>();
    }

    public override void Operate()
    {
        dialog.ActiveNpcName = DisplayName;
        dialog.AddLine("Look who it is!");
        dialog.AddLine("You're here now so make yourself useful. These stupid thrum have been preventing my experimentation on the soil here!");
        dialog.AddLine("If you get rid of the filthy creatures I'll give you a magnetic gun so you can get that DNA you need.");
        dialog.AddLine("It's a win-win! Come back when you're finished.");
        if (thrumPack.transform.childCount == 0)
        {
            dialog.AddLine("Oh, you already did it? Fantastic. Little bastards...");
            dialog.AddLine("Here's the magnet gun I promised! You can use it to pull heavy metal objects towards yourself.");
            dialog.AddLine("Good luck ahead. You'll need it...");
            if (GameObject.FindGameObjectWithTag("MagnetGun") != null)
            {
                var item = Instantiate<GameObject>(magGun);
                item.transform.position = transform.position + Vector3.forward;
                var itemHeightOffset = item.GetComponentInChildren<Renderer>().bounds.size.y / 2;
            }

        }
        else
        {
            if (thrumPack.transform.childCount > 1)
            {
                dialog.AddLine("There are " + thrumPack.transform.childCount + " thrum left. Go get em' Champ!");
            }
            else
            {
                dialog.AddLine("There is " + thrumPack.transform.childCount + " thrum left. Go get em' Champ!");
            }
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
                dialog.AddLine("Hey! What's the hurry?");
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
