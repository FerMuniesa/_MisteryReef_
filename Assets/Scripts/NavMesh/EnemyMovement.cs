using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum MOVEMENT_STATE : int
{
    MOVING,WAITING
}


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Transform destinationPos;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private int StopDistance = 1;

    private bool onTarget = false;

    [SerializeField]
    private AnimatorHandler animatorHandler;

    [SerializeField]
    private float rangoDistance;

    [SerializeField]
    private float speedLimit;

    private bool moving = false;

    private MOVEMENT_STATE mOVEMENT_STATE;


    public MOVEMENT_STATE GetState()
    {
        return mOVEMENT_STATE;
    }


    public bool SetDestination(Transform destination, float rango)
    {
        agent.isStopped = false;
        destinationPos = destination;
        moving = true;
        agent.stoppingDistance = rango;
        var status = agent.SetDestination(destination.position);
        mOVEMENT_STATE = status ? MOVEMENT_STATE.MOVING : MOVEMENT_STATE.WAITING;
        return status;
    }

    private void Update()
    {
        if (agent.hasPath && agent.velocity.magnitude > 0.0f)
        {
            animatorHandler.gameObject.GetComponent<Animator>().SetFloat("Vertical", agent.velocity.magnitude > 0.0f ? 1.0f : 0.0f);
        }
        else
        {
            animatorHandler.gameObject.GetComponent<Animator>().SetFloat("Vertical", 0.0f);
            mOVEMENT_STATE = MOVEMENT_STATE.WAITING;
        }
        //if (moving)
        //{
        //    float distance = Vector3.Distance(transform.position, destinationPos.position);
        //    if (distance > StopDistance)
        //    {
        //        agent.SetDestination(destinationPos.position);
        //        agent.stoppingDistance = StopDistance;
        //    }
        //}

        //animatorHandler.UpdateAnimatorValues(agent.velocity.magnitude > 0 ? 1 : 0, 0, true);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 dir = -transform.up;
        float distance = 5.0f;
        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            float d = Vector3.Distance(transform.position,hit.point);
            if (d > rangoDistance)
            {
                transform.position = hit.point;
            }
        }
    }

    public void Stop()
    {
        mOVEMENT_STATE = MOVEMENT_STATE.WAITING;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }
}
