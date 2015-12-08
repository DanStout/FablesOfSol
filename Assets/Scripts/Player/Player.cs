using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	void Start()
	{
	
	}

    void Update()
    {
    }
    public void OnDeathAnimationEnd()
    {
        var gameScreen = GameObject.FindGameObjectWithTag("GameOverScreen").GetComponent<GameOverScreen>();
        gameScreen.Show();
    }
}
