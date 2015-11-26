using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private SettingsMenu settingsMenu;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            if (!settingsMenu.IsOpen)
            {
                settingsMenu.Open();
            }
            else
            {
                settingsMenu.Close();
            }
        }
    }
}
