using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
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
            changeColor(Color.red);
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
}
