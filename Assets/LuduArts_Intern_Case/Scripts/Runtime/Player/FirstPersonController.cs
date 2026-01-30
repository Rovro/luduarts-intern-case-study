using System;
using UnityEngine;

namespace FPSGame.Runtime
{
    /// <summary>
    /// Karakterin fiziksel hareketlerini ve kamera kontrollerini yöneten sýnýf.
    /// Kamera yönetimini "Runtime Re-parenting" yöntemiyle ele alýr.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(InputManager))]
    public class FirstPersonController : MonoBehaviour
    {
        #region Fields

        [Header("Movement")]
        [SerializeField] private float m_WalkSpeed = 5f;
        [SerializeField] private float m_SprintSpeed = 8f;
        [SerializeField] private float m_JumpHeight = 1.2f;
        [SerializeField] private float m_Gravity = -15.0f;

        [Header("Camera Settings")]
        [Tooltip("Kameranýn oturacaðý göz hizasý objesi.")]
        [SerializeField] private Transform m_CameraRoot;
        [SerializeField] private float m_MouseSensitivity = 1f;
        [SerializeField] private float m_TopClamp = 85f;
        [SerializeField] private float m_BottomClamp = -85f;

        private CharacterController m_Controller;
        private InputManager m_InputManager;
        private Camera m_PlayerCamera;
        private float m_VerticalVelocity;
        private float m_CameraPitch;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            InitializeComponents();
            InitializeCamera();
        }

        private void Update()
        {
            HandleCameraRotation();
            HandleMovement();
        }

        #endregion

        #region Methods

        private void InitializeComponents()
        {
            m_Controller = GetComponent<CharacterController>();
            m_InputManager = GetComponent<InputManager>();

            if (m_CameraRoot == null)
            {
                Debug.LogError($"[FirstPersonController] CameraRoot is not assigned in {name}!");
                enabled = false;
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void InitializeCamera()
        {
            m_PlayerCamera = Camera.main;

            if (m_PlayerCamera == null)
            {
                Debug.LogError("[FirstPersonController] No MainCamera found in the scene via Camera.main!");
                return;
            }

            m_PlayerCamera.transform.SetParent(m_CameraRoot);
            m_PlayerCamera.transform.localPosition = Vector3.zero;
            m_PlayerCamera.transform.localRotation = Quaternion.identity;
        }

        private void HandleCameraRotation()
        {
            // EKLEME: Eðer cursor kilitli deðilse (yani envanter açýksa), kamera dönmesin.
            if (Cursor.lockState != CursorLockMode.Locked) return;

            if (m_InputManager.LookInput.sqrMagnitude < 0.001f) return;

            float mouseX = m_InputManager.LookInput.x * m_MouseSensitivity;
            float mouseY = m_InputManager.LookInput.y * m_MouseSensitivity;

            m_CameraPitch -= mouseY;
            m_CameraPitch = Mathf.Clamp(m_CameraPitch, m_BottomClamp, m_TopClamp);

            m_CameraRoot.localRotation = Quaternion.Euler(m_CameraPitch, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

        private void HandleMovement()
        {
            bool isGrounded = m_Controller.isGrounded;

            if (isGrounded && m_VerticalVelocity < 0)
            {
                m_VerticalVelocity = -2f;
            }

            float targetSpeed = m_InputManager.IsSprintPressed ? m_SprintSpeed : m_WalkSpeed;

            if (m_InputManager.MoveInput == Vector2.zero)
            {
                targetSpeed = 0f;
            }

            Vector3 inputDirection = transform.right * m_InputManager.MoveInput.x + transform.forward * m_InputManager.MoveInput.y;

            m_Controller.Move(inputDirection * (targetSpeed * Time.deltaTime));

            if (m_InputManager.IsJumpPressed && isGrounded)
            {
                m_VerticalVelocity = Mathf.Sqrt(m_JumpHeight * -2f * m_Gravity);
            }

            m_VerticalVelocity += m_Gravity * Time.deltaTime;
            m_Controller.Move(Vector3.up * (m_VerticalVelocity * Time.deltaTime));
        }

        #endregion
    }
}