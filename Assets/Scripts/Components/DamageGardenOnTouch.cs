using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageGardenOnTouch : MonoBehaviour
{
    [SerializeField] float damage = 10f;

    [SerializeField] bool destroyOnTouch = true;

    public float Damage { get => damage; set => damage = value; }

    [System.Serializable]
    public class DamageGardenEvent : UnityEvent<float>
    {
    }

    public DamageGardenEvent collidedWithGarden;

    // Start is called before the first frame update
    void Start()
    {
        if (collidedWithGarden == null)
            collidedWithGarden = new DamageGardenEvent();

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        collidedWithGarden.AddListener(gameManager.TakeGardenDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Garden"))
        {
            collidedWithGarden.Invoke(Damage);
        }
            if (destroyOnTouch)
                Destroy(gameObject);
    }
}
