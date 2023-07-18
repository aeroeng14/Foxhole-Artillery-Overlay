using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIObject : MonoBehaviour
{
    // Add this script to the moveable object
    public RectTransform rectTransform;
    Vector3 offset;
    public void GetOffset()
    {
        offset = rectTransform.position - Input.mousePosition;
    }

    public void MoveObject()
    {
        rectTransform.position = Input.mousePosition + offset;
    }
}
