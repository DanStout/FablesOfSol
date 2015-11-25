using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleDoor : BaseOperable
{
    public PositionToggleable[] otherDoors;

    public PositionToggleable ownDoor;

    public override void Operate()
    {
        ownDoor.Toggle();

        foreach (var door in otherDoors)
        {
            door.Toggle();
        }
    }

    public override string ActionText
    {
        get { return "Toggle Door"; }
    }
}
