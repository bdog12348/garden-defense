using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public GameObject wep;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;
    public bool isAttacking = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 = left click, 1 = right click
        {
            if (CanAttack) 
            {
                WepAttack();
            }
        }
    }

    public void WepAttack()
    {
        isAttacking = true;
        CanAttack = false;
        StartCoroutine(ResetAttackCooldown());
        Debug.Log("ATTACKING");
    }

    IEnumerator ResetAttackCooldown() 
    {
        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;
        CanAttack = true;
    }
}
