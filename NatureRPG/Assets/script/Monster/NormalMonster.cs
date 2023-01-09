using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMonster : Monster
{
    public static int monsterCount = 0;
    [SerializeField]
    public Collider HitCollider;

    private enum NORMALMONSTERSTATE
    {
        Idle,
        Chase,
        Attack,
        Die
    }

    private NORMALMONSTERSTATE CurState;

    //[SerializeField]
    //private GameObject Target;
    public Player Target;
    [SerializeField]
    private float TargetDistance;
    [SerializeField]
    private float AttackDistance = 3.4f;
    [SerializeField]
    private float NormalMonsterSpeed = 2f;
    public GameObject HitEffect;
    private Animator NormalMonsterAnim;
    private CharacterController NormalMonsterController;
    private Vector3 MoveVec;
    private Transform TargetTransform;
    private Coroutine CurCoroutine;
    
    [SerializeField]
    private float NormalMonsterhp = 100f;

    public float Hp
    {
        get { return NormalMonsterhp; }
        set
        {
            NormalMonsterhp = value;
           // Debug.Log("NormalMonsterHp = " + NormalMonsterhp);
            if (NormalMonsterhp <= 0)
            {
                // ChangeState(NORMALMONSTERSTATE.Die);
                
            }
        }

    }


    private void Awake()
    {
        monsterCount++;
        // Target = GameObject.FindGameObjectWithTag("Player");
        NormalMonsterAnim = GetComponent<Animator>();
        NormalMonsterController = GetComponent<CharacterController>();
        TargetTransform = Target.transform;
    }

    private void Start()
    {
        CurState = NORMALMONSTERSTATE.Idle;
        TargetDistance = 100f;
        
        CurCoroutine = StartCoroutine(Idle());
        
    }


    private void Update()
    {
       
        TargetDistance = Vector3.Distance(transform.position, Target.transform.position);
        //Gravity();

       //if(Target.Hp <=0)
       //{
       //    ChangeState(NORMALMONSTERSTATE.Idle);
       //   
       //}

    }

    //private void Gravity()
    //{
    //    if (NormalMonsterController.isGrounded)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        NormalMonsterController.Move(1f * Time.deltaTime * Physics.gravity);
    //    }
    //    //Debug.Log(PlayerTransformPosition.y);
    //
    //
    //
    //}


    private void ChangeState(NORMALMONSTERSTATE state)
    {
       
        if(CurCoroutine != null)
        {
            StopCoroutine(CurCoroutine);
        }
             
        CurCoroutine = StartCoroutine(state.ToString());
        
    }


    public override void Hit(float atk)
    {
        //Debug.Log("monster����");
        Hp -= atk;

    }


    private IEnumerator Idle()
    {
        while(true)
        {
           
            NormalMonsterAnim.SetBool("IsMove", false);
            if(TargetDistance < 15f && Target.Hp >0)
            {
                ChangeState(NORMALMONSTERSTATE.Chase);
                yield break;
            }
            
            yield return null;
        }
  
    }

    

    protected override IEnumerator Chase()
    {
        while(true)
        {
           
            NormalMonsterAnim.SetBool("IsMove", true);
            Vector3 Look = TargetTransform.position;
            Look.y = transform.position.y;
            transform.LookAt(Look);
            MoveVec = Target.transform.position - transform.position;
            NormalMonsterController.Move(MoveVec.normalized * NormalMonsterSpeed * Time.deltaTime);

            if(NormalMonsterhp <= 0)
            {
                ChangeState(NORMALMONSTERSTATE.Die);
                yield break;

            }
            if (Target.Hp <= 0)
            {
                ChangeState(NORMALMONSTERSTATE.Idle);
                yield break;
            }
            if (TargetDistance < AttackDistance)
            {
                ChangeState(NORMALMONSTERSTATE.Attack);
                yield break;
            }
            else if(TargetDistance > 15f)
            {
                ChangeState(NORMALMONSTERSTATE.Idle);
                yield break;
            }

            yield return null;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)//������ ���� �ݶ��̴����� �浹 ���� ó��
        {
            return;
        }
        else if(other.gameObject.layer == 7)//������ ���̾�� �浹��, ������ �ݶ��̴� off => �߰����� �浹 ����
        {
            Debug.Log(other.name + "�� trigger�浹");//���⿡ ����
            other.enabled = false;
            Instantiate(HitEffect, other.bounds.center, transform.rotation);
            
        }
        else
        {
            
        }
        
    }

    protected override IEnumerator Attack()
    {
       
            NormalMonsterAnim.SetBool("IsMove", false);
            Vector3 Look = TargetTransform.position;
            Look.y = transform.position.y;
            transform.LookAt(Look);
            
            NormalMonsterAnim.SetTrigger("Attack");

            yield return new WaitForSeconds(1f);
            ChangeState(NORMALMONSTERSTATE.Chase);
      
        
    }

    protected override IEnumerator Die()
    {

        NormalMonsterAnim.SetTrigger("Die");
        Target.Exp += 100f;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

}
