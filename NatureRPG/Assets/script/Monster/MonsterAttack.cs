using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            IHitable HitPlayer = other.GetComponent<IHitable>();
            if (HitPlayer != null)
            {
                HitPlayer.Hit(20f);
                Debug.Log("Player ¶§¸²");
            }
        }
        
    }
}
