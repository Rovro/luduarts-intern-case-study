using UnityEngine;
using UnityEngine.InputSystem;

namespace FPSGame.Runtime
{
    /// <summary>
    /// Unity Input System paketini kullanarak oyuncu girdilerini yöneten ve
    /// diðer sýnýflara servis eden sýnýf.
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        #region Fields

        private PlayerInputActions m_InputActions;

        #endregion

        #region Properties

        /// <summary>
        /// Hareket girdisi (WASD).
        /// </summary>
        public Vector2 MoveInput { get; private set; }

        /// <summary>
        /// Bakýþ girdisi (Mouse Delta).
        /// </summary>
        public Vector2 LookInput { get; private set; }

        /// <summary>
        /// Zýplama tuþu basýlý mý?
        /// </summary>
        public bool IsJumpPressed { get; private set; }

        /// <summary>
        /// Koþma tuþu basýlý mý?
        /// </summary>
        public bool IsSprintPressed { get; private set; }

        /// <summary>
        /// Etkileþim tuþu tetiklendi mi?
        /// </summary>
        public bool InteractTriggered { get; private set; }

        /// <summary>
        /// Inventory tuþu tetiklendi mi?
        /// </summary>
        public bool InventoryTriggered { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            m_InputActions = new PlayerInputActions();

            m_InputActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            m_InputActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;

            m_InputActions.Player.Look.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
            m_InputActions.Player.Look.canceled += ctx => LookInput = Vector2.zero;

            m_InputActions.Player.Jump.performed += ctx => IsJumpPressed = true;
            m_InputActions.Player.Jump.canceled += ctx => IsJumpPressed = false;

            m_InputActions.Player.Sprint.performed += ctx => IsSprintPressed = true;
            m_InputActions.Player.Sprint.canceled += ctx => IsSprintPressed = false;

            m_InputActions.Player.Interact.started += ctx => InteractTriggered = true;
            m_InputActions.Player.Interact.canceled += ctx => InteractTriggered = false;

            m_InputActions.Player.Inventory.started += ctx => InventoryTriggered = true;
            m_InputActions.Player.Inventory.canceled += ctx => InventoryTriggered = false;
        }

        private void OnEnable()
        {
            m_InputActions.Enable();
        }

        private void OnDisable()
        {
            m_InputActions.Disable();
        }

        #endregion

        #region Methods
        public void ConsumeInteract()
        {
            InteractTriggered = false;
        }
        public void ConsumeInventoryInput()
        {
            InventoryTriggered = false;
        }

        #endregion
    }
}