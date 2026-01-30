using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FPSGame.Runtime.Inventory;

namespace FPSGame.Runtime.UI
{
    /// <summary>
    /// Envanterdeki bir kutucuðu (Slot) temsil eder.
    /// Ýkon, Adet ve Týklama iþlemlerini yönetir.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class InventoryItemUI : MonoBehaviour
    {
        #region Fields

        [Header("UI References")]
        [Tooltip("Eþyanýn ikonunun gösterileceði Image bileþeni.")]
        [SerializeField] private Image m_IconImage;

        [Tooltip("Eþya adedini gösteren sað alt köþe yazýsý.")]
        [SerializeField] private TextMeshProUGUI m_QuantityText;

        [Tooltip("Arkaplan veya çerçeve butonu.")]
        [SerializeField] private Button m_Button;

        private int m_SlotIndex;

        #endregion

        #region Events

        public event Action<int> OnItemClicked;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            m_Button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveListener(HandleClick);
        }

        #endregion

        #region Methods

        public void Initialize(InventoryController.InventorySlot slot, int index)
        {
            m_SlotIndex = index;

            // 1. Ýkon Ayarý
            if (m_IconImage != null)
            {
                if (slot.Data.Icon != null)
                {
                    m_IconImage.sprite = slot.Data.Icon;
                    m_IconImage.color = Color.white; // Görünür yap
                }
                else
                {
                    // Ýkon yoksa þeffaf yap veya default sprite koy
                    m_IconImage.color = Color.clear;
                }
            }

            // 2. Adet Ayarý (1 tane varsa sayýyý gizleyebiliriz, tercih senin)
            if (m_QuantityText != null)
            {
                if (slot.StackSize > 1)
                {
                    m_QuantityText.text = slot.StackSize.ToString();
                    m_QuantityText.gameObject.SetActive(true);
                }
                else
                {
                    // Tekli itemlarda sayý yazmasýn (daha temiz görünür)
                    m_QuantityText.text = "";
                    m_QuantityText.gameObject.SetActive(false);
                }
            }
        }

        private void HandleClick()
        {
            OnItemClicked?.Invoke(m_SlotIndex);
        }

        #endregion
    }
}