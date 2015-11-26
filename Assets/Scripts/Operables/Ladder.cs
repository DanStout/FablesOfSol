using UnityEngine;
using System.Collections;

public class Ladder : BaseOperable
{
    public override void Operate()
    {
        GameManager.RelocatePlayerToTagOnNextLevel("BauLabEntrance");
        Application.LoadLevel("1 - Bau");
    }
}
