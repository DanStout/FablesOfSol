using UnityEngine;
using System.Collections;

public class RunAudioBehaviour : StateMachineBehaviour
{
    private IRunAnimationTransition run;

    public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (run == null)
        {
            run = animator.GetComponent<IRunAnimationTransition>();
        }

        run.OnRunStateEnter();
    }

    public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        run.OnRunStateExit();
    }
}