using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack1BehaviorEnter : StateMachineBehaviour
{
    [SerializeField]
    public Collider HitCollider;
    [SerializeField]
    private Player Player;
    //[SerializeField]
    //private AttackPoint NormalAttackPoint;
    public WeaponScript Weapon;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        Player = animator.GetComponent<Player>();
        HitCollider = Player.Weapon;
        HitCollider.enabled = true;
        Weapon = HitCollider.GetComponent<WeaponScript>();
        Weapon.Atk = 20f + (Player.Level-1) * 1f;
        //Debug.Log("attack1 enter");
        // NormalAttackPoint = animator.GetComponent<AttackPoint>();
        // NormalAttackPoint = HitCollider.GetComponent<AttackPoint>();
        // NormalAttackPoint.OnChangeAtk += NormalAttackPoint.NormalAttack;
        // Debug.Log(NormalAttackPoint.PlayerAtk);
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attack");
        }
        //NormalAttackPoint.OnChangeAtk -= NormalAttackPoint.NormalAttack;
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //NormalAttackPoint.OnChangeAtk -= NormalAttackPoint.NormalAttack;
        //Debug.Log("attack1 exit");
    }
}
