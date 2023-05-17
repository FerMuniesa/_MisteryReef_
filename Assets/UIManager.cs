using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public static class UIActions
{
    public static Action<string> ShowMsg;
    public static Action EraseMsg;
    public static Action ActiveQuickSlot;
}

namespace GR
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        EquipmentWindowUI equipmentWindowUI;

        [Header("UI Windows")]
        [Tooltip("Referencia al hud padre")]
        public GameObject hudWindow;
        [Tooltip("Submenu del player")]
        public GameObject selectWindow;
        [Tooltip("Panel con el inventario del player")]
        public GameObject weaponInventoryWindow;

        [SerializeField]
        public GameObject textPanelNPC;

        [SerializeField]
        public TextMeshProUGUI textName;

        [SerializeField]
        public TextMeshProUGUI textText;

        [Header("Weapon Inventory")]
        public GameObject weaponInventorySlotPrefab;
        public Transform weaponInventorySlotsParent;
        //  Inventario de las armas desde el player
        WeaponInventorySlot[] weaponInventorySlots;

        [Header("Texto info")]
        [SerializeField]
        private GameObject textPanel;

        [SerializeField]
        private GameObject quickSlotPanel;

        [SerializeField]
        private TextMeshProUGUI infoText;

        private void OnEnable()
        {
            UIActions.ShowMsg += ShowMessage;
            UIActions.EraseMsg += EraseMessage;
        }

        private void OnDisable()
        {
            UIActions.ShowMsg -= ShowMessage;
            UIActions.EraseMsg -= EraseMessage;
        }

        private void Awake()
        {
            equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
        }

        private void Start()
        {
            weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        }

        /// <summary>
        /// Crea los slots del inventario
        /// </summary>
        public void UpdateUI()
        {
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if (i < playerInventory.weaponsInventory.Count)
                {
                    GameObject cuadrado = null;
                    if (weaponInventorySlots.Length < playerInventory.weaponsInventory.Count)
                    {
                        //  Cuadrado del inventario
                        cuadrado = Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
                    WeaponItem weaponItem = playerInventory.weaponsInventory[i];
                    WeaponInventorySlot slot = weaponInventorySlots[i];
                    //  Añadir el callback
                    weaponInventorySlots[i].GetComponentInChildren<Button>().onClick.AddListener(
                        delegate () 
                        {
                            InventoryActions.EquipRightHandWeaponItem(weaponItem);
                            quickSlotPanel.GetComponent<QuickSlotsUI>().UpdateWeaponQuickSlotsUI(false, weaponItem);
                            slot.ClearInventorySlot();
                            print("click"); 
                        });
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
        }

        /// <summary>
        /// Quita un cuadrado de un arma del inventario
        /// </summary>
        private void RemoveWeaponInventory()
        {

        }

        public void ShowInteractuableMessage(string _name, string _text)
        {
            textPanelNPC.SetActive(true);
            textName.text = _name;
            textText.text = _text;
        }

        public void CloseInteractuableMessage()
        {
            textPanelNPC.SetActive(false);

        }

        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
            quickSlotPanel.SetActive(true);
        }

        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
            quickSlotPanel.SetActive(false);
        }

        public void CloseAllInventoryWindows()
        {
            weaponInventoryWindow.SetActive(false);
        }

        /// <summary>
        /// Muestra un texto informativo
        /// </summary>
        private void ShowMessage(string msg)
        {
            textPanel.SetActive(true);
            infoText.text = msg;
        }

        private void EraseMessage()
        {
            textPanel.SetActive(false);
            infoText.text = "";
        }
    }
}