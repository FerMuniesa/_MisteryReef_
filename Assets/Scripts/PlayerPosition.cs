using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField]
    private float rangoDistance;

    [SerializeField]
    private float rayDistance;


    private void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 dir = -transform.up;
        float distance = rayDistance;
        int layer = (1 << 11);
        if (Physics.Raycast(transform.position, dir, out hit, distance, layer, QueryTriggerInteraction.Collide))
        {
            float d = Vector3.Distance(transform.position, hit.point);
            if (d > rangoDistance)
            {
                transform.position = hit.point;
            }
        }
    }
}
