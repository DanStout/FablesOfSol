using UnityEngine;
using System.Collections;

public class DieBehaviour : StateMachineBehaviour
{
    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var gameScreen = GameObject.FindGameObjectWithTag("GameOverScreen").GetComponent<GameOverScreen>();
        gameScreen.Show();
    }
}
