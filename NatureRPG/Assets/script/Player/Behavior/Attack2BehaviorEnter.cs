using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2BehaviorEnter : StateMachineBehaviour
{
    [SerializeField]
    public Collider HitCollider;
    [SerializeField]
    private Player Player;
    
    public WeaponScript Weapon;
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = animator.GetComponent<Player>();
        HitCollider = Player.Weapon;
        HitCollider.enabled = true;
        //NormalAttackPoint = HitCollider.GetComponent<AttackPoint>();
        //NormalAttackPoint.OnChangeAtk += NormalAttackPoint.NormalAttack;
        //Debug.Log("attack2 enter");
        Weapon = HitCollider.GetComponent<WeaponScript>();
        Weapon.Atk = 20f + (Player.Level - 1) * 1f;
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("attack2");
        }
        //NormalAttackPoint.OnChangeAtk -= NormalAttackPoint.NormalAttack;
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Weapon.Atk = 0f;
       // NormalAttackPoint.OnChangeAtk -= NormalAttackPoint.NormalAttack;
        //Debug.Log("attack2 exit");
    }
}
