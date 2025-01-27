using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    ITouchable selectedObject;


    void Start()
    {
    }
    void Update()
    {

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
                if (selectedObject != null)
                {
                    selectedObject.SelectToggle(false);
                }
                selectedObject = newObject;
                newObject.SelectToggle(true);

            }
            else
            {
                // No object hit
                if (selectedObject != null)
                {
                    selectedObject.SelectToggle(false);
                    selectedObject = null;
                }
            }
        }

    }
}
