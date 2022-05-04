using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public GameObject wep;
    protected bool CanAttack = true;
    protected float AttackCooldown = 1.0f;
    protected bool isAttacking = false;

    protected float Damage;

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

     //add animation and audio here later
    public abstract void WepAttack();

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;
        CanAttack = true;
    }
    protected float attackCooldown;

}
