using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : BaseObjectScript
{

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
            changeColor(Color.blue);
        }
        else
            changeColor(Color.white);

    }

    public void changeColor(Color color)
    {
        r.material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void MoveObject(Transform transform, Touch touch)
    {
        //touch position on screen into a ray to detect the users touoch on the ground.
        Ray ray = Camera.main.ScreenPointToRay(transform.position);

        // layer mask for detecting the ground, so preventing the ray from hitting objects that is not the ground.
        int maskGroundLayer = LayerMask.GetMask("Ground");

        RaycastHit hit;

        //casting a ray from the camera towards where the user touched. 
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, maskGroundLayer))
        {
            //position where the ray hit the ground. 
            Vector3 touchPosition = hit.point;


            Collider collider = transform.GetComponent<Collider>();
            if (collider != null)
            {
                // retrieving half of the height of the object 
                float offsetHeight = collider.bounds.extents.y;
                //preventing the object from sinking below the ground. 
                transform.position = new Vector3(touchPosition.x, touchPosition.y + offsetHeight, touchPosition.z);

            }
        } 


    }
}
