using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CalculateAiming : MonoBehaviour
{

    [SerializeField] GameObject gunMarker;
    [SerializeField] GameObject targetMarker;
    [SerializeField] GameObject AimLinePanel;

    public void calculate_aimpoint()
    {
        Vector3 gun_position, target_position, gun_target_vector;
        float azimuth_deg, distance_mag;
        
        // make sure both markers are visible before running calculations so the user isn't confused
        if (gunMarker.activeSelf && targetMarker.activeSelf)
        {
            AimLinePanel.SetActive(false);

            //float pixelScale = 10;// [m/pixel]

            // get the world pixel positions of the marker icons based on their pivot points
            gun_position = gunMarker.transform.position;
            target_position = targetMarker.transform.position;

            // find distance between them
            gun_target_vector = target_position - gun_position;
            distance_mag = gun_target_vector.magnitude;

            // calculate the azimuth between the aiming line and due N (Up in the canvas)
            azimuth_deg = Vector3.SignedAngle(gun_target_vector, transform.up, new Vector3(0, 0, 1));
            if (azimuth_deg < 0) { azimuth_deg += 360.0f; }

            
            // draw the line between them for visual aid
            if (gun_position != target_position) { draw_projectile_line(gun_position,distance_mag, gun_target_vector); }

            // convert meters/pixel!
            Debug.Log(azimuth_deg + ", " + distance_mag);
            //Debug.Log("Aim Here");


        }
        else { azimuth_deg = 0.0f; distance_mag = 0.0f;}


    }

    void draw_projectile_line(Vector3 gunOrigin, float linelength, Vector3 line_vec)
    {
        int panelwidth = 10;
        int panelheight;

        //float angle = Vector3.SignedAngle(transform.up, line_vec, new Vector3(0, 0, 1)) ;


        //AimLinePanel.GetComponent<RectTransform>().position = gunOrigin;
        //AimLinePanel.GetComponent<RectTransform>().Rotate(new Vector3 (0,0,angle));
        //Debug.Log(angle);
        ////AimLinePanel.GetComponent<RectTransform>().

        //AimLinePanel.SetActive(true);

    }


    //void get_wind()
    //{



    //}











}
