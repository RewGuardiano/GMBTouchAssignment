using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : BaseObjectScript
{
    Renderer r;

    protected override void Start()
    {
        r = GetComponent<Renderer>();
        base.Start();
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
