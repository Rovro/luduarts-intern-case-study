using System.Collections;
using UnityEngine;

namespace FPSGame.Runtime.Interaction.Interactables
{
    /// <summary>
    /// Oyuncunun açýp kapatabildiði fiziksel þalter/switch.
    /// </summary>
    public class SwitchController : MonoBehaviour, IInteractable
    {
        #region Fields

        [Header("Settings")]
        [Tooltip("Baþlangýçta açýk mý?")]
        [SerializeField] private bool m_IsOn = false;

        [Tooltip("Tek seferlik mi? (True ise açýlýnca bir daha kapanmaz)")]
        [SerializeField] private bool m_IsOneWay = false;

        [Header("Animation")]
        [Tooltip("Dönecek olan kol parçasý.")]
        [SerializeField] private Transform m_HandleMesh;
        [SerializeField] private float m_OnAngle = 45f;
        [SerializeField] private float m_OffAngle = -45f;
        [SerializeField] private float m_Speed = 5f;

        private Coroutine m_AnimCoroutine;

        #endregion

        #region Properties

        // Diðer scriptlerin okuyacaðý durum verisi
        public bool IsOn => m_IsOn;

        public string InteractionPrompt
        {
            get
            {
                if (m_IsOneWay && m_IsOn) return string.Empty; // Zaten açýldýysa prompt yok
                return m_IsOn ? "Turn Off" : "Turn On";
            }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            // Baþlangýç açýsýna getir
            UpdateRotation(true);
        }

        #endregion

        #region Methods

        public bool OnInteract(GameObject interactor)
        {
            if (m_IsOneWay && m_IsOn) return false;

            m_IsOn = !m_IsOn;

            UpdateRotation();

            // Ses efekti vb. buraya eklenebilir
            Debug.Log($"Switch {(m_IsOn ? "Activated" : "Deactivated")}");

            return true;
        }

        private void UpdateRotation(bool immediate = false)
        {
            if (m_HandleMesh == null) return;

            float targetX = m_IsOn ? m_OnAngle : m_OffAngle;
            Quaternion targetRot = Quaternion.Euler(targetX, 0, 0);

            if (immediate)
            {
                m_HandleMesh.localRotation = targetRot;
            }
            else
            {
                if (m_AnimCoroutine != null) StopCoroutine(m_AnimCoroutine);
                m_AnimCoroutine = StartCoroutine(AnimateHandle(targetRot));
            }
        }

        private IEnumerator AnimateHandle(Quaternion target)
        {
            while (Quaternion.Angle(m_HandleMesh.localRotation, target) > 0.1f)
            {
                m_HandleMesh.localRotation = Quaternion.Slerp(
                    m_HandleMesh.localRotation,
                    target,
                    Time.deltaTime * m_Speed);
                yield return null;
            }
            m_HandleMesh.localRotation = target;
        }

        #endregion
    }
}