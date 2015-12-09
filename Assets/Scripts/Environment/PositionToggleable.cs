using UnityEngine;
using System.Collections;

public class PositionToggleable : MonoBehaviour
{
    public Vector3 positionAddition;
    private bool isToggled;
    public AudioClip downSound;
    public AudioClip upSound;
    private AudioSource audSrc;

    void Start()
    {
        audSrc = GetComponent<AudioSource>();
    }

    public void Toggle()
    {
        if (isToggled)
        {
            transform.localPosition -= positionAddition;
            audSrc.PlayOneShot(upSound);
        }
        else
        {
            transform.localPosition += positionAddition;
            audSrc.PlayOneShot(downSound);
        }
        isToggled = !isToggled;
    }
}
