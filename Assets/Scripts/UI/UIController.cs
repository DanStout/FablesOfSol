using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private SettingsMenu settingsMenu;

    void Start()
    {
        settingsMenu.Close();
    }

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            settingsMenu.Toggle();
        }
    }
}
