using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonster : Monster
{
    private enum BOSSSTATE
    {
        Idle,
        Chase,
        Attack,
        Die
    }

    private CharacterController BossController;
    private Animator BossAnimator;

    public GameObject HitEffect;
    public Collider BossHitCollider1;
    public Collider BossHitCollider2;
    public Collider BossHitCollider3;
    [SerializeField]
    private float BossSpeed = 3f;
    public Player Target;
    [SerializeField]
    private float TargetDistance;
    [SerializeField]
    private float AttackDistance = 3f;
    [SerializeField]
    private float BossHp = 1000f;

    private int AttackNum;
    
    public float Hp
    {
        get { return BossHp; }
        set
        {
            BossHp = value;
            
            if (BossHp <= 0)
            {
                

            }
        }

    }
    private Vector3 BossMoveVec;
    private Transform TargetTransform;
    private Coroutine BossCurCoroutine;
    private void Awake()
    {
        BossAnimator = GetComponent<Animator>();
        BossController = GetComponent<CharacterController>();
        TargetTransform = Target.transform;
        AttackNum = 0;
    }

    private void Start()
    {
        TargetDistance = 100f;
        BossCurCoroutine = StartCoroutine(Idle());
    }

    private void Update()
    {
        TargetDistance = Vector3.Distance(transform.position, Target.transform.position);
        if (BossController.isGrounded)
        {
            return;
        }
        else
        {
            BossController.Move(Physics.gravity * 1f * Time.deltaTime);
        }
    }

    public override void Hit(float atk)
    {
        Hp -= atk;
    }

    private void ChangeState(BOSSSTATE state)
    {
        
        if (BossCurCoroutine != null)
        {
            StopCoroutine(BossCurCoroutine);
        }

        BossCurCoroutine = StartCoroutine(state.ToString());
        
    }

    private IEnumerator Idle()
    {
        while (true)
        {
            //NormalMonsterAnim.SetBool("IsAttack", false);
            BossAnimator.SetBool("IsMove", false);
            if (TargetDistance < 20f && Target.Hp > 0)
            {
                ChangeState(BOSSSTATE.Chase);
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
            BossAnimator.SetBool("IsMove", true);
            Vector3 Look = TargetTransform.position;
            Look.y = transform.position.y;
            transform.LookAt(Look);
            BossMoveVec = Target.transform.position - transform.position;
            BossController.Move(BossMoveVec.normalized * BossSpeed * Time.deltaTime);
            
            if (BossHp <= 0)
            {
                ChangeState(BOSSSTATE.Die);
                yield break;

            }
            if (Target.Hp <= 0)
            {
                ChangeState(BOSSSTATE.Idle);
                yield break;
            }
            if (TargetDistance < AttackDistance)
            {
                ChangeState(BOSSSTATE.Attack);
                yield break;
            }
            else if (TargetDistance > 20f)
            {
                ChangeState(BOSSSTATE.Idle);
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
            //Debug.Log(other.name + "�� trigger�浹");//���⿡ ����
            Instantiate(HitEffect, other.bounds.center, transform.rotation);
            other.enabled = false;
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
        AttackNum = Random.Range(1, 4);
        BossAnimator.SetBool("IsMove", false);
        Vector3 Look = TargetTransform.position;
        Look.y = transform.position.y;
        transform.LookAt(Look);
        //Debug.Log("����");

        BossAnimator.SetTrigger("Attack"+ AttackNum);

        if (BossHp <= 0)
        {
            ChangeState(BOSSSTATE.Die);
            yield break;

        }
        yield return new WaitForSeconds(1f);
        ChangeState(BOSSSTATE.Chase);
        // }

    }

    protected override IEnumerator Die()
    {

        BossAnimator.SetTrigger("Die");

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

}
