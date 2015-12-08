using UnityEngine;
using System.Collections;

public class AttackBehaviour : StateMachineBehaviour
{
    private PlayerAttack attack;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (attack == null)
        {
            attack = animator.GetComponent<PlayerAttack>();
        }
        attack.AttackAnimationDone();
    }
}
