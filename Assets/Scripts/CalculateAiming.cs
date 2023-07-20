using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CalculateAiming : MonoBehaviour
{

    [SerializeField] GameObject gunMarker;
    [SerializeField] GameObject targetMarker;
    [SerializeField] GameObject AimLinePanel;
    [SerializeField] GameObject gridMarker;
    [SerializeField] GameObject scaleslider;
    public TMP_Text text_panel;

    float pixel_scale;

    void Start()
    {
        text_panel.text = "Azimuth: 0 deg         Distance: 0m";
    }

    public void reset_text()
    {
        text_panel.text = "Azimuth: 0 deg         Distance: 0m";
    }    

    public void reset_slider()
    {
        scaleslider.GetComponent<Slider>().value = 1.0f;
    }

    public void calculate_aimpoint()
    {
        Vector3 gun_position, target_position, gun_target_vector;
        float azimuth_deg, distance_mag_pixels;
        




        // make sure both markers are visible before running calculations so the user isn't confused
        if (gridMarker.activeSelf && gunMarker.activeSelf && targetMarker.activeSelf)
        {
            // turn the line invisible while the calculations are ongoing
            AimLinePanel.SetActive(false);

            //float pixelScale = 10;// [m/pixel]

            // get the world pixel positions of the marker icons based on their pivot points
            gun_position = gunMarker.transform.position;
            target_position = targetMarker.transform.position;

            // find distance between them
            gun_target_vector = target_position - gun_position;
            distance_mag_pixels = gun_target_vector.magnitude;

            // calculate the azimuth between the aiming line and due N (Up in the canvas)
            azimuth_deg = Vector3.SignedAngle(gun_target_vector, transform.up, new Vector3(0, 0, 1));

            // round the numbers
            distance_mag_pixels = Mathf.Round(distance_mag_pixels);
            azimuth_deg = Mathf.Round(azimuth_deg * 10.0f) / 10.0f;// X.X

            if (azimuth_deg < 0) { azimuth_deg += 360.0f; }


            // add in the wind offset to the target location based on what gun type is firing
            // (need options selection panel working for this)
            //
            //
            //
            //
            //
            //
            //

            // draw the line between the marker icons for visual aid
            if (gun_position != target_position) { draw_projectile_line(gun_target_vector); }

        }
        else { azimuth_deg = 0.0f; distance_mag_pixels = 0.0f;} // output zeros since there is no difference to calculate


        Debug.Log("pixel scale " + pixel_scale);
        Debug.Log("distance in pixels " + distance_mag_pixels);

        text_panel.text = "Azimuth: " + azimuth_deg + " deg         Distance: " + distance_mag_pixels*pixel_scale + "m";

    }

    void draw_projectile_line(Vector3 gun_target_vector)
    {
        float panel_vector_angle;
        Vector2 panel_line_length;

        // calculate the angle difference between the local Up axis for the panel and the gun-target vector
        panel_vector_angle = Vector3.SignedAngle(AimLinePanel.transform.up, gun_target_vector, new Vector3(0, 0, 1));

        // move the panel so it matches the gun origin
        AimLinePanel.GetComponent<RectTransform>().position = gunMarker.GetComponent<RectTransform>().position;

        // rotate the panel object by the angle difference
        AimLinePanel.GetComponent<RectTransform>().Rotate(new Vector3 (0,0,panel_vector_angle));

        // change the length of the line to match the distance between the markers
        panel_line_length = targetMarker.GetComponent<RectTransform>().position - gunMarker.GetComponent<RectTransform>().position;
        AimLinePanel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, panel_line_length.magnitude);

        // turn the line visible
        AimLinePanel.SetActive(true);
    }

    public float get_scale()
    {
        float foxhole_large_grid_m = 125.0f;
        
        // length of a side in pixels
        Vector2 scale_factor = gridMarker.GetComponent<RectTransform>().sizeDelta;

        // meters/pixel
        return pixel_scale = foxhole_large_grid_m/scale_factor.x;
    }

    //void get_wind()
    //{



    //}











}
