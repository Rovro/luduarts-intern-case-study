using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame.Runtime.Inventory
{
    /// <summary>
    /// Oyuncunun envanter mantýðýný ve fiziksel eþya gösterimini yöneten sýnýf.
    /// </summary>
    public class InventoryController : MonoBehaviour
    {
        #region Nested Types

        [Serializable]
        public class InventorySlot
        {
            public ItemData Data;
            public int StackSize;
            // Bu slota karþýlýk gelen fiziksel objelerin listesi
            // (Stack olduðu için birden fazla olabilir, biz sadece birini göstereceðiz)
            public List<GameObject> PhysicalObjects = new List<GameObject>();

            public InventorySlot(ItemData data, int amount)
            {
                Data = data;
                StackSize = amount;
            }
        }

        #endregion

        #region Fields

        [Header("Settings")]
        [SerializeField] private int m_MaxSlots = 10;

        [Header("References")]
        [Tooltip("Toplanan eþyalarýn saklandýðý görünmez depo (Sýrt çantasý gibi).")]
        [SerializeField] private Transform m_InventoryContainer;

        [Tooltip("Seçili eþyanýn eldeki pozisyonu.")]
        [SerializeField] private Transform m_CurrentObjectHandle;

        // Private fields
        private List<InventorySlot> m_Slots;
        private int m_SelectedSlotIndex = 0;
        private GameObject m_CurrentVisualObject;

        #endregion

        #region Events

        public event Action<List<InventorySlot>> OnInventoryChanged;
        public event Action<ItemData> OnSelectionChanged;

        #endregion

        #region Properties

        public ItemData CurrentItem
        {
            get
            {
                if (m_Slots.Count == 0 || m_SelectedSlotIndex >= m_Slots.Count)
                    return null;
                return m_Slots[m_SelectedSlotIndex].Data;
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_Slots = new List<InventorySlot>();

            if (m_InventoryContainer == null || m_CurrentObjectHandle == null)
            {
                Debug.LogError("Inventory Container or Handle references are missing!");
                enabled = false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fiziksel nesneyi envantere ekler.
        /// </summary>
        public bool AddItem(ItemData itemData, GameObject physicalObject)
        {
            // 1. Önce var olan stack'e eklemeye çalýþ
            foreach (var slot in m_Slots)
            {
                if (slot.Data == itemData && slot.StackSize < itemData.MaxStackSize)
                {
                    slot.StackSize++;
                    slot.PhysicalObjects.Add(physicalObject);

                    // Nesneyi depoya kaldýr
                    StorePhysicalObject(physicalObject);

                    NotifyChanges();
                    return true;
                }
            }

            // 2. Yeni slot aç
            if (m_Slots.Count >= m_MaxSlots)
            {
                Debug.LogWarning("Inventory is full!");
                return false;
            }

            var newSlot = new InventorySlot(itemData, 1);
            newSlot.PhysicalObjects.Add(physicalObject);
            m_Slots.Add(newSlot);

            // Nesneyi depoya kaldýr
            StorePhysicalObject(physicalObject);

            NotifyChanges();

            // Ýlk eþyaysa veya þu an elimiz boþsa, yeni geleni eline al
            if (m_Slots.Count == 1 || m_CurrentVisualObject == null)
            {
                SelectSlot(m_Slots.Count - 1);
            }

            return true;
        }

        /// <summary>
        /// Bir sonraki eþyayý seçer (Scroll ile kullaným için).
        /// </summary>
        public void SelectNextItem()
        {
            if (m_Slots.Count == 0) return;

            int nextIndex = (m_SelectedSlotIndex + 1) % m_Slots.Count;
            SelectSlot(nextIndex);
        }

        /// <summary>
        /// Belirli bir indexteki slotu aktif eder ve eldeki görseli günceller.
        /// </summary>
        public void SelectSlot(int index)
        {
            if (index < 0 || index >= m_Slots.Count) return;

            m_SelectedSlotIndex = index;
            UpdateVisuals();

            OnSelectionChanged?.Invoke(CurrentItem);
        }

        /// <summary>
        /// Objeyi fiziksel olarak 'InventoryContainer' altýna alýp gizler ve fiziðini kapatýr.
        /// </summary>
        private void StorePhysicalObject(GameObject obj)
        {
            // Collider ve Fizik kapat
            var col = obj.GetComponent<Collider>();
            if (col) col.enabled = false;

            var rb = obj.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
                rb.linearVelocity = Vector3.zero; // Unity 6 (eski adý velocity)
                rb.angularVelocity = Vector3.zero;
            }

            // Hiyerarþi yönetimi
            obj.transform.SetParent(m_InventoryContainer);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.SetActive(false);
        }

        /// <summary>
        /// Seçili olan slotun ilk fiziksel objesini ele alýr (`CurrentObjectHandle`).
        /// </summary>
        private void UpdateVisuals()
        {
            // 1. Önce eldekini depoya geri gönder/temizle (her durumda yapýlmalý)
            if (m_CurrentVisualObject != null)
            {
                m_CurrentVisualObject.transform.SetParent(m_InventoryContainer);
                m_CurrentVisualObject.transform.localPosition = Vector3.zero;
                m_CurrentVisualObject.SetActive(false);
                m_CurrentVisualObject = null;
            }

            // CRASH FIX: Eðer envanter boþaldýysa veya index sýnýr dýþýysa iþlem yapma
            if (m_Slots.Count == 0)
            {
                return;
            }

            // Ekstra güvenlik: Index sýnýr dýþýysa sýfýrla
            if (m_SelectedSlotIndex >= m_Slots.Count)
            {
                m_SelectedSlotIndex = 0;
            }

            // 2. Yeni seçimi getir
            var currentSlot = m_Slots[m_SelectedSlotIndex];

            if (currentSlot.PhysicalObjects.Count > 0)
            {
                // Slotun içindeki ilk objeyi görsel olarak kullanýyoruz
                GameObject objToShow = currentSlot.PhysicalObjects[0];

                if (objToShow != null)
                {
                    objToShow.transform.SetParent(m_CurrentObjectHandle);
                    objToShow.transform.localPosition = Vector3.zero;
                    objToShow.transform.localRotation = Quaternion.identity;
                    objToShow.SetActive(true);

                    m_CurrentVisualObject = objToShow;
                }
            }
        }

        private void NotifyChanges()
        {
            OnInventoryChanged?.Invoke(m_Slots);
        }
        public bool HasItem(string itemID)
        {
            foreach (var slot in m_Slots)
            {
                if (slot.Data.ID == itemID) return true;
            }
            return false;
        }

        public bool RemoveItem(string itemID)
        {
            for (int i = 0; i < m_Slots.Count; i++)
            {
                var slot = m_Slots[i];
                if (slot.Data.ID == itemID)
                {
                    // Stackten bir tane düþ, fiziksel objeyi yok et/kaldýr
                    if (slot.PhysicalObjects.Count > 0)
                    {
                        var objToRemove = slot.PhysicalObjects[slot.PhysicalObjects.Count - 1];
                        slot.PhysicalObjects.RemoveAt(slot.PhysicalObjects.Count - 1);
                        Destroy(objToRemove);
                    }

                    slot.StackSize--;

                    // Eðer stack bittiyse slotu tamamen sil
                    if (slot.StackSize <= 0)
                    {
                        // Eðer silinen slot þu an elimizde tuttuðumuzsa, görsel referansý hemen kopar
                        if (m_SelectedSlotIndex == i && m_CurrentVisualObject != null)
                        {
                            m_CurrentVisualObject = null; // UpdateVisuals temizleyecek ama referansý null yapalým
                        }

                        m_Slots.RemoveAt(i);

                        // Index Ayarlamasý:
                        // Liste boþaldýysa index 0 kalabilir (UpdateVisuals'da check var).
                        // Liste boþalmadýysa ama son elemaný sildiysek, indexi bir geri çek.
                        if (m_SelectedSlotIndex >= m_Slots.Count)
                        {
                            m_SelectedSlotIndex = Mathf.Max(0, m_Slots.Count - 1);
                        }
                    }

                    UpdateVisuals(); // Artýk güvenli
                    NotifyChanges();
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}