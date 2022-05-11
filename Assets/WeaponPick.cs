using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPick : MonoBehaviour
{
    public Transform equipPosition;
    public float distance = 10f;
    GameObject currentWep;
    GameObject wp;

    Vector3 weaponBaseLocation;
    Quaternion weaponBaseRotation;
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
                Debug.Log($"Grabbable, {hit.transform.name}");
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
        //Set new current weapon
        currentWep = wp;

        //Make it so that raycast will not hit currently held weapon
        SetLayerRecursively(currentWep, LayerMask.NameToLayer("Ignore Raycast"));

        /*
            Enable weaponbase script of whatever weapon we are holding
            Every weapon should have their own version of a weapon script, even if its just empty
            Currently every weapon controls themselves for attacking, might want to change that to being controlled in the player controls later
        */
        WeaponBase weaponBase = currentWep.GetComponent<WeaponBase>();
        if (weaponBase != null)
        {
            weaponBase.enabled = true;
        }

        //Store location of where we grabbed the weapon to put it back when we drop it
        weaponBaseLocation = currentWep.transform.position;
        weaponBaseRotation = currentWep.transform.rotation;

        //currentWep.GetComponent<Rigidbody>().useGravity = false;
        //currentWep.GetComponent<Rigidbody>().freezeRotation = true;

        //Move weapon to location of player
        currentWep.transform.parent = equipPosition;
        currentWep.transform.position = equipPosition.position;
        currentWep.transform.rotation = equipPosition.rotation;
    }

    private void drop() 
    {
        //Put weapon back on layer where we can raycast it 
        SetLayerRecursively(currentWep, LayerMask.NameToLayer("Pickupable"));

        //Disable weaponbase script so it doesn't attack while we aren't holding it
        WeaponBase weaponBase = currentWep.GetComponent<WeaponBase>();
        if (weaponBase != null)
        {
            weaponBase.enabled = false;
        }

        //Commented out rigidbody stuff since we just will not use gravity on weapons
        //currentWep.GetComponent<Rigidbody>().useGravity = true;
        //currentWep.GetComponent<Rigidbody>().freezeRotation = false;
        //currentWep.GetComponent<Rigidbody>().isKinematic = false;
        
        //Move weapon back to its original location and unparent
        currentWep.transform.parent = null;
        currentWep.transform.rotation = weaponBaseRotation;
        currentWep.transform.position = weaponBaseLocation;

        //Reset currentWep
        currentWep = null;
    }

    void SetLayerRecursively(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach(Transform t in gameObject.transform)
        {
            SetLayerRecursively(t.gameObject, layer);
        }
    }
}