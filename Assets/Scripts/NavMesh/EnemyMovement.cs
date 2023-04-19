using GR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerPosition;


    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private int StopDistance = 1;

    private bool onTarget = false;

    [SerializeField]
    private AnimatorHandler animatorHandler;



    private void Update()
    {
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        if (distance > StopDistance)
        {
            agent.SetDestination(playerPosition.position);
            agent.stoppingDistance = StopDistance;
        }
        animatorHandler.gameObject.GetComponent<Animator>().SetFloat("Vertical", agent.velocity.magnitude > 0 ? 1 : 0);

        //animatorHandler.UpdateAnimatorValues(agent.velocity.magnitude > 0 ? 1 : 0, 0, true);
    }
}
