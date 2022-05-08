using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPickup : MonoBehaviour
{
    [SerializeField] private LayerMask PickupMask;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Transform d;
    [Space]
    [SerializeField] private GameObject hitbox;
    [SerializeField] private Transform hitboxLocation;
    [SerializeField] StarterAssets.StarterAssetsInputs inputs;
    [Space]
    [SerializeField] private float PickupRange;

    private Rigidbody CurrentObject;

    void Update() 
    {
        if (inputs.pickup) 
        {
            Debug.Log("E Pressed");
            if (CurrentObject)  // if already holding a weapon, drop it
            {
                //hitbox = CurrentObject.GameObject.Find("Hitbox");
                CurrentObject.useGravity = true;
                CurrentObject.freezeRotation = false;
                CurrentObject.transform.rotation = Quaternion.identity;
                Debug.Log("Putting Down Current Wep");
                inputs.drop = false;
                /*hitbox.transform.parent = transform;
                hitbox.transform.position = Vector3.zero;
                hitbox.transform.rotation = Quaternion.identity;*/
                CurrentObject = null;
                return;
            }
            Ray CameraRay = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(CameraRay, out RaycastHit HitInfo, PickupRange, PickupMask)) 
            {
                CurrentObject = HitInfo.rigidbody;
                CurrentObject.useGravity = false;
            }
        }
    }
}
