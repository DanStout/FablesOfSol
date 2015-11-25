using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public void PlayClicked()
    {
        Application.LoadLevel(1);
    }

    public void QuitClicked()
    {
        Application.Quit();
    }
}
