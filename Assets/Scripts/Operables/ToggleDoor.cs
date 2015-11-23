using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleDoor : BaseOperable
{
    public ToggleDoor[] otherDoors;

    public Transform ownDoor;
    public Vector3 positionAddition;
    private bool isToggled;

    public void Toggle()
    {
        if (isToggled)
        {
            ownDoor.localPosition -= positionAddition;
        }
        else
        {
            ownDoor.localPosition += positionAddition;
        }
        isToggled = !isToggled;
    }

    public override void Operate()
    {
        Toggle();

        foreach(var door in otherDoors)
        {
            door.Toggle();
        }
    }

    public override string ActionText
    {
        get { return "Toggle Door"; }
    }
}
