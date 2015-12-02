using UnityEngine;
using System.Collections;

public class DnaItem : BaseItem
{
    public override string Name
    {
        get { return "DNA"; }
    }

    public override void Use()
    {
        print("Using DNA");
    }
}
