using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform d;
    
    void Update() 
    {
        if (Input.GetButtonDown("Pickup"))  //E
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            this.transform.position = d.position;
            this.transform.parent = GameObject.Find("WepSlot").transform;
            Debug.Log("PickingUp");
        }

        if (Input.GetButtonDown("Throw"))  //G
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            Debug.Log("Putting Down");
        }
    }
}
