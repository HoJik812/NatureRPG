using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponScript : MonoBehaviour
{
   
    public float Atk;

    private void OnTriggerEnter(Collider other)
    {
       
        //if(other.gameObject.layer == 8)//���ͷ��̾�� �浹��
        //{
        //    Debug.Log(other.name + "�� ���Ⱑ �浹");
        //}
        if(other.gameObject.layer == 8)
        {
            IHitable HitMonster = other.GetComponent<IHitable>();
            if (HitMonster != null)
            {

                HitMonster.Hit(Atk);

                Debug.Log(Atk + "�� ������� ���� ������");
            }
        }
        
        

    }

}
