using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayWeapon : WeaponBase
{
    public override bool CanAttackType(GameObject other)
    {
        EnemyType type = other.GetComponent<EnemyType>();
        if (type.GetEnemyType() == EnemyType.Type.Raccoon)
        {
            return true;
        }else
        {
            return false;
        }
    }

    //Animator animator;

    //private void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}

    public override void WepAttack()
    {
        base.WepAttack();
    }
}
