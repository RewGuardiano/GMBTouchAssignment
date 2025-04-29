using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderScript : BaseObjectScript
{
    Renderer r;
  
  

    protected override void Start()
    {
        base.Start();
        r = GetComponent<Renderer>();
    }

 
    

    public override void SelectToggle(bool selected)
    {

        {
            if (selected)
            {
                ChangeColor(Color.yellow);
            }
            else
            {
                ChangeColor(Color.white);
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

        // Calculate the touch position in world space
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.WorldToScreenPoint(transform.position).z));

        
        transform.position = new Vector3(touchPosition.x, touchPosition.y,touchPosition.z);
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