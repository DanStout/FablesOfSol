using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private SettingsMenu settingsMenu;
    private float originalTimeScale;

    void Start()
    {
        originalTimeScale = Time.timeScale;
        settingsMenu.Close();
    }

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            Time.timeScale = Time.timeScale == 0 ? originalTimeScale : 0;
            settingsMenu.Toggle();
        }
    }
}
