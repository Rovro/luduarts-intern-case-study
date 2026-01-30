using UnityEngine;
using FPSGame.Runtime.Inventory;

namespace FPSGame.Runtime.Interaction.Interactables
{
    public class PickableItem : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Item Settings")]
        [SerializeField] private ItemData m_ItemData;

        #endregion

        #region Properties

        public string InteractionPrompt => m_ItemData != null ? $"Pick up {m_ItemData.ItemName}" : "Pick up Item";

        #endregion

        #region Methods

        public bool OnInteract(GameObject interactor)
        {
            if (m_ItemData == null) return false;

            var inventory = interactor.GetComponent<InventoryController>();
            if (inventory == null) return false;

            // Kendimizi (gameObject) de gönderiyoruz
            // Destroy KULLANMIYORUZ. InventoryController yönetecek.
            if (inventory.AddItem(m_ItemData, gameObject))
            {
                Debug.Log($"Collected: {m_ItemData.ItemName}");
                return true;
            }

            return false;
        }

        #endregion
    }
}