using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GR
{
    public class Interactable : MonoBehaviour
    {
        public float radius = 0.6f;
        public string interactbleText;
        internal string interactableText;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radius);
        }

        public virtual void Interact(PlayerManager playerManager)
        {
            Debug.Log("You interacted with an object!");
        }
    }
}