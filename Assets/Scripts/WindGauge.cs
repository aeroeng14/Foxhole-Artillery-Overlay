using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGauge : MonoBehaviour
{
    [SerializeField] GameObject wind_t1;
    [SerializeField] GameObject wind_t2;
    [SerializeField] GameObject wind_t3;
    [SerializeField] GameObject wind_t4;
    [SerializeField] GameObject wind_gauge;

    // accessed by calculate_aimpoint() in CalculateAiming.cs
    public int wind_strength;
    public float wind_direction;

    void Awake()
    {
        wind_strength = 1;// minimum wind value
        wind_direction = 0.0f; // due N to start with
    }

    public void increment_wind()
    {
        // turn the various scaled images on and off depending on the current wind tier
        switch (wind_strength)
        {
            case 1: // always start with T1 wind active in the UI
                wind_strength++;
                wind_t1.SetActive(false);
                wind_t2.SetActive(true);
                break;
            case 2:
                wind_strength++;
                wind_t2.SetActive(false);
                wind_t3.SetActive(true);
                break;
            case 3:
                wind_strength++;
                wind_t3.SetActive(false);
                wind_t4.SetActive(true);
                break;
            case 4:
                wind_strength = 1;
                wind_t4.SetActive(false);
                wind_t1.SetActive(true);
                break;
        }
    }

    public void rotate_windgauge()
    {
        Vector3 mouse_vane_offset;
        Vector3 arrow_forward;
        arrow_forward = wind_gauge.transform.up; // (0,1,0)
        float angle_difference;

        // positions are in world space, not canvas
        mouse_vane_offset = Input.mousePosition - wind_gauge.GetComponent<RectTransform>().position;

        // get the difference between the vectors in degrees
        angle_difference = Vector3.SignedAngle(arrow_forward, mouse_vane_offset, new Vector3(0, 0, 1));

        // rotate the image about its pivot point
        wind_gauge.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, angle_difference));

        // record the final wind vane rotation in case it's needed in the calculations
        wind_direction = 360.0f - wind_gauge.GetComponent<RectTransform>().eulerAngles.z;                    
    }
  
    public void reset_wind_canvas()
    {
        Vector3 current_rotation;

        // how is the wind vane currently rotated
        current_rotation = wind_gauge.GetComponent<RectTransform>().rotation.eulerAngles;

        // rotate the image back about its pivot point
        wind_gauge.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -current_rotation[2]));

        // reset the wind tiers
        wind_strength = 1;
        wind_t1.SetActive(true);
        wind_t2.SetActive(false);
        wind_t3.SetActive(false);
        wind_t4.SetActive(false);
    }
}
