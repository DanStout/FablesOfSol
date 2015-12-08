using UnityEngine;
using System.Collections;

public class DnaItem : BaseItem
{
    public override void Use()
    {
        print("Using DNA");
    }

    public override string Name
    {
        get { return "DNA"; }
    }

    protected override void InitialSetup()
    {

    }
}
