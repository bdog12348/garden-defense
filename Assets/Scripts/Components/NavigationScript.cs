using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationScript : MonoBehaviour
{
    [SerializeField] private Transform target;

    public UnityEvent ReachedDestination;

    private NavMeshAgent agent;

    bool destinationReached = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;

        if (ReachedDestination == null)
            ReachedDestination = new UnityEvent();
    }

    private void Update()
    {
        if (agent.remainingDistance <= .5f && !destinationReached)
        {
            destinationReached = true;
            ReachedDestination.Invoke();
        }else if (agent.remainingDistance >= 1f)
        {
            destinationReached = false;
        }
    }

    public void AssignNewTarget(Transform newTarget)
    {
        target = newTarget;
        agent.destination = target.position;
    }

    public void AssignNewDestination(Vector3 newTargetPosition)
    {
        agent.destination = newTargetPosition;
    }

    public void AssignTargetDestination()
    {
        if (target != null)
        {
            agent.destination = target.position;
        }
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public void GetGardenTarget()
    {
        target = GameObject.Find("Garden").transform;
    }
}
