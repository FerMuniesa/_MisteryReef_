using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public static class EnemyActions
{
    public static Action<GameObject> OnDeath;
}

namespace GR
{
    public class EnemyStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        // TODO
        public int currentHealth = 0;
        public GameObject Slider;

        //  Rango de ataque de los enemigos
        public float attackRange;

        public int dmgAttack;

        public float cd;

        public float rangeVision;

        public Slider slider;

        Animator animator;



        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (slider != null)
            {
                slider.value = currentHealth;
            }
        }

        void Start()
        {
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel()
        {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth = currentHealth - damage;

            animator.Play("Damage");

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                animator.Play("Death");
                Invoke("Desactivar", 2);
                EnemyActions.OnDeath(gameObject);
                //HANDLE PLAYER DEATH
            }
        }

        void Desactivar()
        {
            Slider.SetActive(false);
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
