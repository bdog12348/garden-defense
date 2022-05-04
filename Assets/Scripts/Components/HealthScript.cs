using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthScript : MonoBehaviour
{
    [Header("Health Variables")]
    float health;
    [SerializeField] float maxHealth = 10f;

    [Header("Events")]
    public UnityEvent Died;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        if (Died == null)
            Died = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float amount)
    {
        if (amount < 0f)
        {
            Debug.LogError("Damage must be greater than 0");
            return;
        }

        health -= amount;
    }

    public void GiveHealth(float amount)
    {
        if (amount < 0f)
        {
            Debug.LogError("Healing amount must be greater than 0");
            return;
        }

        health += amount;

        if (health > maxHealth)
            health = maxHealth;
    }

    public void Die()
    {
        Died.Invoke();
        Destroy(gameObject);
    }
}
