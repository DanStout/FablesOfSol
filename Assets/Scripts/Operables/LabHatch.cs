using UnityEngine;
using System.Collections;

public class LabHatch : BaseOperable
{
    public override void Operate()
    {
        Application.LoadLevel("1.5 - Bau Lab");
    }
}
