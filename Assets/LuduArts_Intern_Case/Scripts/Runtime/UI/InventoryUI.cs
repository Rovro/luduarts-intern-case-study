using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FPSGame.Runtime.Inventory;

namespace FPSGame.Runtime.UI
{
    /// <summary>
    /// Envanter arayüzünü yöneten sýnýf. 
    /// Açma/Kapama, Cursor yönetimi ve Eþya seçimi iþlemlerini yapar.
    /// </summary>
    public class InventoryUI : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private InventoryController m_InventoryController;
        [SerializeField] private InputManager m_InputManager;

        [Tooltip("Açýlýp kapanacak olan ana panel.")]
        [SerializeField] private GameObject m_InventoryWindow;

        [Tooltip("Liste elemanlarýnýn oluþturulacaðý Content objesi.")]
        [SerializeField] private Transform m_ItemContainer;

        [Tooltip("InventoryItemUI scripti içeren prefab.")]
        [SerializeField] private GameObject m_ItemPrefab;

        [Header("Selected Item Display")]
        [SerializeField] private TextMeshProUGUI m_CurrentItemText;

        private bool m_IsInventoryOpen;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Referanslarý otomatik bulmaya çalýþ (Inspector boþsa)
            if (m_InventoryController == null)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    m_InventoryController = player.GetComponent<InventoryController>();
                    m_InputManager = player.GetComponent<InputManager>();
                }
            }

            // Baþlangýçta kapalý olsun
            if (m_InventoryWindow != null)
            {
                m_InventoryWindow.SetActive(false);
            }

            m_IsInventoryOpen = false;
        }

        private void OnEnable()
        {
            if (m_InventoryController != null)
            {
                m_InventoryController.OnInventoryChanged += RefreshList;
                m_InventoryController.OnSelectionChanged += UpdateSelectionText;
            }
        }

        private void OnDisable()
        {
            if (m_InventoryController != null)
            {
                m_InventoryController.OnInventoryChanged -= RefreshList;
                m_InventoryController.OnSelectionChanged -= UpdateSelectionText;
            }
        }

        private void Update()
        {
            HandleInput();
        }

        #endregion

        #region Methods

        private void HandleInput()
        {
            if (m_InputManager != null && m_InputManager.InventoryTriggered)
            {
                ToggleInventory();
                m_InputManager.ConsumeInventoryInput();
            }
        }

        private void ToggleInventory()
        {
            m_IsInventoryOpen = !m_IsInventoryOpen;

            if (m_InventoryWindow != null)
            {
                m_InventoryWindow.SetActive(m_IsInventoryOpen);
            }

            // Cursor Yönetimi
            if (m_IsInventoryOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void RefreshList(List<InventoryController.InventorySlot> slots)
        {
            // Eski listeyi temizle
            foreach (Transform child in m_ItemContainer)
            {
                Destroy(child.gameObject);
            }

            // Yeni listeyi oluþtur
            for (int i = 0; i < slots.Count; i++)
            {
                GameObject obj = Instantiate(m_ItemPrefab, m_ItemContainer);

                // Item UI Scriptini al ve verileri doldur
                var itemUI = obj.GetComponent<InventoryItemUI>();
                if (itemUI != null)
                {
                    itemUI.Initialize(slots[i], i);

                    // Event'e abone ol (Butona týklanýnca ne olacak?)
                    itemUI.OnItemClicked += HandleItemClicked;
                }
            }
        }

        private void HandleItemClicked(int index)
        {
            // Controller'a "Bu slotu seç" emri ver
            if (m_InventoryController != null)
            {
                m_InventoryController.SelectSlot(index);
                Debug.Log($"InventoryUI: Slot {index} selected via click.");
            }
        }

        private void UpdateSelectionText(ItemData currentItem)
        {
            if (m_CurrentItemText == null) return;

            if (currentItem != null)
            {
                m_CurrentItemText.text = $"Hand: {currentItem.ItemName}";
            }
            else
            {
                m_CurrentItemText.text = "Hand: Empty";
            }
        }

        #endregion
    }
}