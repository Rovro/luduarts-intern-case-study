using System;

namespace FPSGame.Runtime.Inventory
{
    /// <summary>
    /// Envanterdeki tek bir slotu temsil eder.
    /// Eþya verisini ve adedini tutar.
    /// </summary>
    [Serializable]
    public class InventorySlot
    {
        #region Fields

        private ItemData m_ItemData;
        private int m_StackSize;

        #endregion

        #region Constructors

        public InventorySlot(ItemData item, int amount)
        {
            m_ItemData = item;
            m_StackSize = amount;
        }

        #endregion

        #region Properties

        public ItemData ItemData => m_ItemData;
        public int StackSize => m_StackSize;

        #endregion

        #region Methods

        /// <summary>
        /// Slotun kapasitesini aþmayacak þekilde ekleme yapar.
        /// </summary>
        /// <param name="amount">Eklenecek miktar.</param>
        public void AddToStack(int amount)
        {
            m_StackSize += amount;
        }

        #endregion
    }
}