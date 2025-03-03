using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    ITouchable selectedObject;
    private Transform selectedTransform;
    private Vector2 startingTouchPosition;
    private CameraController cameraController;

    void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }
    void Update()
    {
        // Enable/Disable camera controls based on selection
        cameraController.enabled = selectedObject == null;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startingTouchPosition = touch.position;
                tapRegisteredAt(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (selectedTransform != null)
                {
                    OnTouchMove(touch);
                }
            }
        }
    }
    public void tapRegisteredAt(Vector2 tapPosition)
    {
        Ray r = Camera.main.ScreenPointToRay(tapPosition);
        RaycastHit info;
        if (Physics.Raycast(r, out info))
        {
            ITouchable newObject = info.collider.gameObject.GetComponent<ITouchable>();
            if (newObject != null)
            {
                if (selectedObject == newObject)
                {
                    selectedObject.SelectToggle(false);
                    selectedObject = null;
                    selectedTransform = null;
                }

                else
                {

                    if (selectedObject != null)
                    {
                        selectedObject.SelectToggle(false);
                    }
                    selectedObject = newObject;
                    selectedTransform = info.collider.transform;
                    newObject.SelectToggle(true);
                }

            }
            else
            {
             
                selectedObject?.SelectToggle(false);
                selectedObject = null;
                selectedTransform = null;
            }

        }
    }

    private void OnTouchMove(Touch touch)
    {
        if (selectedObject is ITouchable obj)
        {
            obj.MoveObject(selectedTransform, touch);
        }
    }
    public void OnObjectScaleAndRotate(Touch touch1, Touch touch2)
    {
        if (selectedObject is BaseObjectScript obj)
        {
            obj.ScaleObject(touch1, touch2);
            obj.RotateObject(touch1, touch2);
        }
    }
}
