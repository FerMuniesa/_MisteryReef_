using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    [SerializeField]
    private Transform[] puntosDePatrulla;


    public Transform DamePuntoPatrulla()
    {
        return puntosDePatrulla[Random.Range(0, puntosDePatrulla.Length)];
    }
}
