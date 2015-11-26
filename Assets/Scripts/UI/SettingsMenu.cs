using UnityEngine;
using System.Collections;

public class SettingsMenu : MonoBehaviour
{
    public bool IsOpen { get { return gameObject.activeSelf;} }

    public void Open()
    {
        gameObject.SetActive(true);
        GameManager.Pause();
    }

    public void Close()
    {
        GameManager.Resume();
        gameObject.SetActive(false);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnResumeClicked()
    {
        Close();
    }
}
