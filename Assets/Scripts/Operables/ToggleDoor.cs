using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleDoor : BaseOperable
{
    public GameObject otherObject;
    public Vector3 downPosition;

    private bool selfIsOpen = false;

    public override void Operate()
    {
        if (selfIsOpen)
        {
            transform.position -= downPosition;
        }
        else
        {
            transform.position += downPosition;
        }
        selfIsOpen = !selfIsOpen;
    }

    public override string ActionText
    {
        get { return "Toggle Door"; }
    }
}
