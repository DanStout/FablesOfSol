using UnityEngine;
using System.Collections;

public class Teleporter : BaseOperable
{
    public string respawnDestinationTag;
    public string sceneToLoad;

    protected override void Start()
    {
        base.Start();
    }

    public override void Operate()
    {
        GameManager.RelocatePlayerToTagOnNextLevel(respawnDestinationTag);
        Application.LoadLevel(sceneToLoad);
    }
}
