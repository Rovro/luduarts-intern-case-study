using UnityEngine;

namespace FPSGame.Runtime.Interaction
{
    /// <summary>
    /// Hareketli parçalar (Child objeler) üzerindeki etkileþimleri 
    /// ana kontrolcüye (Parent) yönlendiren yardýmcý sýnýf.
    /// </summary>
    public class InteractionRelay : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("References")]
        [Tooltip("Etkileþimin yönlendirileceði asýl kontrolcü (Genelde Parent'taki script).")]
        [SerializeField] private GameObject m_MainInteractableObject;

        private IInteractable m_MainInteractable;

        #endregion

        #region Properties

        public string InteractionPrompt
        {
            get
            {
                ValidateReference();
                return m_MainInteractable != null ? m_MainInteractable.InteractionPrompt : string.Empty;
            }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            ValidateReference();
        }

        #endregion

        #region Methods

        public bool OnInteract(GameObject interactor)
        {
            ValidateReference();

            if (m_MainInteractable != null)
            {
                // Çaðrýyý direkt ana patrona yönlendiriyoruz
                return m_MainInteractable.OnInteract(interactor);
            }

            return false;
        }

        private void ValidateReference()
        {
            // Eðer cachelenmiþ referans yoksa ve obje atanmýþsa al
            if (m_MainInteractable == null && m_MainInteractableObject != null)
            {
                m_MainInteractable = m_MainInteractableObject.GetComponent<IInteractable>();

                if (m_MainInteractable == null)
                {
                    Debug.LogError($"InteractionRelay: '{m_MainInteractableObject.name}' objesinde IInteractable bulunamadý!");
                }
            }
        }

        #endregion
    }
}