using MoreMountains.Feedbacks;
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


    public MMFeedbacks killFeedback;

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
        Debug.Log($"Took damage {amount}");

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
        Debug.Log($"{gameObject.name} died");
        //Instantiate(HitParticle, new Vector3(other.transform.position.x,
        //transform.position.y, other.transform.position.z), other.transform.rotation);
        //killFeedback.PlayFeedbacks(transform.position);
        Destroy(gameObject);
    }
}
