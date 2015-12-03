using UnityEngine;
using System.Collections;

public class Teleporter : BaseOperable
{
    public string respawnDestinationTag;
    public string sceneToLoad;

    public override void Operate()
    {
        GameManager.RelocatePlayerToTagOnNextLevel(respawnDestinationTag);
        Application.LoadLevel(sceneToLoad);
    }
}
