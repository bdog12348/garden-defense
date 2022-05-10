using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySwatterWeapon : WeaponBase
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        AttackCooldown = .35f;
    }

    public override void WepAttack()
    {
        base.WepAttack();
        animator.SetTrigger("Attack");
    }

    public override bool CanAttackType(GameObject other)
    {
        EnemyType type = other.GetComponent<EnemyType>();
        if (type.GetEnemyType() == EnemyType.Type.Beetle)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
