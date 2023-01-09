using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveBehavior : StateMachineBehaviour
{
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject PlayerCam;

    
    private CharacterController PlayerController;
    [SerializeField]
    private float MoveSpeed = 2.5f;
    private Vector3 MoveVec;
    private Animator animator1;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator1 = animator;
        PlayerController = animator.GetComponent<CharacterController>();
        PlayerCam = GameObject.FindGameObjectWithTag("MainCamera");
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Move(animator);

        if(Input.GetMouseButtonDown(0))
        {
           // AttackTrigger("Attack1");
            Attack1(animator);
        }
        else if((Input.GetMouseButtonDown(1)))
        {
            //AttackTrigger("Attack2");
            Attack2(animator);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Skill1(animator);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Skill2(animator);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Skill3(animator);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            Roll(animator);
        }
        

        Gravity();
        
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private void Gravity()
    {
        if(PlayerController.isGrounded)
        {
            return;
        }
        else
        {
            PlayerController.Move(1f * Time.deltaTime * Physics.gravity);
        }
        

        
        
    }


    private void Move(Animator animator)
    {
        
        float HoriVec = Input.GetAxis("Horizontal");

        float VertVec = Input.GetKey(KeyCode.LeftShift) ? Input.GetAxisRaw("Vertical") : Input.GetAxis("Vertical") / 2;
        MoveSpeed = Input.GetKey(KeyCode.LeftShift) ? 8f : 4f;
        Vector3 MoveInput = new Vector3(HoriVec, 0, VertVec);

        Vector3 forwardVec = new Vector3(PlayerCam.transform.forward.x, 0f, PlayerCam.transform.forward.z).normalized;
        Vector3 rightVec = new Vector3(PlayerCam.transform.right.x, 0f, PlayerCam.transform.right.z).normalized;
        MoveVec = MoveInput.x * rightVec + MoveInput.z * forwardVec;

        Player.transform.forward = forwardVec;
        
        PlayerController.Move(MoveSpeed * Time.deltaTime * MoveVec);
       
        animator.SetFloat("HoriVec", HoriVec);
        animator.SetFloat("VertVec", VertVec);
    }

    public void AttackTrigger(string skillName)
    {
        animator1.SetTrigger(skillName);
    }

    private void Attack1(Animator animator)
    {
        animator.SetTrigger("attack");
    }

    private void Attack2(Animator animator)
    {
        animator.SetTrigger("attack2");
    }

    private void Skill1(Animator animator)
    {
        animator.SetTrigger("Skill1");
    }

    private void Skill2(Animator animator)
    {
        animator.SetTrigger("Skill2");
    }

    private void Skill3(Animator animator)
    {
         animator.SetTrigger("Skill3");
    }

    private void Roll(Animator animator)
    {
        animator.SetTrigger("Roll");
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
