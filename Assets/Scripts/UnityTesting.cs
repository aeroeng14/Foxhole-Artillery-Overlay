using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UnityTesting : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    //// Figure out how to get the canvas location of a UI element (here a GPS marker icon)
    //[SerializeField] GameObject Canvas;
    //[SerializeField] GameObject GunObject;

    //void Start()
    //{
    //    Vector3 marker_position;

    //    marker_position = GunObject.GetComponent<RectTransform>().anchoredPosition;
    //    Debug.Log("Marker location (at pivot point): " + marker_position[0] + ", " + marker_position[1] + ", " + marker_position[2]);
    //}

    //--------------------------------------------------------------------------------
    //// Figure out how to move GameObject to follow the mouse input
    //[SerializeField] GameObject ArrowIcon;

    //Vector3 offset;

    //public void GetOffset()
    //{
    //    // positions are in world space, not canvas
    //    offset = ArrowIcon.GetComponent<RectTransform>().position - Input.mousePosition;
    //}

    //public void MoveArrow()
    //{
    //    ArrowIcon.GetComponent<RectTransform>().position = Input.mousePosition + offset;
    //}

    ////--------------------------------------------------------------------------------
    //// Figure out how to rotate a GameObject to follow the mouse input
    //[SerializeField] GameObject VaneIcon;

    //public void RotateArrow()
    //{
    //    Vector3 mouse_vane_offset;
    //    Vector3 arrow_forward;
    //    arrow_forward = VaneIcon.transform.up; // (0,1,0)

    //    // positions are in world space, not canvas
    //    mouse_vane_offset = Input.mousePosition - VaneIcon.GetComponent<RectTransform>().position;

    //    float angle_difference = Vector3.SignedAngle(arrow_forward, mouse_vane_offset, new Vector3(0, 0, 1));

    //    VaneIcon.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, angle_difference));
    //}

    //--------------------------------------------------------------------------------

}
