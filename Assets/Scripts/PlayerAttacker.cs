using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GR
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        PlayerStats playerStats;
        public string lastAttack;

        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            inputHandler = GetComponent<InputHandler>();
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag && playerStats.GetStamina() >= weapon.baseStamina)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.oh_light_attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.oh_light_attack_02, true);
                }
            }
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            if (playerStats.GetStamina() >= weapon.baseStamina)
            {
                weaponSlotManager.attackingWeapon = weapon;
                animatorHandler.PlayTargetAnimation(weapon.oh_light_attack_01, true);
                lastAttack = weapon.oh_light_attack_01;
            }
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            if (playerStats.GetStamina() >= weapon.baseStamina)
            {
                weaponSlotManager.attackingWeapon = weapon;
                animatorHandler.PlayTargetAnimation(weapon.oh_light_attack_01, true);
                lastAttack = weapon.oh_light_attack_01;
            }
        }
    }
}
