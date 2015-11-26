using UnityEngine;
using System.Collections;


/// <summary>
/// Handles any information which needs to be retained between scenes
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager mgr;
    private static string _redirectTag;
    private static float originalTimeScale;

    void Awake()
    {
        //Make sure we only ever have one of this object across all scenes
        if (mgr)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this);
            mgr = this;
        }
    }

    public static void RelocatePlayerToTagOnNextLevel(string tag)
    {
        _redirectTag = tag;
    }

    void OnLevelWasLoaded(int level)
    {
        if (_redirectTag != null)
        {
            var player = GameObject.FindGameObjectWithTag("PlayerAndCamera");
            var obj = GameObject.FindGameObjectWithTag(_redirectTag);
            player.transform.position = obj.transform.position;
            _redirectTag = null;
        }
    }

    public static void Pause()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    public static void Resume()
    {
        Time.timeScale = originalTimeScale;
    }




}
