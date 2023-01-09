using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player: MonoBehaviour, IHitable
{
    public GameObject HitEffect;
    public Transform HitPoint;
    public GameObject healEffect;
    public GameObject levelupEffect;
    public Transform effectPoint;
    public Collider Weapon;
    public float MAXHP = 500f;
    public float MAXMP = 300f;
    public float MAXEXP = 100f;
    [SerializeField]
    private float PlayerExp = 0;
    [SerializeField]
    private float Playerhp = 10f;
    [SerializeField]
    private float PlayerMp = 10f;
    [SerializeField]
    private int PlayerLevel = 1;
    public float Hp
    {
        get { return Playerhp; }
        set
        {
            Playerhp = value;

            if (Playerhp > MAXHP)
            {
                Playerhp = MAXHP;
            }
            //Debug.Log("PlayerHp = " + Playerhp);
            if (Playerhp <= 0)
            {
                PlayerAnimator.SetTrigger("Die");
            }
        }

    }
    public float Mp
    {
        get { return PlayerMp; }
        set
        {
            PlayerMp = value;

            if (PlayerMp > MAXMP)
            {
                PlayerMp = MAXMP;
            }
        }
    }

    
    public float Exp
    {
        get { return PlayerExp; }
        set
        {
            PlayerExp = value;
            if(PlayerExp >= MAXEXP)
            {
                Level++;
            }
        }
    }
    public int Level
    {
        get { return PlayerLevel; }
        set
        {
            if (PlayerLevel < value)
            {
                PlayerLevel = value;
                Instantiate(levelupEffect, effectPoint.position, transform.rotation);
                MAXHP += 20f;
                MAXMP += 20f;
                PlayerExp = 0f;
            }
        }
    }
    private Animator PlayerAnimator;
    
    // Start is called before the first frame update
    void Start()
    {
        Hp = MAXHP;
        Mp = MAXMP;
        Exp = 0f;
        Level = 1;
        
    }

    private void Awake()
    {
        PlayerAnimator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (!(Playerhp == MAXHP) || !(PlayerMp == MAXMP))
            {
                Instantiate(healEffect, effectPoint.position, transform.rotation);
                Debug.Log("회복");
                Hp += MAXHP / 2;
                Mp += MAXMP / 2;
            }
            
        }
    }

    public void Hit(float damage)
    {
        //Debug.Log("Player 맞음");
        Hp -= damage;
    }

    public void ColliderOn()
    {
        Debug.Log("collider on");
        Weapon.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name +"과 trigger충돌");//몬스터 레이어에게 피격당함.
        if(other.gameObject.layer == 9)//몬스터의 공격레이어와 충돌시  콜라이더 off => 불필요한 충돌 방지
        {
            other.enabled = false;
            Instantiate(HitEffect, HitPoint.position, transform.rotation);
        }
        
    }
}
