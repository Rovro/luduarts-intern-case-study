using UnityEngine;
using TMPro;
using FPSGame.Runtime.Interaction;

namespace FPSGame.Runtime.UI
{
    /// <summary>
    /// InteractionDetector eventlerini dinleyerek UI güncellemesi yapar.
    /// Event-Driven çalýþtýðý için performans dostudur.
    /// </summary>
    public class InteractionUI : MonoBehaviour
    {
        #region Fields

        [Header("References")]
        [SerializeField] private InteractionDetector m_Detector;
        [SerializeField] private TextMeshProUGUI m_PromptText;

        [Header("Settings")]
        [SerializeField] private string m_KeySuffix = " [E]";

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (m_Detector == null)
            {
                Debug.LogError("InteractionDetector is not assigned in InteractionUI!");
                enabled = false;
                return;
            }

            if (m_PromptText == null)
            {
                Debug.LogError("PromptText is not assigned in InteractionUI!");
                enabled = false;
                return;
            }

            m_PromptText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if (m_Detector != null)
            {
                m_Detector.OnTargetChanged += HandleTargetChanged;
            }
        }

        private void OnDisable()
        {
            if (m_Detector != null)
            {
                m_Detector.OnTargetChanged -= HandleTargetChanged;
            }
        }

        #endregion

        #region Methods

        private void HandleTargetChanged(IInteractable interactable)
        {
            if (interactable != null)
            {
                string message = interactable.InteractionPrompt + m_KeySuffix;
                m_PromptText.text = message;
                m_PromptText.gameObject.SetActive(true);
            }
            else
            {
                m_PromptText.gameObject.SetActive(false);
                m_PromptText.text = string.Empty;
            }
        }

        #endregion
    }
}