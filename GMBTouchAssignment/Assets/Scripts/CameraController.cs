using UnityEngine;

public class CameraController : MonoBehaviour
{
    ITouchable selectedObject;

    public float panSpeed = 10f;
    public float rotationSpeed = 0.2f;
    public float zoomSpeed = 1f;


    private float lastPinchDistance;
    private Vector2 lastMidPoint;
    private ManagerScript managerScript;
    private Vector3 lastPanPosition;
    private bool isPanning = false;
    private Vector2 lastTouchPosition;
 


    public Transform groundPlane;


    void Start()
    {
        managerScript = FindObjectOfType<ManagerScript>();
    }

    void Update()
    {

        
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                if (selectedObject == null)
                {
                    lastPanPosition = t.position;
                    isPanning = true;
                }
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
        else if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            Vector2 currentMidpoint = (touch1.position + touch2.position) / 2;

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                //Initialize rotation/zoom
                lastMidPoint = currentMidpoint;
                lastPinchDistance = Vector2.Distance(touch1.position, touch2.position);
            }
            else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
            {
                //rotation of camera based on midpoint movement
                Vector2 cameraRotationDelta = currentMidpoint - lastMidPoint;
                float horizontalAngle = cameraRotationDelta.x * rotationSpeed;
                OrbitCameraHorizontal(horizontalAngle);

                float verticalAngle = -cameraRotationDelta.y * rotationSpeed;
                OrbitCameraVertical(verticalAngle);
                lastMidPoint = currentMidpoint;

                //Zoom camera base on pinch distance

                float currentPinchDistance = Vector2.Distance(touch1.position,touch2.position);
                float pinchDelta = currentPinchDistance - lastPinchDistance;
                CameraZoom(pinchDelta * zoomSpeed);
                lastPinchDistance = currentPinchDistance;
            }
        }
    }

    void PanCamera(Vector3 newPanPosition)
    {
        Vector3 offset = Camera.main.ScreenToViewportPoint(lastPanPosition - newPanPosition);

        Vector3 right = Vector3.ProjectOnPlane(transform.right, Vector3.up);
        Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up);

        Vector3 move = (right * offset.x + forward * offset.y) * panSpeed;

        transform.Translate(move, Space.World);

        lastPanPosition = newPanPosition;
    }

    void OrbitCameraHorizontal(float angle)
    {
        transform.RotateAround(transform.position, groundPlane.up, angle);
    }

    void OrbitCameraVertical(float angle)
    {
        transform.RotateAround(transform.position, transform.right, angle);
    }
    void CameraZoom(float pinchDelta)
    {
        //Move the camera along its forward direction based on pinch delta
        Vector3 zoomDirection = transform.forward * (pinchDelta * zoomSpeed * Time.deltaTime);
        transform.position += zoomDirection;
    }

  
}