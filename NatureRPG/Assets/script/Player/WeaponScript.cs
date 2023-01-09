using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponScript : MonoBehaviour
{
   
    public float Atk;

    private void OnTriggerEnter(Collider other)
    {
       
        //if(other.gameObject.layer == 8)//몬스터레이어와 충돌시
        //{
        //    Debug.Log(other.name + "과 무기가 충돌");
        //}
        if(other.gameObject.layer == 8)
        {
            IHitable HitMonster = other.GetComponent<IHitable>();
            if (HitMonster != null)
            {

                HitMonster.Hit(Atk);

                Debug.Log(Atk + "의 대미지로 적을 때린다");
            }
        }
        
        

    }

}
