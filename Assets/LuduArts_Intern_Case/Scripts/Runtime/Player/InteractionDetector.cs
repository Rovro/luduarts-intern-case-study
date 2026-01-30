using System;
using UnityEngine;
using FPSGame.Runtime;

namespace FPSGame.Runtime.Interaction
{
    /// <summary>
    /// Oyuncunun baktýðý IInteractable nesneleri Raycast ile tespit eder.
    /// Durum deðiþikliklerini event ile bildirir.
    /// </summary>
    [RequireComponent(typeof(InputManager))]
    public class InteractionDetector : MonoBehaviour
    {
        #region Fields

        [Header("Detection Settings")]
        [SerializeField] private float m_InteractionRange = 3.0f;
        [SerializeField] private LayerMask m_InteractionLayer;

        [Header("Debug")]
        [SerializeField] private bool m_ShowDebugGizmos = true;

        private InputManager m_InputManager;
        private Camera m_PlayerCamera;
        private IInteractable m_CurrentInteractable;

        #endregion

        #region Events

        /// <summary>
        /// Odaklanýlan etkileþim nesnesi deðiþtiðinde tetiklenir.
        /// Parametre null ise oyuncu boþluða bakýyordur.
        /// </summary>
        public event Action<IInteractable> OnTargetChanged;

        #endregion

        #region Properties

        public IInteractable CurrentInteractable => m_CurrentInteractable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_InputManager = GetComponent<InputManager>();
        }

        private void Start()
        {
            InitializeCamera();
        }

        private void Update()
        {
            DetectInteractable();
            HandleInteractionInput();
        }

        private void OnDrawGizmos()
        {
            if (!m_ShowDebugGizmos || m_PlayerCamera == null)
            {
                Debug.LogWarning("[InteractionDetector] Cannot draw gizmos. Either debug is disabled or player camera is null.");
                return;
            } 

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(m_PlayerCamera.transform.position, m_PlayerCamera.transform.forward * m_InteractionRange);
        }

        #endregion

        #region Methods

        private void InitializeCamera()
        {
            m_PlayerCamera = GetComponentInChildren<Camera>();

            if (m_PlayerCamera == null)
            {
                m_PlayerCamera = Camera.main;

                if (m_PlayerCamera == null)
                {
                    Debug.LogError("[InteractionDetector] Camera not found via GetComponentInChildren or Camera.main!");
                }
            }
        }

        private void DetectInteractable()
        {
            if (m_PlayerCamera == null)
            {
                Debug.LogWarning("[InteractionDetector] Player camera is null. Cannot detect interactables.");
                return;
            }

            IInteractable newInteractable = null;
            Ray ray = new Ray(m_PlayerCamera.transform.position, m_PlayerCamera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, m_InteractionRange, m_InteractionLayer))
            {
                Debug.Log($"Raycast Hit: {hitInfo.collider.name}");
                newInteractable = hitInfo.collider.GetComponent<IInteractable>();
            }

            if (newInteractable != m_CurrentInteractable)
            {
                Debug.Log($"Target Changed: {(newInteractable != null ? "New Item" : "Null")}");
                m_CurrentInteractable = newInteractable;
                OnTargetChanged?.Invoke(m_CurrentInteractable);
            }
        }

        private void HandleInteractionInput()
        {
            if (m_InputManager.InteractTriggered && m_CurrentInteractable != null)
            {
                m_CurrentInteractable.OnInteract(gameObject);
                m_InputManager.ConsumeInteract();
            }
        }

        #endregion
    }
}