using UnityEngine;

namespace FPSGame.Runtime.Inventory
{
    /// <summary>
    /// Oyundaki toplanabilir eþyalarýn temel veri yapýsý.
    /// </summary>
    [CreateAssetMenu(fileName = "NewItem", menuName = "InteractionSystem/Inventory/Item Data")]
    public class ItemData : ScriptableObject
    {
        #region Fields

        [Tooltip("Eþyanýn oyun içinde görünen adý.")]
        [SerializeField] private string m_ItemName;

        [Tooltip("Eþyanýn benzersiz ID'si (Ýleride Save sistemi için gerekli).")]
        [SerializeField] private string m_ID;

        [Tooltip("UI'da gösterilecek ikon.")]
        [SerializeField] private Sprite m_Icon;

        #endregion

        #region Properties

        public string ItemName => m_ItemName;
        public string ID => m_ID;
        public Sprite Icon => m_Icon;

        #endregion
    }
}