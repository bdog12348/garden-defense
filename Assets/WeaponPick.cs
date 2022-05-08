using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPick : MonoBehaviour
{
    public Transform equipPosition;
    public float distance = 10f;
    GameObject currentWep;
    GameObject wp;

    bool canGrab;

    void Update()
    {
        CheckWeapons();

        if (canGrab) 
        {
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                Debug.Log("Pressing E");
                if (currentWep != null)
                    drop();
                pickup();
            }
        }

        if (currentWep != null) 
        {
            if (Input.GetKeyDown(KeyCode.G))
                drop();
        }
    }

    private void CheckWeapons() 
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distance))
        {
            if (hit.transform.tag == "Weapon")
            {
                Debug.Log("Grabbable");
                canGrab = true;
                wp = hit.transform.gameObject;
            }
        }
        else 
        {
            canGrab = false;
        }
    }

    private void pickup() 
    {
        currentWep = wp;
        currentWep.GetComponent<Rigidbody>().useGravity = false;
        currentWep.GetComponent<Rigidbody>().freezeRotation = true;

        currentWep.transform.position = equipPosition.position;
        currentWep.transform.parent = equipPosition;
        currentWep.transform.rotation = equipPosition.rotation;
    }

    private void drop() 
    {
        currentWep.GetComponent<Rigidbody>().useGravity = true;
        currentWep.GetComponent<Rigidbody>().freezeRotation = false;

        currentWep.transform.parent = null;
        currentWep.GetComponent<Rigidbody>().isKinematic = false;
        currentWep = null;
    }
}
