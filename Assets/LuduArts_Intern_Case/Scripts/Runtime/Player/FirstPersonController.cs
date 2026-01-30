using System;
using UnityEngine;

namespace FPSGame.Runtime
{
    /// <summary>
    /// Karakterin fiziksel hareketlerini ve kamera kontrollerini yöneten sýnýf.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(InputManager))]
    public class FirstPersonController : MonoBehaviour
    {
        #region Fields

        // Serialized private instance fields
        [Header("Movement")]
        [SerializeField] private float m_WalkSpeed = 5f;
        [SerializeField] private float m_SprintSpeed = 8f;
        [SerializeField] private float m_JumpHeight = 1.2f;
        [SerializeField] private float m_Gravity = -15.0f;

        [Header("Camera")]
        [SerializeField] private Transform m_CameraRoot;
        [SerializeField] private float m_MouseSensitivity = 1f;
        [SerializeField] private float m_TopClamp = 85f;
        [SerializeField] private float m_BottomClamp = -85f;

        // Non-serialized private instance fields
        private CharacterController m_Controller;
        private InputManager m_InputManager;
        private float m_VerticalVelocity;
        private float m_CameraPitch;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_Controller = GetComponent<CharacterController>();
            m_InputManager = GetComponent<InputManager>();

            if (m_CameraRoot == null)
            {
                Debug.LogError($"CameraRoot is not assigned in {name}!");
                enabled = false;
                return;
            }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            HandleCamera();
            HandleMovement();
        }

        #endregion

        #region Methods

        private void HandleCamera()
        {
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