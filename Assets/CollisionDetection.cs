using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    //public GameObject HitParticle;

    void onTriggerEnter(Collider other) 
    {
        if (other.tag == "Enemy" && wc.isAttacking) 
        {
            Debug.Log(other.name); //see what enemy we're hitting
            //Instantiate(HitParticle, new Vector3(other.transform.position.x,
            //transform.position.y, other.transform.position.z), other.transform.rotation);
            Destroy(other);
        }
    }

}
