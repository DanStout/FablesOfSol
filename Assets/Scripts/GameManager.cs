﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    private static GameManager mgr;
    private static string _redirectTag;

    void Awake()
    {
        if (mgr)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
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
            var player = GameObject.FindGameObjectWithTag("Player");
            var obj = GameObject.FindGameObjectWithTag(_redirectTag);
            player.transform.position = obj.transform.position;
            _redirectTag = null;
        }
    }




}
