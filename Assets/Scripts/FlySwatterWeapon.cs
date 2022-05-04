using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlySwatterWeapon : WeaponBase
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void WepAttack()
    {
        base.WepAttack();
        animator.SetTrigger("Attack");
    }
}
