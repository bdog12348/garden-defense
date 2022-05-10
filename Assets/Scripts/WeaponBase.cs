using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    //public GameObject wep;
    public bool isAttacking = false;

    protected bool CanAttack = true;
    protected float AttackCooldown = 1.0f;
    [SerializeField] protected float Damage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 = left click, 1 = right click
        {
            if (CanAttack)
            {
                WepAttack();
                StartCoroutine(ResetAttackCooldown());
            }
        }
    }

    public float GetDamage()
    {
        return Damage;
    }

     //add animation and audio here later
    public virtual void WepAttack()
    {
        isAttacking = true;
        CanAttack = false;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        isAttacking = false;
        CanAttack = true;
    }

    public abstract bool CanAttackType(GameObject other);
}
