using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class CalculateAiming : MonoBehaviour
{
    [SerializeField] GameObject GameWindowCanvas;
    [SerializeField] GameObject OptionsPanelCanvas;
    [SerializeField] GameObject WindCanvas;

    public TMP_Text text_panel;

    float pixel_scale;
    int wind_tier;
    float wind_direction_deg;

    void Awake()
    {
        reset_text();

        GameWindowCanvas.GetComponent<MarkerLocations>().reset_slider();

        pixel_scale = 1f;
        wind_tier = 1;
        wind_direction_deg = 0.0f;
    }

    public void reset_text()
    {
        text_panel.text = "Azimuth: 0 deg         Distance: 0m";
        Image aimingPanel = GetComponentInChildren<Image>();
        aimingPanel.color = Color.white;
    }

    public void calculate_aimpoint()
    {
        Vector3 gun_position, target_position, gun_target_vector, wind_vector, aim_here_vector;
        float azimuth_deg, distance_mag_pixels, distance_mag_m, wind_offset_mag, min_Range, max_Range;
        Quaternion wind_angle_rotation;
        int gun_type;

        MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();

        //make sure both markers are visible before running calculations so the user isn't confused
        if (marker_class.is_grid_marker_open() && marker_class.is_gun_marker_open() && marker_class.is_target_marker_open())
        {
            // turn the line invisible while the calculations are ongoing
            marker_class.set_aimline_open(false);

            // get the world pixel positions of the marker icons based on their pivot points
            gun_position = marker_class.get_gun_marker_position();
            target_position = marker_class.get_target_marker_position();

            // find distance between them as a vector
            gun_target_vector = target_position - gun_position;

            // draw the line between the marker icons for visual aid
            if (gun_position != target_position) { draw_projectile_line(gun_target_vector, gun_position, target_position); }

            //
            // add in the wind offset to the target location based on what gun type is firing
            //
            // ----------------------------------------------------------------------------------------
            wind_direction_deg = WindCanvas.GetComponent<WindGauge>().wind_direction;
            wind_tier = WindCanvas.GetComponent<WindGauge>().wind_strength;

            // get the raw wind offset distance depending on the type of gun (120 vs 150, etc.)
            gun_type = OptionsPanelCanvas.GetComponent<DropdownController>().gunType;
            wind_offset_mag = calc_wind_offset(gun_type, wind_tier);

            // decompose the wind direction into vector components so we can add it easily
            wind_angle_rotation = Quaternion.Euler(0, 0, -wind_direction_deg); // the unit vector
            wind_vector = wind_angle_rotation * new Vector3(0,1,0) * wind_offset_mag;// multiplied by the offset from above

            aim_here_vector = gun_target_vector + -wind_vector;

            // ----
            //Debug.Log("Pixel Wind X: " + wind_vector.x + " Pixel Wind Y: " + wind_vector.y);
            //Debug.Log("Pixel Gun-Target Line Only X: " + gun_target_vector.x + " Pixel Gun-Target Line Only Y: " + gun_target_vector.y);
            //Debug.Log("Pixel Where to Aim w/wind X: " + aim_here_vector.x + " Pixel Where to Aim w/wind Y: " + aim_here_vector.y);
            // ----
            // ----------------------------------------------------------------------------------------

            // calculate the distance along the new aiming vector
            distance_mag_pixels = aim_here_vector.magnitude;

            // calculate the azimuth between the aiming line and due N (Up in the canvas)
            azimuth_deg = Vector3.SignedAngle(aim_here_vector, transform.up, new Vector3(0, 0, 1));
            if (azimuth_deg < 0) { azimuth_deg += 360.0f; }
        }
        else 
        {
            // output zeros since there is no difference to calculate if all three are not on screen
            azimuth_deg = 0.0f; 
            distance_mag_pixels = 0.0f;
        } 
        
        // convert the distance in pixel to in-game meters
        pixel_scale = GameWindowCanvas.GetComponent<MarkerLocations>().get_grid_marker_scale();

        distance_mag_m = Mathf.Round(distance_mag_pixels * pixel_scale);
        azimuth_deg = Mathf.Round(azimuth_deg * 10.0f) / 10.0f;// X.Xdeg format

        // print the results to the text box
        text_panel.text = "Azimuth: " + azimuth_deg + " deg         Distance: " + distance_mag_m + "m";

        // color the AimingPanel if in range or not if a gun has been selected in the options drop down menus
        Image aimingPanel = GetComponentInChildren<Image>();
        if (OptionsPanelCanvas.GetComponent<DropdownController>().gun != 0 && distance_mag_m > 0f)
        {
            min_Range = OptionsPanelCanvas.GetComponent<DropdownController>().minRange;
            max_Range = OptionsPanelCanvas.GetComponent<DropdownController>().maxRange;

            if (distance_mag_m >= min_Range && distance_mag_m <= max_Range)
            {
                aimingPanel.color = Color.green;
                //aimingPanel.color = new Color(0f,1f,0f,0.5f);
            }
            else
            {
                aimingPanel.color = Color.red;
                //aimingPanel.color = new Color(1f,0f,0f,0.5f);
            }


        }
        else
        {
            aimingPanel.color = Color.white;
        }

    }

    void draw_projectile_line(Vector3 gun_target_vector, Vector3 gun_position, Vector3 target_position)
    {
        float panel_vector_angle;
        Vector2 panel_line_length;
        Vector3 aimline_transform_up;

        MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();

        // calculate the angle difference between the local Up axis for the panel and the gun-target vector
        aimline_transform_up = marker_class.GetComponent<MarkerLocations>().get_aimline_up_transform();
        panel_vector_angle = Vector3.SignedAngle(aimline_transform_up, gun_target_vector, new Vector3(0, 0, 1));

        // move the panel so it matches the gun origin
        marker_class.set_aimline_position(marker_class.get_gun_marker_position());

        // rotate the panel object by the angle difference
        marker_class.rotate_aimline_panel(new Vector3(0, 0, panel_vector_angle));

        // change the length of the line to match the distance between the markers
        panel_line_length = target_position - gun_position;
        marker_class.set_aimline_panel_length(panel_line_length.magnitude);

        // turn the line visible
        marker_class.set_aimline_open(true);
    }
    
    float calc_wind_offset(int gun_type, int wind_tier)
    {
        float wind_offset_mag = 0.0f; // initialize with

        // insert equations/constants below for each wind tier once I know them in the future for
        // each arty type
        switch (gun_type)
        {
            case 0: // no gun_type selected and independent of wind
                wind_offset_mag = 0.0f;
                break;
            case 1: // mortars
                switch (wind_tier)
                {
                    case 1:
                        wind_offset_mag = 0.0f;
                        break;
                    case 2:
                        wind_offset_mag = 0.0f;
                        break;
                    case 3:
                        wind_offset_mag = 0.0f;
                        break;
                    case 4:
                        wind_offset_mag = 0.0f;
                        break;
                    case 5:
                        wind_offset_mag = 0.0f;
                        break;
                }
                break;
            case 2: // gunboats
                switch (wind_tier)
                {
                    case 1:
                        wind_offset_mag = 10.0f;
                        break;
                    case 2:
                        wind_offset_mag = 20.0f;
                        break;
                    case 3:
                        wind_offset_mag = 30.0f;
                        break;
                    case 4:
                        wind_offset_mag = 40.0f;
                        break;
                    case 5:
                        wind_offset_mag = 50.0f;
                        break;
                }
                break;
            case 3: // 120mm
                switch (wind_tier)
                {
                    case 1:
                        wind_offset_mag = 25.0f;
                        break;
                    case 2:
                        wind_offset_mag = 50.0f;
                        break;
                    case 3:
                        wind_offset_mag = 75.0f;
                        break;
                    case 4:
                        wind_offset_mag = 100.0f;
                        break;
                    case 5:
                        wind_offset_mag = 125.0f;
                        break;
                }
                break;
            case 4: // 150mm
                switch (wind_tier)
                {
                    case 1:
                        wind_offset_mag = 25.0f;
                        break;
                    case 2:
                        wind_offset_mag = 50.0f;
                        break;
                    case 3:
                        wind_offset_mag = 75.0f;
                        break;
                    case 4:
                        wind_offset_mag = 100.0f;
                        break;
                    case 5:
                        wind_offset_mag = 125.0f;
                        break;
                }
                break;
            case 5: // 300mm
                switch (wind_tier)
                {
                    case 1:
                        wind_offset_mag = 50.0f;
                        break;
                    case 2:
                        wind_offset_mag = 100.0f;
                        break;
                    case 3:
                        wind_offset_mag = 150.0f;
                        break;
                    case 4:
                        wind_offset_mag = 200.0f;
                        break;
                    case 5:
                        wind_offset_mag = 250.0f;
                        break;
                }
                break;
            case 6: // rockets
                switch (wind_tier)
                {
                    case 1:
                        wind_offset_mag = 0.0f;
                        break;
                    case 2:
                        wind_offset_mag = 0.0f;
                        break;
                    case 3:
                        wind_offset_mag = 0.0f;
                        break;
                    case 4:
                        wind_offset_mag = 0.0f;
                        break;
                    case 5:
                        wind_offset_mag = 0.0f;
                        break;
                }
                break;
        }

        return wind_offset_mag;
    }
}
