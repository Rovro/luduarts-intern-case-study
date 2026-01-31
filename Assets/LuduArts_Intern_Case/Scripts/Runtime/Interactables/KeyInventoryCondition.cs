using UnityEngine;
using FPSGame.Runtime.Inventory;

namespace FPSGame.Runtime.Interaction.Locks
{
    /// <summary>
    /// Envanterde belirli bir eþyanýn olup olmadýðýný kontrol eden kilit koþulu.
    /// </summary>
    public class KeyInventoryCondition : LockCondition
    {
        #region Fields

        [Tooltip("Bu kilidi açmak için gereken eþya.")]
        [SerializeField] private ItemData m_RequiredKey;

        [Tooltip("Kilit açýldýðýnda anahtar envanterden silinsin mi?")]
        [SerializeField] private bool m_ConsumeKey = true;

        #endregion

        #region LockCondition Implementation

        public override string DenialMessage => m_RequiredKey != null
            ? $"Requires {m_RequiredKey.ItemName}"
            : "Requires a specific key";

        public override bool IsMet(GameObject interactor)
        {
            if (m_RequiredKey == null)
            { 
                Debug.LogWarning("No key assigned to KeyInventoryCondition.");
                return true;
            }// Anahtar atanmamýþsa kilit yok sayýlýr

            var inventory = interactor.GetComponent<InventoryController>();
            if (inventory == null)
            {
                Debug.LogWarning("Interactor has no InventoryController component.");
                return false;
            }

            // InventoryController'a HasItem(ID) metodunu eklemiþtik, onu kullanýyoruz
            return inventory.HasItem(m_RequiredKey.ID);
        }

        public override void OnUnlock(GameObject interactor)
        {
            if (m_ConsumeKey)
            {
                var inventory = interactor.GetComponent<InventoryController>();
                if (inventory != null)
                {
                    inventory.RemoveItem(m_RequiredKey.ID);
                }
            }
        }

        #endregion
    }
}