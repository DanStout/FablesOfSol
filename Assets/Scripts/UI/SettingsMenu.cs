using UnityEngine;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    public bool IsOpen { get { return gameObject.activeSelf;} }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
