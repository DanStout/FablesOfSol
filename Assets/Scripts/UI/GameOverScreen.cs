using UnityEngine;
using System.Collections;

public class GameOverScreen : MonoBehaviour
{
    public GameObject child;

    public void Show()
    {
        child.SetActive(true);
        GameManager.Pause();
    }

    public void OnRestartClicked()
    {
        GameManager.Resume();
        Application.LoadLevel(1);
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
