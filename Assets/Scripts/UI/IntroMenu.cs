using UnityEngine;
using System.Collections;

public class IntroMenu : MonoBehaviour
{
    public void OkClicked()
    {
        Close();   
    }

    public void Open()
    {
        gameObject.SetActive(true);
        GameManager.Pause();
    }

    public void Close()
    {
        GameManager.Resume();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Cancel"))
            Close();
    }
}