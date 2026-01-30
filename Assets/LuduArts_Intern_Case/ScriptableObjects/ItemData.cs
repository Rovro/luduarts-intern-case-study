using UnityEngine;

namespace FPSGame.Runtime.Inventory
{
    /// <summary>
    /// Oyundaki toplanabilir eþyalarýn temel veri yapýsý.
    /// </summary>
    [CreateAssetMenu(fileName = "NewItem", menuName = "FPSGame/Inventory/Item Data")]
    public class ItemData : ScriptableObject
    {
        #region Fields

        [Tooltip("Eþyanýn oyun içinde görünen adý.")]
        [SerializeField] private string m_ItemName;

        [Tooltip("Eþyanýn benzersiz ID'si.")]
        [SerializeField] private string m_ID;

        [Tooltip("UI'da gösterilecek ikon.")]
        [SerializeField] private Sprite m_Icon;

        [Tooltip("Bu eþyadan üst üste en fazla kaç tane birikebilir?")]
        [Min(1)]
        [SerializeField] private int m_MaxStackSize = 1;

        #endregion

        #region Properties

        public string ItemName => m_ItemName;
        public string ID => m_ID;
        public Sprite Icon => m_Icon;
        public int MaxStackSize => m_MaxStackSize;

        #endregion
    }
}