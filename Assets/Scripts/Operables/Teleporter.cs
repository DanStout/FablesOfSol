using UnityEngine;
using System.Collections;

public class Teleporter : BaseOperable
{
    public override void Operate()
    {
        Application.LoadLevel("2 - Cronon");
    }
}
