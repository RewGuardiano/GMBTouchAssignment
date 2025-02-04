using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    bool hasMoved = false;
    float timer = 0;
    float maxTimer = 0.5f;
    ManagerScript managerScript;
   
    
    void Start()
    {
        if (managerScript == null)
        {
            managerScript = FindObjectOfType<ManagerScript>();
        }

   
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            print(t.phase);
            print(t.position);

            timer += Time.deltaTime;
            switch (t.phase)
            {

                case TouchPhase.Began:
                    timer = 0;
                    
                    hasMoved = false;
                    break;

                case TouchPhase.Moved:
                    hasMoved = true;
                    break;
                    

                case TouchPhase.Ended:
                    if ((timer < maxTimer) && hasMoved)
                    {
                        managerScript.tapRegisteredAt(t.position);
                    }
                    break;
            }
            print(t.phase);
        }
        //Handles two finger touches on the screen for Scaling and Rotation.
        if (Input.touchCount == 2)
        {
            managerScript.OnObjectScaleAndRotate(Input.GetTouch(0), Input.GetTouch(1));
        }
    }
}
