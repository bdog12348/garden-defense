using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavigationScript : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }

    public void AssignNewTarget(Transform newTarget)
    {
        target = newTarget;
        agent.destination = target.position;
    }
}
