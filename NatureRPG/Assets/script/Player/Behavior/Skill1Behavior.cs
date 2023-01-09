using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Behavior : StateMachineBehaviour
{
    [SerializeField]
    public Collider HitCollider;
    [SerializeField]
    private Player Player;
    //[SerializeField]
    //private AttackPoint NormalAttackPoint;
    public WeaponScript Weapon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = true;
        Player = animator.GetComponent<Player>();
        HitCollider = Player.Weapon;
        HitCollider.enabled = true;
        //NormalAttackPoint = HitCollider.GetComponent<AttackPoint>();
        //NormalAttackPoint.OnChangeAtk += NormalAttackPoint.Skill1;
        Weapon = HitCollider.GetComponent<WeaponScript>();
        Weapon.Atk = 30f + (Player.Level - 1) * 10f;
        //Debug.Log("Skill1 enter");
        
            Player.Mp -= 30f;
        
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.applyRootMotion = false;
        HitCollider.enabled = false;
        //NormalAttackPoint.OnChangeAtk -= NormalAttackPoint.Skill1;
        //Debug.Log("Skill1 exit");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
