using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchable
{
    void SelectToggle(bool isSelected);

    void MoveObject(Transform transform, Touch touch);

}
