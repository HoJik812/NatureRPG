using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMonster : Monster

{
    public Collider HitCollider;

    private enum SUMMONMONSTERSTATE
    {
        Idle,
        Chase,
        Attack,
        Die
    }

    private SUMMONMONSTERSTATE CurState;

    //[SerializeField]
    //private GameObject Target;
    public Player Target;
    [SerializeField]
    private float TargetDistance;
    [SerializeField]
    private float AttackDistance = 3.4f;
    [SerializeField]
    private float SummonMonsterSpeed = 2f;
    public GameObject HitEffect;
    private Animator SummonMonsterAnim;
    private CharacterController SummonMonsterController;
    private Vector3 MoveVec;
    private Transform TargetTransform;
    private Coroutine CurCoroutine;
    public LayerMask testLayerMask;
   
    [SerializeField]
    private float SummonMonsterhp = 100f;

    public float Hp
    {
        get { return SummonMonsterhp; }
        set
        {
            SummonMonsterhp = value;
            // Debug.Log("NormalMonsterHp = " + NormalMonsterhp);
            if (SummonMonsterhp <= 0)
            {
                // ChangeState(NORMALMONSTERSTATE.Die);

            }
        }

    }


    private void Awake()
    {

        // Target = GameObject.FindGameObjectWithTag("Player");
        SummonMonsterAnim = GetComponent<Animator>();
        SummonMonsterController = GetComponent<CharacterController>();
        TargetTransform = Target.transform;
    }

    private void Start()
    {
        CurState = SUMMONMONSTERSTATE.Idle;
        TargetDistance = 100f;
        SummonMonsterAnim.SetBool("IsWait", true);
        CurCoroutine = StartCoroutine(Idle());
        //StartCoroutine(Idle());
    }


    private void Update()
    {

        TargetDistance = Vector3.Distance(transform.position, Target.transform.position);
        

    }

    
    private void ChangeState(SUMMONMONSTERSTATE state)
    {
        //Debug.Log(state.ToString());
        if (CurCoroutine != null)
        {
            StopCoroutine(CurCoroutine);
        }

        CurCoroutine = StartCoroutine(state.ToString());
        //StartCoroutine(state.ToString());
        //Debug.Log(state.ToString());
    }


    public override void Hit(float atk)
    {
        //Debug.Log("monster����");
        Hp -= atk;

    }


    private IEnumerator Idle()
    {
        while (true)
        {
            //NormalMonsterAnim.SetBool("IsAttack", false);
            SummonMonsterAnim.SetBool("IsMove", false);
            if (TargetDistance < 15f && Target.Hp > 0)
            {
                ChangeState(SUMMONMONSTERSTATE.Chase);
                yield break;
            }
            //Debug.Log("Idle��...");
            yield return null;
        }

    }



    protected override IEnumerator Chase()
    {
        while (true)
        {
            //Debug.Log("�߰���..");
            SummonMonsterAnim.SetBool("IsMove", true);
            Vector3 Look = TargetTransform.position;
            Look.y = transform.position.y;
            transform.LookAt(Look);
            MoveVec = Target.transform.position - transform.position;
            SummonMonsterController.Move(MoveVec.normalized * SummonMonsterSpeed * Time.deltaTime);

            if (SummonMonsterhp <= 0)
            {
                ChangeState(SUMMONMONSTERSTATE.Die);
                yield break;

            }
            if (Target.Hp <= 0)
            {
                ChangeState(SUMMONMONSTERSTATE.Idle);
                yield break;
            }
            if (TargetDistance < AttackDistance)
            {
                ChangeState(SUMMONMONSTERSTATE.Attack);
                yield break;
            }
            else if (TargetDistance > 15f)
            {
                ChangeState(SUMMONMONSTERSTATE.Idle);
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
        else if (other.gameObject.layer == 7)//������ ���̾�� �浹��, ������ �ݶ��̴� off => �߰����� �浹 ����
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
        //Debug.Log("����!!");
        // while(true)
        // {
        SummonMonsterAnim.SetBool("IsMove", false);
        Vector3 Look = TargetTransform.position;
        Look.y = transform.position.y;
        transform.LookAt(Look);
        //Debug.Log("����");
        SummonMonsterAnim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);
        ChangeState(SUMMONMONSTERSTATE.Chase);
        // }

    }

    protected override IEnumerator Die()
    {

        SummonMonsterAnim.SetTrigger("Die");
        Target.Exp += 100f;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
