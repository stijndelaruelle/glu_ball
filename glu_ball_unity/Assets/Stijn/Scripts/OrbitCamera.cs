using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GLUBall
{
    public class OrbitCamera : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private CinemachineVirtualCamera m_VirtualCamera = null;
        private CinemachineOrbitalTransposer m_VirtualCameraOribitalTransposer = null;

        [Header("Settings")]
        [SerializeField]
        private float m_MouseSensitivity = 1.0f;

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

        private void Start()
        {
            if (m_VirtualCamera != null)
                m_VirtualCameraOribitalTransposer = m_VirtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        }

        private void Update()
        {
            RotateCamera();
        }

        //Rotating
        private void RotateCamera()
        {
            if (m_InputActions == null || m_VirtualCameraOribitalTransposer == null)
                return;

            if (m_IsRotating == false)
                return;

            //Get the amount the camera input moved
            Vector2 delta = m_InputActions.Camera.Rotate.ReadValue<Vector2>();

            //Calculate the new rotation
            float currentBias = m_VirtualCameraOribitalTransposer.m_Heading.m_Bias; //From -180 to 180
            float newBias = currentBias + (delta.x * m_MouseSensitivity * Time.deltaTime);
            newBias = WrapFloat(newBias, -180.0f, 180.0f); //Wrap the number around (between -180 and 180)

            //Set the new rotation
            m_VirtualCameraOribitalTransposer.m_Heading.m_Bias = newBias;
        }

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

        //Utilities
        private float WrapFloat(float f, float min, float max)
        {
            //https://stackoverflow.com/questions/14415753/wrap-value-into-range-min-max-without-division
            return (((f - min) % (max - min)) + (max - min)) % (max - min) + min;
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