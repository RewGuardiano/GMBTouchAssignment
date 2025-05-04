using UnityEngine;

public class CameraController : MonoBehaviour
{
    ITouchable selectedObject;

    public float panSpeed = 10f;
    public float rotationSpeed = 0.2f;
    public float zoomSpeed = 1f;

    internal float lastPinchDistance;
    internal Vector2 lastMidPoint;
    internal ManagerScript managerScript;
    internal bool isPanning = false;
    internal Vector3 lastPanPosition;

    public Transform groundPlane;
    internal JoystickController joystick;

    void Start()
    {
        managerScript = FindObjectOfType<ManagerScript>();
        joystick = FindObjectOfType<JoystickController>();
    }

    void Update()
    {
        // Handle joystick panning first to ensure it takes priority
        if (joystick.IsJoystickActive && selectedObject == null)
        {
            Vector2 joystickInput = joystick.JoystickInput;
            Vector3 right = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
            Vector3 panDirection = (right * joystickInput.x + forward * joystickInput.y) * panSpeed * Time.deltaTime;
            transform.position += panDirection;
        }
        // Handle single-touch panning outside the joystick
        else if (Input.touchCount == 1 && selectedObject == null)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                lastPanPosition = t.position;
                isPanning = true;
            }
            else if (t.phase == TouchPhase.Moved && isPanning)
            {
                PanCamera(t.position);
            }
            else if (t.phase == TouchPhase.Ended)
            {
                isPanning = false;
            }
        }
        // Handle two-finger rotation and zoom
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 currentMidpoint = (touch1.position + touch2.position) / 2;

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                lastMidPoint = currentMidpoint;
                lastPinchDistance = Vector2.Distance(touch1.position, touch2.position);
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                Vector2 cameraRotationDelta = currentMidpoint - lastMidPoint;
                float horizontalAngle = cameraRotationDelta.x * rotationSpeed;
                OrbitCameraHorizontal(horizontalAngle);

                float verticalAngle = -cameraRotationDelta.y * rotationSpeed;
                OrbitCameraVertical(verticalAngle);
                lastMidPoint = currentMidpoint;

                float currentPinchDistance = Vector2.Distance(touch1.position, touch2.position);
                float pinchDelta = currentPinchDistance - lastPinchDistance;
                CameraZoom(pinchDelta * zoomSpeed);
                lastPinchDistance = currentPinchDistance;
            }
        }
    }

    void PanCamera(Vector3 newPanPosition)
    {
        Vector3 offset = Camera.main.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(-offset.x * panSpeed, 0, -offset.y * panSpeed) * Time.deltaTime;
        transform.Translate(move, Space.World);
        lastPanPosition = newPanPosition;
    }

    public void OrbitCameraHorizontal(float angle)
    {
        transform.RotateAround(transform.position, groundPlane.up, angle);
    }

    internal void OrbitCameraVertical(float angle)
    {
        transform.RotateAround(transform.position, transform.right, angle);
    }

    internal void CameraZoom(float pinchDelta)
    {
        Vector3 zoomDirection = transform.forward * (pinchDelta * zoomSpeed * Time.deltaTime);
        transform.position += zoomDirection;
    }
}