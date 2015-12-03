using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private SettingsMenu settingsMenu;

    public HealthUI healthUI { get; private set; }

    void Awake()
    {
        healthUI = GetComponentInChildren<HealthUI>();
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
