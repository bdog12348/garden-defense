using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform d;
    public GameObject hitbox;
    public Transform hitboxLocation;
    [SerializeField] StarterAssets.StarterAssetsInputs inputs;

    private Transform prevWep;
    
    void Update() 
    {
        if (inputs.pickup) 
        {
            if (this.transform.parent != null) // if currently holding, drop current wep
            {
                prevWep = this.transform;
                drop();
            }

            //pickup another wep
            if(this.transform != prevWep)
                pickup();
        }
    }

    void pickup() 
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = true;
        this.transform.position = d.position;
        this.transform.parent = GameObject.Find("WepSlot").transform;
        Debug.Log("PickingUp");
        inputs.pickup = false;
        hitbox.transform.parent = hitboxLocation;
        hitbox.transform.position = hitboxLocation.position;
        hitbox.transform.rotation = hitboxLocation.rotation;
        transform.rotation = d.rotation;
    }

    void drop() 
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().freezeRotation = false;
        Debug.Log("Putting Down");
        inputs.drop = false;
        hitbox.transform.parent = transform;
        hitbox.transform.position = Vector3.zero;
        hitbox.transform.rotation = Quaternion.identity;
        transform.rotation = Quaternion.identity;
    }
}
