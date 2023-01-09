using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IHitable
{
    
    

   // protected string name;
    protected float hp;
    protected float atk;

    public virtual void Hit(float atk)//interface
    {

    }

    protected virtual IEnumerator Die()
    {
        yield return null;
    }

    protected virtual IEnumerator Attack()
    {
        yield return null;

    }

    protected virtual IEnumerator Chase()
    {
        yield return null;

    }



}
