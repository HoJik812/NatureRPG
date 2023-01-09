using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMonsterAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            IHitable HitPlayer = other.GetComponent<IHitable>();
            if (HitPlayer != null)
            {
                HitPlayer.Hit(30f);
                Debug.Log("Player ¶§¸²");
            }
        }

    }
}
