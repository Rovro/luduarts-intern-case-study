using UnityEngine;
using FPSGame.Runtime.Inventory;

namespace FPSGame.Runtime.Interaction.Interactables
{
    /// <summary>
    /// Yerden toplanabilen eþyalar için etkileþim sýnýfý.
    /// Þimdilik sadece Prompt verisini saðlar, toplama iþlemi devre dýþýdýr.
    /// </summary>
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
            return true;
        }

        #endregion
    }
}