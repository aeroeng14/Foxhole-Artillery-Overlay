using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


// This MainPanelController class will contain all methods related to the MainMenuPanel GameObject
//
// Functionality includes:
//  -Trigger the opening/closing of the MainMenuPanel and close the OptionsMenuPanel if it is open too on Main close
//  -Spawning Prefab GameObjects in the GameWindowCanvas once their icon is clicked by the mouse
//
public class MainPanelController : MonoBehaviour
{
    // define the panel GameObjects we will be working with
    [SerializeField] GameObject GameWindowCanvas;
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsMenuPanel;
    [SerializeField] GameObject OptionsMenuCanvas;
    [SerializeField] GameObject SupportingCanvas;

    public void OpenMainPanel()
    {
        // check if the panel is not already open
        if (!MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(true); // open the main panel
            SupportingCanvas.SetActive(true); // open the aiming box and wind gauge
        }
        else // if it's already open, close the panel
        {
            // setting up some master variables first
            CalculateAiming calculateAiming_class = SupportingCanvas.GetComponentInChildren<CalculateAiming>();
            WindGauge wind_class = SupportingCanvas.GetComponentInChildren<WindGauge>();
            MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();


            MainMenuPanel.SetActive(false); // close the main panel

            // close any other UI elements open and reset their positions/scales/etc. if need be
            //
            if (OptionsMenuPanel.activeSelf)
            {
                OptionsMenuCanvas.GetComponent<DropdownController>().reset_dropdowns();
                OptionsMenuPanel.SetActive(false);
            }

            // reset wind using the method in that attached script (GameObject must be active to access the attached method)
            wind_class.reset_wind_canvas();

            // reset the text on the az/distance text panel
            calculateAiming_class.reset_text();


            //close the SupportCanvas
            SupportingCanvas.SetActive(false);

            // close and reset the grid scale marker icon
            if (marker_class.is_grid_marker_open())
            {
                marker_class.set_grid_marker_open(false);
                marker_class.set_grid_marker_position(Vector3.zero);
                marker_class.GetComponent<MarkerLocations>().reset_slider();
            }

            // close and reset the gun marker icon
            if (marker_class.is_gun_marker_open())
            {
                marker_class.set_gun_marker_open(false);
                marker_class.set_gun_marker_position(Vector3.zero);
            }

            // close and reset the target marker icon
            if (marker_class.is_target_marker_open())
            {
                GameWindowCanvas.GetComponent<MarkerLocations>().set_target_marker_open(false);
                GameWindowCanvas.GetComponent<MarkerLocations>().set_target_marker_position(Vector3.zero);
            }

            // turn off the gun-target vector line
            marker_class.set_aimline_open(false);
        }
    }

    public void OpenOptionsPanel()
    {
        // check if the panel is not already open
        if (!OptionsMenuPanel.activeSelf) { OptionsMenuPanel.SetActive(true); }
        // if it's already open, close the panel
        else { OptionsMenuPanel.SetActive(false); }
    }

    public void GridMarkerOpen()
    {
        MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();
        //CalculateAiming calculateAiming_class = SupportingCanvas.GetComponentInChildren<CalculateAiming>();

        // display the GameObject at the center of the screen so it can be mouse dragged
        if (!marker_class.is_grid_marker_open())
        {
            marker_class.set_grid_marker_open(true);

            // get an initial pixel scale so the variable is filled
            //marker_class.get_grid_marker_scale();
        }
        else
        {
            // reset position to center, rescale, and hide
            marker_class.set_grid_marker_position(Vector3.zero);
            marker_class.set_grid_marker_scale(Vector3.one);
            marker_class.set_grid_marker_open(false);
        }
    }

    public void GunMarkerOpen()
    {
        MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();

        // display the GameObject at the center of the screen so it can be mouse dragged
        if (!marker_class.is_gun_marker_open())
        {
            marker_class.set_gun_marker_open(true);
        }
        else
        {
            // reset position to center and hide
            marker_class.set_gun_marker_position(Vector3.zero);
            marker_class.set_gun_marker_open(false);
            marker_class.set_aimline_open(false);
        }
    }

    public void TargetMarkerOpen()
    {
        MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();

        // display the GameObject at the center of the screen so it can be mouse dragged
        if (!marker_class.is_target_marker_open())
        {
            marker_class.set_target_marker_open(true);
        }
        else
        {
            // reset position to center and hide
            marker_class.set_target_marker_position(Vector3.zero);
            marker_class.set_target_marker_open(false);
            marker_class.set_aimline_open(false);
        }
    }
}
