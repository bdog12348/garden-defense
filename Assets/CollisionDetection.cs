using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CollisionDetection : MonoBehaviour
{
    //public GameObject HitParticle;

    public WeaponBase wc;

    bool tookDamage = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" && wc.isAttacking && !tookDamage)
        {
            Debug.Log(transform.parent);
            Debug.Log(other.name); //see what enemy we're hitting
            other.GetComponent<HealthScript>().TakeDamage(wc.GetDamage());
            tookDamage = true;
        }else if (wc.isAttacking == false)
        {
            tookDamage = false;
        }
    }

}
