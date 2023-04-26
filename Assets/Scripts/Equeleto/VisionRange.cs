using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
    [SerializeField]
    private EnemySenses senses;


    public void SetUp(float rangeVision)
    {
        GetComponent<SphereCollider>().radius = rangeVision;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Te vi enemigo");
            senses.ISeeEnemy(other.gameObject);
        }
    }
}
