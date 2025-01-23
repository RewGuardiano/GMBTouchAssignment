using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float strafeSpeed = 0.01f;
    public float rotationSpeed = 0.5f;
    public float zoomSpeed = 0.5f;

    private Transform cameraTransform;
    private Vector3 targetPosition;
    private float currentDistance;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        targetPosition = Vector3.zero; // Assuming we're orbiting around the world origin
        currentDistance = Vector3.Distance(cameraTransform.position, targetPosition);
    }

    public void HandleStrafe(Vector2 delta)
    {
        Vector3 right = cameraTransform.right;
        Vector3 up = cameraTransform.up;

        Vector3 strafe = (right * -delta.x + up * -delta.y) * strafeSpeed;
        targetPosition += strafe;
        cameraTransform.position += strafe;
    }

    public void HandleRotation(Vector2 rotationDelta, float zoomDelta)
    {
        // Rotation
        cameraTransform.RotateAround(targetPosition, Vector3.up, rotationDelta.x * rotationSpeed);
        cameraTransform.RotateAround(targetPosition, cameraTransform.right, -rotationDelta.y * rotationSpeed);

        // Zoom
        currentDistance = Mathf.Clamp(currentDistance - zoomDelta * zoomSpeed, 1f, 20f);
        cameraTransform.position = targetPosition - cameraTransform.forward * currentDistance;
    }
}
