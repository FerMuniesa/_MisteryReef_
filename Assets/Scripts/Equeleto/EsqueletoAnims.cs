using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoAnims : MonoBehaviour
{

    [SerializeField]
    private EnemySenses senses;

    /// <summary>
    /// Una llamada al metodo de hacer da�o en el frame de da�o
    /// </summary>
    public void OnAttackEvent()
    {
        senses.Attack();
    }
}
