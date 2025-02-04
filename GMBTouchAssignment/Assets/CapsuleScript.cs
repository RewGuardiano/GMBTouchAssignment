using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    Renderer r;
    void Start()
    {
        r = GetComponent<Renderer>();
    }
    public void SelectToggle(bool selected)
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

    public void MoveObject()
    {

    }
}
