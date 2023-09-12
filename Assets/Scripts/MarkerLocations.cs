using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MarkerLocations : MonoBehaviour
{
    
    [SerializeField] GameObject gunMarker;
    [SerializeField] GameObject targetMarker;
    [SerializeField] GameObject gridMarker;
    [SerializeField] GameObject aimLine;
    [SerializeField] GameObject dispersion_circle;

    // global variables checking if the icon has ever been moved or not from its original position on the canvas
    public bool isMoved_gun, isMoved_target, isMoved_grid;

    private void Start()
    {
        isMoved_grid = false;
        isMoved_gun = false;
        isMoved_target = false;
    }

    //
    // Gets attributes of the marker icons for access by other class methods
    //
    public Vector3 get_gun_marker_position()
    {
        return gunMarker.transform.position;
    }

    public Vector3 get_target_marker_position()
    {
        return targetMarker.transform.position;
    }

    public Vector3 get_dispersion_marker_position()
    {
        return dispersion_circle.transform.position;
    }

    public Vector3 get_aimline_position()
    {
        return aimLine.transform.position;
    }

    public Vector3 get_aimline_up_transform()
    {
        return aimLine.transform.up;
    }
    
    public float get_grid_marker_scale()
    {
        float return_scale_m_per_pix, foxhole_small_grid_m_x = 125f, foxhole_large_grid_m_x = 2197f;
        Vector2 grid_size_pixels;

        // length of a grid side in pixels
        grid_size_pixels = gridMarker.GetComponent<RectTransform>().sizeDelta;

        if (GameObject.Find("OptionsPanelCanvas").GetComponent<GridChanger>().grid_is_small) { return_scale_m_per_pix = foxhole_small_grid_m_x / grid_size_pixels.x; }
        else { return_scale_m_per_pix = foxhole_large_grid_m_x / grid_size_pixels.x; };

        return return_scale_m_per_pix;
    }

    //
    // Sets attributes of the marker icons that are assessed by other methods
    //
    public void set_gun_marker_position(Vector3 position)
    {
        gunMarker.transform.localPosition = position;
    }

    public void set_target_marker_position(Vector3 position)
    {
        targetMarker.transform.localPosition = position;
    }

    public void set_dispersion_marker_position(Vector3 position)
    {
        dispersion_circle.transform.localPosition = position;
    }

    public void set_grid_marker_position(Vector3 position)
    {
        gridMarker.transform.localPosition = position;
    }

    public void set_aimline_position(Vector3 position)
    {
        aimLine.GetComponent<RectTransform>().position = position;
    }

    public void set_dispersion_marker_scale(Vector2 scale)
    {
        dispersion_circle.GetComponent<RectTransform>().sizeDelta = scale;
    }

    public void set_grid_marker_scale(Vector2 scale)
    {
        gridMarker.GetComponent<RectTransform>().sizeDelta = scale;
    }

    public void rotate_aimline_panel(Vector3 rotation)
    {
        aimLine.GetComponent<RectTransform>().Rotate(rotation);
    }

    public void set_aimline_panel_length(float line_length) 
    {
        aimLine.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, line_length);
    }

    //
    // Checks if the marker icons are visible or not
    //
    public bool is_gun_marker_open()
    {
        return gunMarker.activeSelf;
    }

    public bool is_target_marker_open()
    {
        return targetMarker.activeSelf;
    }

    public bool is_grid_marker_open()
    {
        return gridMarker.activeSelf;
    }

    //
    // Sets if the marker icons are visible or not
    //
    public void set_gun_marker_open(bool flag)
    {
        gunMarker.SetActive(flag);
    }

    public void set_target_marker_open(bool flag)
    {
        targetMarker.SetActive(flag);
    }

    public void set_dispersion_marker_open(bool flag)
    {
        dispersion_circle.SetActive(flag);
    }

    public void set_grid_marker_open(bool flag)
    {
        gridMarker.SetActive(flag);
    }

    public void set_aimline_open(bool flag)
    {
        aimLine.SetActive(flag);
    }

    //
    // General reset
    //
    public void reset_slider()
    {
        gridMarker.GetComponentInChildren<UnityEngine.UI.Slider>().value = 1.0f;
    }
}
