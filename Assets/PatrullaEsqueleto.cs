using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaEsqueleto : StateMachineBehaviour
{
    public Transform player;
    ContenedorEsqueleto contenedorEsqueleto;
    UnityEngine.AI.NavMeshAgent puppet;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        puppet = animator.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        contenedorEsqueleto = animator.gameObject.GetComponent<ContenedorEsqueleto>();
        contenedorEsqueleto.maxPosition = contenedorEsqueleto.destination.Length - 1;

    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        puppet.speed = 2f;
        puppet.destination = contenedorEsqueleto.destination[contenedorEsqueleto.nextPosition].position;
        if (Vector3.Distance(puppet.transform.position, contenedorEsqueleto.destination[contenedorEsqueleto.nextPosition].position) <= 2f)
        {
            if (contenedorEsqueleto.nextPosition >= contenedorEsqueleto.maxPosition)
            {
                contenedorEsqueleto.nextPosition = 0;
            }
            else
            {
                contenedorEsqueleto.nextPosition++;
            }
            puppet.destination = contenedorEsqueleto.destination[contenedorEsqueleto.nextPosition].position;
        }


        if (Vector3.Distance(puppet.transform.position, player.position) <= 4f)
        {
            animator.SetTrigger("Navigation");

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }
}
