using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : BaseObjectScript
{
    Renderer r;
    private Rigidbody rb;
    private bool hasStacked = false;

    private TowerGameScript gameScript;

    protected override void Start()
    {
        base.Start();
        r = GetComponent<Renderer>(); // Ensure Renderer is assigned]
        rb = GetComponent<Rigidbody>();


        // Check if Rigidbody exists, if not, add one
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>(); // Re-add Rigidbody if missing
            rb.useGravity = true;
           
        }
        gameScript = FindObjectOfType<TowerGameScript>(); // Reference the main game script
    }

    // Detect when the cube lands on another cube
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasStacked && collision.gameObject.CompareTag("Stackable"))
        {
            // Check if we are actually landing on top
            if (collision.contacts[0].normal.y > 0.5f) // Ensures it's a top-down collision
            {
                hasStacked = true; // Mark this cube as stacked
                gameScript.UpdateScore(); // Increase the score
            }
        }
    }


    public override void SelectToggle(bool selected)
    {
        if (selected)
        {
            ChangeColor(Color.yellow); // Change color when selected
        }
        else
        {
            ChangeColor(Color.white); // Change back when deselected
        }
    }

    public void ChangeColor(Color color)
    {
        r.material.color = color;
    }


    // This method moves the object based on touch
    public override void MoveObject(Transform transform, Touch touch)
    {
        
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(transform.position).z));
        transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z); // Keep the Z position the same
    }
}
