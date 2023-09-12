using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIObjectTarget : MonoBehaviour
{
    // Add this script to the moveable object
    public RectTransform rectTransform;
    public RectTransform dispersion_rectTransform;
    Vector3 position_offset_from_mouse;
    
    // how far away is the mouse input position from the pivot point of the icon
    public void GetOffset()
    {
        position_offset_from_mouse = rectTransform.position - Input.mousePosition;
    }

    // translate the icon to be the same position as the mouse, including the original offset between the two
    public void MoveObject()
    {
        rectTransform.position = Input.mousePosition + position_offset_from_mouse;
        dispersion_rectTransform.position = Input.mousePosition + position_offset_from_mouse; // add in the dispersion circle too since I want the movement parented
                                                                                              // but cannot make it a child otherwise it renders on top of the marker

        // flag if the icon has ever been moved from its starting location
        GameObject.Find("GameWindowCanvas").GetComponent<MarkerLocations>().isMoved_target = true;
    }
}
