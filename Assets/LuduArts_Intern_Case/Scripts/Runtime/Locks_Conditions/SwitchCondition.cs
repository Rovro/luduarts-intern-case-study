using UnityEngine;
using FPSGame.Runtime.Interaction.Interactables;

namespace FPSGame.Runtime.Interaction.Locks
{
    /// <summary>
    /// Bir kapýnýn açýlmasý için belirli bir þalterin (SwitchController) 
    /// aktif olup olmadýðýný kontrol eder.
    /// </summary>
    public class SwitchCondition : LockCondition
    {
        #region Fields

        [Header("Requirement")]
        [Tooltip("Bu kilidi açmak için hangi þalterin indirilmesi gerekiyor?")]
        [SerializeField] private SwitchController m_RequiredSwitch;

        [Tooltip("Þalter kapalýyken gösterilecek hata mesajý.")]
        [SerializeField] private string m_FailureMessage = "Requires Power";

        #endregion

        #region LockCondition Implementation

        public override string DenialMessage => m_FailureMessage;

        public override bool IsMet(GameObject interactor)
        {
            if (m_RequiredSwitch == null)
            {
                // Eðer þalter atanmamýþsa developer hatasýdýr, ama oyun kilitlenmesin diye true dönelim
                Debug.LogWarning($"SwitchCondition on {name} has no reference switch!");
                return true;
            }

            return m_RequiredSwitch.IsOn;
        }

        // OnUnlock metodunu override etmemize gerek yok, 
        // çünkü þalteri kapý açýlýnca yok etmiyoruz/kapatmýyoruz.

        #endregion
    }
}