using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaccoonEnemy : MonoBehaviour
{
    NavigationScript navigation;
    NavMeshAgent agent;

    [SerializeField]
    float runAwaySpeed = 3f;
    [SerializeField]
    float normalSpeed = 2f;
    [SerializeField]
    float runAwayDistance = 4f;

    bool PlayerInRange = false;
    bool runningAway = false;

    // Start is called before the first frame update
    void Start()
    {
        navigation = GetComponent<NavigationScript>();
        agent = GetComponent<NavMeshAgent>();

        navigation.GetGardenTarget();
        agent.speed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInRange && !runningAway)
        {
            navigation.AssignNewDestination(RandomPointOnCircleEdge(runAwayDistance));
            navigation.ReachedDestination.AddListener(GetNormalTarget);
            runningAway = true;
            agent.speed = runAwaySpeed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }

    Vector3 RandomPointOnCircleEdge(float radius)
    {
        float angle = Random.Range(0, 2f * Mathf.PI);
        Vector3 returnPoint = transform.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        Debug.Log($"Returning {returnPoint.x},{returnPoint.y},{returnPoint.z}");
        return returnPoint;
    }

    void GetNormalTarget()
    {
        navigation.AssignTargetDestination();
        navigation.ReachedDestination.RemoveListener(GetNormalTarget);
        runningAway = false;
        agent.speed = normalSpeed;
    }
}
