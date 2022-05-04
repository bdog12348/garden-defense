using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    //public GameObject HitParticle;

    public MMFeedbacks killFeedback;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered zone");
        if (other.tag == "Enemy" && wc.isAttacking)
        {
            Debug.Log(transform.parent);
            Debug.Log(other.name); //see what enemy we're hitting
            //Instantiate(HitParticle, new Vector3(other.transform.position.x,
            //transform.position.y, other.transform.position.z), other.transform.rotation);
            killFeedback.PlayFeedbacks(other.transform.position);
            Destroy(other);
        }
    }

}
