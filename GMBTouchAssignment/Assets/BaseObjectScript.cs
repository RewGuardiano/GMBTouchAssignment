using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObjectScript : MonoBehaviour,ITouchable
{
    protected Renderer objectRenderer;

    protected virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();
    }

    public virtual void SelectToggle(bool selected)
    {
    }

    public virtual void MoveObject(Transform transform, Touch touch)
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
