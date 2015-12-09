using UnityEngine;
using System.Collections;

/// <summary>
/// Handles any information which needs to be retained between scenes
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject itemScriptHolder;
    private static GameManager mgr;
    private static string _redirectTag;
    private static float originalTimeScale;

    void Awake()
    {
        //Make sure we only ever have one of this object across all scenes
        if (mgr != null)
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
            if (obj == null)
            {
                print("Object with tag {0} was not found".FormatWith(_redirectTag));
                return;
            }
            player.transform.position = obj.transform.position;
            _redirectTag = null;
        }
    }

    /// <summary>
    /// Persists an item script so it'll be retained across scenes
    /// </summary>
    /// <param name="item">The item script to retain. (The original will be deleted when its gameobject is deleted)</param>
    public BaseItem SaveItem(BaseItem item)
    {
        var savedItem = itemScriptHolder.AddComponent(item.GetType()) as BaseItem;
        savedItem = savedItem.GetCopyOf(item);
        savedItem.inventoryTile = item.inventoryTile;
        return savedItem;
    }

    public static GameManager GetInstance()
    {
        return mgr;
    }

    public static void Pause()
    {
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public static void Resume()
    {
        Time.timeScale = originalTimeScale;
        AudioListener.pause = false;
    }

}
