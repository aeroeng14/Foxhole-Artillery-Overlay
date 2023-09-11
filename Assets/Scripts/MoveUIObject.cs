using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUIObject : MonoBehaviour
{
    // Add this script to the moveable object
    public RectTransform rectTransform;
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

        // flag if the icon has ever been moved from its starting location
        if(this.gameObject.name == "GunMarkerIcon"){GameObject.Find("GameWindowCanvas").GetComponent<MarkerLocations>().isMoved_gun = true;}
        else {GameObject.Find("GameWindowCanvas").GetComponent<MarkerLocations>().isMoved_grid = true;}
    }
}
