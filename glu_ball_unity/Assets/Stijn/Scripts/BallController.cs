using UnityEngine;
using UnityEngine.InputSystem;

namespace GLUBall
{
    public class BallController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private Rigidbody m_RigidBody = null;

        [SerializeField]
        private SphereCollider m_SphereCollider = null;

        [SerializeField]
        private Transform m_CameraTransform = null;
        private Transform m_Transform = null;

        [Header("Settings")]
        [SerializeField]
        private float m_Accelleration = 5.0f;

        [SerializeField]
        private float m_JumpStrength = 5.0f;

        //State
        private InputActions_GLUBall m_InputActions = null;
        private Vector2 m_MovementInputThisFrame = Vector2.zero;
        private bool m_HasJumpedThisFrame = false;

        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            DeinitializeInput();
        }

        private void Initialize()
        {
            //Cache our transform (gameObject.transform = GetComponent<Transform>())
            m_Transform = gameObject.transform;

            InitializeInput();
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

            m_MovementInputThisFrame = m_InputActions.Default.Movement.ReadValue<Vector2>();
        }

        private void HandleMovement()
        {
            if (m_RigidBody == null)
                return;

            //Calculate the force vector
            Vector3 forceDirection = new Vector3(m_MovementInputThisFrame.x, 0.0f, m_MovementInputThisFrame.y);

            //Move in the direction the camera is pointing in
            if (m_CameraTransform != null)
            {
                //Forward
                Vector3 cameraForward = m_CameraTransform.forward;
                cameraForward.y = 0;

                //Right
                Vector3 cameraRight = m_CameraTransform.right;
                cameraRight.y = 0;

                //Alter force direction
                forceDirection = ((cameraForward * m_MovementInputThisFrame.y) + (cameraRight * m_MovementInputThisFrame.x)).normalized;
            }

            //Multiply by the accelleration
            forceDirection *= m_Accelleration * Time.fixedDeltaTime;

            //Actually apply the force
            m_RigidBody.AddForce(forceDirection, ForceMode.Force);

            //Reset
            m_MovementInputThisFrame = Vector2.zero;
        }

        private void HandleJump()
        {
            if (m_HasJumpedThisFrame == false)
                return;

            //Check if we are grounded
            if (IsGrounded())
            {
                //Perform the jump
                if (m_RigidBody != null)
                    m_RigidBody.AddForce(Vector3.up * m_JumpStrength, ForceMode.Impulse);
            }

            //Reset
            m_HasJumpedThisFrame = false;
        }

        private bool IsGrounded()
        {
            if (m_Transform == null || m_SphereCollider == null)
                return true;

            //Spherecast instead of raycast (more flexible on slopes)
            //Not OverlapSphere as that doesn't return the contact points (needed to calculate wether or not the slope is too steep)
            RaycastHit hitInfo = default;
            bool hasHit = Physics.SphereCast(m_Transform.position, m_SphereCollider.radius - 0.1f, Vector3.down, out hitInfo, 0.1f);

            return hasHit;
        }

        //Input Callbacks
        private void OnJumpInput(InputAction.CallbackContext context)
        {
            m_HasJumpedThisFrame = true;
        }
    }
}