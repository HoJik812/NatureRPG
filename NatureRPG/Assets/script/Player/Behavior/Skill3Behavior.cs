using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3Behavior : StateMachineBehaviour
{
    [SerializeField]
    public Collider HitCollider;
    [SerializeField]
    private Player Player;
    public WeaponScript Weapon;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator.GetComponent<Player>();
        HitCollider = Player.Weapon;
        //HitCollider.enabled = true;
        Weapon = HitCollider.GetComponent<WeaponScript>();
        Weapon.Atk = 50f + (Player.Level - 1) * 10f;
        Player.Mp -= 50f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // HitCollider.enabled = true;
        //Weapon.Atk = 50f;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        HitCollider.enabled = false;
    }

    //public void ColliderOn()
    //{
    //    Debug.Log("collider on");
    //    HitCollider.enabled = true;
    //}

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
