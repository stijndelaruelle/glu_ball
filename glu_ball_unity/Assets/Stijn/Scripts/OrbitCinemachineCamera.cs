using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GLUBall
{
    [RequireComponent(typeof(CinemachineFreeLook))]
    public class OrbitCinemachineCamera : MonoBehaviour, AxisState.IInputAxisProvider
    {
        [Header("Settings")]
        [SerializeField]
        private float m_RotateSensitivity = 1.0f;

        [SerializeField]
        private float m_ZoomSensitivity = 1.0f;

        //State
        private InputActions_GLUBall m_InputActions = null;
        private bool m_IsRotating = false;

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

            //Enable the Camera action map
            m_InputActions.Camera.Enable();

            //Subscribe to actions
            m_InputActions.Camera.StartRotating.performed += OnStartRotatingInput;
            m_InputActions.Camera.StopRotating.performed += OnStopRotatingInput;
        }

        private void DeinitializeInput()
        {
            if (m_InputActions == null)
                return;

            //Unsubscribe from all actions
            m_InputActions.Camera.StartRotating.performed -= OnStartRotatingInput;
            m_InputActions.Camera.StopRotating.performed -= OnStopRotatingInput;

            //Disable the map again
            m_InputActions.Camera.Disable();
        }

        //Rotating
        private void StartRotating()
        {
            if (m_IsRotating == true)
                return;

            m_IsRotating = true;
        }

        private void StopRotating()
        {
            if (m_IsRotating == false)
                return;

            //Reset state
            m_IsRotating = false;
        }

        public float GetAxisValue(int axis)
        {
            //AxisState.IInputAxisProvider override
            if (enabled == false || m_InputActions == null)
                return 0.0f;

            //Only rotate when required
            Vector2 rotateDelta = Vector2.zero;
            if (m_IsRotating == true)
            {
                rotateDelta = m_InputActions.Camera.Rotate.ReadValue<Vector2>();
                rotateDelta *= m_RotateSensitivity;
            }

            //Always allowed to zoom
            float zoomDelta = m_InputActions.Camera.Zoom.ReadValue<Vector2>().y; //No idea why this is required to be a Vector2
            zoomDelta = Mathf.Clamp(zoomDelta, -1.0f, 1.0f); //120 and -120 on most systems (except for linux?)
            zoomDelta *= m_ZoomSensitivity;

            //0 = X, 1 = Y, 2 = Z
            switch (axis)
            {
                case 0: return rotateDelta.x;
                case 1: return zoomDelta;
                case 2: return rotateDelta.y;
            }

            return 0.0f;
        }

        //Input callbacks
        private void OnStartRotatingInput(InputAction.CallbackContext context)
        {
            StartRotating();
        }

        private void OnStopRotatingInput(InputAction.CallbackContext context)
        {
            StopRotating();
        }
    }
}