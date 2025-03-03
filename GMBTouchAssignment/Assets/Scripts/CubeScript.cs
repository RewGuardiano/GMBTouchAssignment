using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : BaseObjectScript
{
    Renderer r;
    private Rigidbody rb;
    private bool hasStacked = false;
    private bool canMove = true;

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
                canMove = false;
                ChangeColor(Color.white);
                gameScript.UpdateScore(); // Increase the score
                gameScript.SpawnNewCube();


            }
        }
    }


    public override void SelectToggle(bool selected)
    {
        if (gameScript.IsGameOver()) return;//Disable selection if the game is over
        if (canMove)
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
    }

    public void ChangeColor(Color color)
    {
        r.material.color = color;
    }


    // This method moves the object based on touch
    public override void MoveObject(Transform transform, Touch touch)
    {
        if (!canMove || gameScript.IsGameOver()) return;
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(transform.position).z));
        transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z); // Keep the Z position the same

        
    }

    public override void ScaleObject(Touch t1, Touch t2)
    {
        // Do nothing (Disable scaling for cubes)
    }

    public override void RotateObject(Touch t1, Touch t2)
    {
        // Do nothing (Disable rotation for cubes)
    }

}
