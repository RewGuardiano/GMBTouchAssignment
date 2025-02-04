using UnityEngine;

public class CameraController : MonoBehaviour
{
    ITouchable selectedObject;

    public float panSpeed = 10f;
    public float rotationSpeed = 2.0f;
   


    private ManagerScript managerScript;
    private Vector3 lastPanPosition;
    private bool isPanning = false;
    private Vector2 lastTouchPosition;

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
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
            {
                lastTouchPosition = t1.position - t2.position;
            }
            else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
            {
                Vector2 currentTouchPosition = t1.position - t2.position;
                float angle = Vector2.SignedAngle(lastTouchPosition, currentTouchPosition);

                OrbitCamera(angle);
                lastTouchPosition = currentTouchPosition;
            }
        }
    }

    void PanCamera(Vector3 newPanPosition)
    {
        Vector3 offset = Camera.main.ScreenToViewportPoint(lastPanPosition - newPanPosition);

        // Move the camera in world space
        Vector3 move = new Vector3(offset.x * panSpeed, 0, offset.y * panSpeed);
        transform.Translate(move, Space.World);

        lastPanPosition = newPanPosition;

       
    }

    void OrbitCamera(float angle)
    {

        transform.RotateAround(transform.position, Vector3.up, angle * rotationSpeed);

    }


  
}