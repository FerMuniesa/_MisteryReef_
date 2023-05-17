using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GR
{
    public static class InteractuableActions
    {
        public static Action Interactuate;
    }


    public class InteractuableText : MonoBehaviour
    {
        [SerializeField]
        private string nameInteractuable;

        [SerializeField]
        private string[] textos;

        [SerializeField]
        private UIManager uIManager;

        private int index = 0;

        private void OnEnable()
        {
            InteractuableActions.Interactuate += NextText;
        }

        private void OnDisable()
        {
            InteractuableActions.Interactuate -= NextText;
        }



        private void NextText()
        {
            index++;
            if (index >= textos.Length)
            {
                CloseInteractuble();
                Destroy(gameObject);
            }
            else
            {
                UpdateText();

            }
        }

        private void CloseInteractuble()
        {
            index = 0;
            uIManager.CloseInteractuableMessage();
        }


        private void UpdateText()
        {
            uIManager.ShowInteractuableMessage(nameInteractuable, textos[index]);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                uIManager.ShowInteractuableMessage(nameInteractuable, textos[index]);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                uIManager.CloseInteractuableMessage();
            }
        }
    }
}
