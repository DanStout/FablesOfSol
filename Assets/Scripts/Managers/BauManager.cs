using UnityEngine;
using System.Collections;

public class BauManager : MonoBehaviour
{
    public IntroMenu intro;

    private static bool hasOpened = false;

    void Start()
    {
        if (!hasOpened)
        {
            intro.Open();
            hasOpened = true;
        }
    }
}
