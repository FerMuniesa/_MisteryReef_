using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsqueletoAnims : MonoBehaviour
{

    [SerializeField]
    private EnemySenses senses;

    /// <summary>
    /// Una llamada al metodo de hacer daño en el frame de daño
    /// </summary>
    public void OnAttackEvent()
    {
        senses.Attack();
    }
}
