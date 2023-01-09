using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3Behaviour : StateMachineBehaviour
{
    [SerializeField]
    public Collider HitCollider;
    [SerializeField]
    private BossMonster BossMonster;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossMonster = animator.GetComponent<BossMonster>();
        HitCollider = BossMonster.BossHitCollider3;
        HitCollider.enabled = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCollider.enabled = false;
    }

}
