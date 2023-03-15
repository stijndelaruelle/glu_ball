using UnityEngine;
using UnityEngine.InputSystem;

namespace GLUBall
{
    public class BallController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Rigidbody m_RigidBody = null;

        [Header("Settings")]
        [SerializeField]
        private float m_Accelleration = 5.0f;

        [SerializeField]
        private float m_JumpStrength = 5.0f;

        //State
        private InputActions_GLUBall m_InputActions = null;
        private Vector2 m_MovementThisFrame = Vector2.zero;
        private bool m_HasJumpedThisFrame = false;

        private void Awake()
        {
            InitializeInput();
        }

        private void OnDestroy()
        {
            DeinitializeInput();
        }

        private void InitializeInput()
        {
            m_InputActions = new InputActions_GLUBall();

            //Enable the default action map
            m_InputActions.Default.Enable();

            //Subscribe to actions
            m_InputActions.Default.Jump.performed += OnJumpInput;
        }

        private void DeinitializeInput()
        {
            if (m_InputActions == null)
                return;

            //Unsubscribe from all actions
            m_InputActions.Default.Jump.performed -= OnJumpInput;

            //Disable the map again
            m_InputActions.Default.Disable();
        }

        private void Update()
        {
            HandleInput();
        }

        private void FixedUpdate()
        {
            //We move only in the fixed update (phyics update step)
            HandleMovement();
            HandleJump();
        }

        private void HandleInput()
        {
            if (m_InputActions == null)
                return;

            m_MovementThisFrame = m_InputActions.Default.Movement.ReadValue<Vector2>();
        }

        private void HandleMovement()
        {
            if (m_RigidBody == null)
                return;

            //Calculate the force vector
            Vector3 forceVector = Vector3.zero;
            forceVector.x = m_MovementThisFrame.x * m_Accelleration * Time.fixedDeltaTime;
            forceVector.z = m_MovementThisFrame.y * m_Accelleration * Time.fixedDeltaTime;

            //Actually apply the force
            m_RigidBody.AddForce(forceVector, ForceMode.Force);

            //Reset
            m_MovementThisFrame = Vector2.zero;
        }

        private void HandleJump()
        {
            if (m_HasJumpedThisFrame == false)
                return;

            //Perform the jump
            if (m_RigidBody != null)
                m_RigidBody.AddForce(Vector3.up * m_JumpStrength, ForceMode.Impulse);

            //Reset
            m_HasJumpedThisFrame = false;
        }

        //Input Callbacks
        private void OnJumpInput(InputAction.CallbackContext context)
        {
            m_HasJumpedThisFrame = true;
        }
    }
}