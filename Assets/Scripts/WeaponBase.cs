using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    //public GameObject wep;
    CrosshairController crosshair;
    public bool isAttacking = false;

    protected bool CanAttack = true;
    protected float AttackCooldown = 1.0f;
    [SerializeField] protected float MaxDistance;
    [SerializeField] protected float Damage;
    bool enemyInRange = false;

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

        if (enemyInRange)
        {
            crosshair.SetActiveState(true);
        }else
        {
            crosshair.SetActiveState(false);
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, MaxDistance))
        {
            if (hit.transform.tag == "Enemy")
            {
                if (CanAttackType(hit.transform.gameObject))
                {
                    enemyInRange = true;
                }else
                {
                    enemyInRange = false;
                }
            }
        }
        else
        {
            enemyInRange = false;
        }
    }

    public void SetCrosshairController(CrosshairController crosshairController)
    {
        crosshair = crosshairController;
    }

    public void Disable()
    {
        crosshair = null;
        enabled = false;
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
