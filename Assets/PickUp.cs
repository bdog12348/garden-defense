using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform d;
    [SerializeField] StarterAssets.StarterAssetsInputs inputs;
    
    void Update() 
    {
        if (inputs.pickup)  //E
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().freezeRotation = true;
            this.transform.position = d.position;
            this.transform.parent = GameObject.Find("WepSlot").transform;
            Debug.Log("PickingUp");
            inputs.pickup = false;
        }

        if (inputs.drop)  //G
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().freezeRotation = false;
            Debug.Log("Putting Down");
            inputs.drop = false;
        }
    }
}
