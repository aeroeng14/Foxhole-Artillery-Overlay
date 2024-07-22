using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CalculateAiming : MonoBehaviour
{
    [SerializeField] GameObject GameWindowCanvas;
    [SerializeField] GameObject OptionsPanelCanvas;
    [SerializeField] GameObject WindCanvas;
    [SerializeField] GameObject dispersion_circle;

    public TMP_Text text_panel;

    float pixel_scale;
    int wind_tier;
    float wind_direction_deg;
    float wind_prev_deg;

    void Awake()
    {
        reset_text();

        GameWindowCanvas.GetComponent<MarkerLocations>().reset_slider();

        pixel_scale = 1f;
        wind_tier = 1;
        wind_direction_deg = 0f;
        wind_prev_deg = 0f;
    }

    public void reset_text()
    {
        text_panel.text = "Azimuth: 0 deg         Distance: 0m";
        UnityEngine.UI.Image aimingPanel = GetComponentInChildren<UnityEngine.UI.Image>();
        aimingPanel.color = Color.white;
    }

    public void calculate_aimpoint()
    {
        Vector3 gun_position, target_position, gun_target_vector_pixels, wind_vector_meters, aim_here_vector_meters;
        float azimuth_deg, distance_mag_meters, wind_offset_mag_meters, min_Range, max_Range, dispersion_m, desired_disp_circle_size_pixel;
        float wind_delta_deg;
        Quaternion wind_angle_rotation;
        int num_range_ticks;

        MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();

        //make sure markers are visible and have been moved before running calculations so the user isn't confused
        if (marker_class.is_grid_marker_open() && marker_class.is_gun_marker_open() && marker_class.is_target_marker_open() && marker_class.isMoved_gun && marker_class.isMoved_target)
        {
            // turn the line invisible while the calculations are ongoing
            marker_class.set_aimline_open(false);

            // get the world pixel positions of the marker icons based on their pivot points
            gun_position = marker_class.get_gun_marker_position();
            target_position = marker_class.get_target_marker_position();

            // find distance between them as a vector
            gun_target_vector_pixels = target_position - gun_position;

            // draw the line between the marker icons for visual aid
            if (gun_position != target_position && marker_class.isMoved_gun == true && marker_class.isMoved_target == true) { draw_projectile_line(gun_target_vector_pixels, gun_position, target_position); }

            // convert the distance in pixel to in-game meters
            pixel_scale = GameWindowCanvas.GetComponent<MarkerLocations>().get_grid_marker_scale();


            // **********************************************************
            // add in the wind offset to the target location based on what gun type is firing
            // **********************************************************
            wind_direction_deg = WindCanvas.GetComponent<WindGauge>().wind_direction;
            wind_tier = WindCanvas.GetComponent<WindGauge>().wind_strength;
            wind_delta_deg = wind_direction_deg - wind_prev_deg;

            // get the raw wind offset distance
            if (OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.Name != "None")
            {
                wind_offset_mag_meters = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.windTierBias[wind_tier - 1];
            }
            else {wind_offset_mag_meters = 0f;}

            // decompose the wind direction into vector components so we can add it easily
            wind_angle_rotation = Quaternion.Euler(0, 0, -wind_direction_deg); // the unit vector
            wind_vector_meters = wind_angle_rotation * new Vector3(0, 1, 0) * wind_offset_mag_meters;// multiplied by the offset from above            
            // **********************************************************


            // includes wind
            aim_here_vector_meters = gun_target_vector_pixels * pixel_scale + -wind_vector_meters;

            // calculate the distance along the new aiming vector
            distance_mag_meters = Mathf.Round(aim_here_vector_meters.magnitude*10f)/10f; // XXX.Xm format

            // calculate the azimuth between the aiming line and due N (Up in the canvas)
            azimuth_deg = Vector3.SignedAngle(aim_here_vector_meters, transform.up, new Vector3(0, 0, 1));
            if (azimuth_deg < 0) { azimuth_deg += 360.0f; }

            azimuth_deg = Mathf.Round(azimuth_deg * 10.0f) / 10.0f;// X.Xdeg format


            // **********************************************************
            // Draw the dispersion ellipse and f(range, wind orientation)
            // **********************************************************
            if (OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.Name != "None")
            {
                num_range_ticks = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.rangeTicks.Length;
                float[] range_array_m = new float[num_range_ticks];
                float[] dispersion_array_m = new float[num_range_ticks];
                for (int i = 0; i < num_range_ticks; i++)
                {
                    range_array_m[i] = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.rangeTicks[i];
                    dispersion_array_m[i] = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.baselineDispersion[i];
                }

                // get the dispersion for a platform in meters
                dispersion_m = interpolate(distance_mag_meters, range_array_m, dispersion_array_m, num_range_ticks);

            }
            else { dispersion_m = 0f; }

            // scale the dispersion circle based on the pixel_scale (if changed)
            if (dispersion_m != 0.0f)
            {
                // get the crosswise and in-track reduction
                float cross_trk_shrink = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.crossTrackReduction[wind_tier - 1];
                float in_trk_shrink = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.inTrackReduction[wind_tier - 1];

                // scale the dispersion circle by the per gun factor as decimal
                desired_disp_circle_size_pixel = dispersion_m / pixel_scale;
                dispersion_circle.GetComponent<RectTransform>().sizeDelta = new Vector2(desired_disp_circle_size_pixel* (1f-cross_trk_shrink), desired_disp_circle_size_pixel* (1f-in_trk_shrink));

                // rotate the dispersion circle so it "faces" into the wind (i.e. the wind affects the shot fall properly)
                dispersion_circle.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, -wind_delta_deg));

                // turn on the marker if it already isn't
                marker_class.set_dispersion_marker_open(true);
            }
            else { marker_class.set_dispersion_marker_open(false); } // make sure the dispersion marker is off because its size would be 0m



            //// ----
            //Debug.Log("Pixel Wind X: " + wind_vector_meters.x + " Pixel Wind Y: " + wind_vector_meters.y);
            //Debug.Log("Pixel Gun-Target Line Only X: " + gun_target_vector_pixels.x * pixel_scale + " Pixel Gun-Target Line Only Y: " + gun_target_vector_pixels.y * pixel_scale);
            //Debug.Log("Pixel Where to Aim w/wind X: " + aim_here_vector_meters.x + " Pixel Where to Aim w/wind Y: " + aim_here_vector_meters.y);
            //// ----
        }
        //else if (marker_class.is_grid_marker_open() && marker_class.is_target_marker_open())
        //{
        //         // convert the distance in pixel to in-game meters
        //         pixel_scale = GameWindowCanvas.GetComponent<MarkerLocations>().get_grid_marker_scale();

        //         // get the dispersion for a platform in meters
        //         dispersion_m = OptionsPanelCanvas.GetComponent<DropdownController>().dispersion;

        //         // scale the dispersion circle based on the pixel_scale (if changed)
        //         if (dispersion_m != 0.0f)
        //         {
        //             desired_disp_circle_size_pixel = dispersion_m / pixel_scale;
        //             dispersion_circle.GetComponent<RectTransform>().sizeDelta = new Vector2(desired_disp_circle_size_pixel, desired_disp_circle_size_pixel);
        //         }

        //         // output zeros since there is no difference to calculate if all three are not on screen
        //         azimuth_deg = 0.0f;
        //         distance_mag_meters = 0.0f;
        //}
        else
        {
            // output zeros since there is no difference to calculate if all three icons are not on screen
            // (makes user verify grid size matches map, and gun/target icons are where they want them)
            azimuth_deg = 0.0f;
            distance_mag_meters = 0.0f;
        }

        // print the calculation results to the text box
        text_panel.text = "Azimuth: " + azimuth_deg + " deg         Distance: " + distance_mag_meters + " m";

        // color the AimingPanel if in range or not, and if a weapon and type has been selected in the options drop down menus
        UnityEngine.UI.Image aimingPanel = GetComponentInChildren<UnityEngine.UI.Image>();
        if (OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.Name != "None" && distance_mag_meters > 0f)
        {
            min_Range = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.minRange;
            max_Range = OptionsPanelCanvas.GetComponent<DropdownController>().chosenPlatform.maxRange;

            if (distance_mag_meters >= min_Range && distance_mag_meters <= max_Range)
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
        
        // set the new wind direction as the previous for the next iteration
        wind_prev_deg = wind_direction_deg;
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
    
    float interpolate(float aiming_range_m, float[] x_interp_array, float[] y_interp_array, int array_size)
    {
        float start_interp_x = 0f, end_interp_x = 0f, start_interp_y = 0f, end_interp_y = 0f;
        int start_idx, end_idx;

        if (array_size > 2)
        {
            start_idx = 0;
            end_idx = 0;
            for (int i = 0; i < array_size-1; i++)
            {
                if(aiming_range_m > x_interp_array[i] && aiming_range_m <= x_interp_array[i+1])
                {
                    start_idx = i;
                    end_idx = i+1;
                }
                start_interp_x  = x_interp_array[start_idx];
                end_interp_x    = x_interp_array[end_idx];
                start_interp_y  = y_interp_array[start_idx];
                end_interp_y    = y_interp_array[end_idx];
            }
        }
        else // only 2 values in the arrays
        {
            start_interp_x  = x_interp_array[0];
            end_interp_x    = x_interp_array[1];
            start_interp_y  = y_interp_array[0];
            end_interp_y    = y_interp_array[1];
        }

        // linear interpolate using the calculated true aiming range, including wind bias, to find the shot dispersion at impact
        float interp_out_m = (start_interp_y * (end_interp_x - aiming_range_m) + end_interp_y * (aiming_range_m - start_interp_x)) / (end_interp_x - start_interp_x);

        // limit the result to the values in the json file
        if(interp_out_m < start_interp_y) { interp_out_m = start_interp_y; }
        if (interp_out_m > end_interp_y) { interp_out_m = end_interp_y; }

        return interp_out_m;
    }

}
