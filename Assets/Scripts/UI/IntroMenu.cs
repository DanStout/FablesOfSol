using UnityEngine;
using System.Collections;

public class IntroMenu : MonoBehaviour
{

    void Start()
    {
        Time.timeScale = 0;
    }

    public void OkClicked()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
            OkClicked();
    }
}