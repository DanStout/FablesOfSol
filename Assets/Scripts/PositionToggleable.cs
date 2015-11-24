using UnityEngine;
using System.Collections;

public class PositionToggleable : MonoBehaviour
{
    public Vector3 positionAddition;
    private bool isToggled;

    public void Toggle()
    {
        if (isToggled)
        {
            transform.localPosition -= positionAddition;
        }
        else
        {
            transform.localPosition += positionAddition;
        }
        isToggled = !isToggled;
    }
}
