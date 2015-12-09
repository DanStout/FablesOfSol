using UnityEngine;
using System.Collections;

public class StateAlerterBehaviour : StateMachineBehaviour 
{
    public string StateName;

    private IStateListener listener;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (listener == null)
        {
            listener = animator.GetComponent<IStateListener>();
        }
        
        listener.StateEntered(StateName);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        listener.StateExited(StateName);
    }

}
