using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : BaseObjectScript
{
    private Vector3 initialWorldPosition; // Stores the initial touch position in world coordinates
    private float touchSensitivity = 0.01f;

    Renderer r;
    protected override void Start()
    {
        base.Start();
        r = GetComponent<Renderer>();
    }
    public override void SelectToggle(bool selected)
    {
        if (selected)
        {
            changeColor(Color.red);
        }
        else
            changeColor(Color.white);

    }

    public void changeColor(Color color)
    {
        r.material.color = color;
    }

    public override void MoveObject(Transform transform, Touch touch)
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        // Convert touch delta to world coordinates
        Vector3 touchDelta = new Vector3(touch.deltaPosition.x, touch.deltaPosition.y, 0) * touchSensitivity;

        // Calculate the direction from the camera to the sphere
        Vector3 direction = transform.position - cameraPosition;

        // Rotate the direction based on touch delta
        direction = Quaternion.Euler(-touchDelta.y, touchDelta.x, 0) * direction;

        // Update the sphere's position
        transform.position = cameraPosition + direction;
    }
}
