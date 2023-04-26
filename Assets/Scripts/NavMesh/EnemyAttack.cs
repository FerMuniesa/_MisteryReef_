using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    private EnemyStats myStats;

    //  Referencia a la posición del enemigo (player)
    private Transform enemyTr;


    private void Start()
    {
        //  Conseguir la referencia a los stats de la entidad
        myStats = GetComponent<EnemyStats>();   
    }

    public void Attack()
    {
        RaycastHit hit;
        //  Pos inicial de donde sale el rayo
        Vector3 InitPos = transform.position;
        //  Buscar la dirección del enemigo (Si tiene) -> de otro modo ataca hacia adelante
        Vector3 dir = enemyTr == null ? transform.forward : Vector3.Normalize(enemyTr.position - transform.position);
        // Rango de ataque
        float attackDistance = myStats.attackRange;

        if (Physics.Raycast(InitPos,dir,out hit,attackDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                int enemyDmg = myStats.dmgAttack;
                hit.collider.gameObject.GetComponent<PlayerStats>().TakeDamage(enemyDmg);
            }
        }
    }
}
