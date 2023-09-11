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
        // check if the main panel is not already open
        if (!MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(true); // open the main panel
            SupportingCanvas.SetActive(true); // open the aiming box and wind gauge
        }
        else // if it's already open, close everything and reset
        {
            // setting up some master variables first
            CalculateAiming calculateAiming_class = SupportingCanvas.GetComponentInChildren<CalculateAiming>();
            WindGauge wind_class = SupportingCanvas.GetComponentInChildren<WindGauge>();
            MarkerLocations marker_class = GameWindowCanvas.GetComponent<MarkerLocations>();

            MainMenuPanel.SetActive(false); // close the main panel

            // close any other UI elements open and reset their positions/scales/etc. if need be
            //
            // ----------------------------------------------------------------------------------------
            
            if (OptionsMenuPanel.activeSelf)
            {
                OptionsMenuPanel.SetActive(false);
            }

            // reset the dropdown menus
            OptionsMenuCanvas.GetComponent<DropdownController>().reset_dropdowns();

            // reset wind using the method in that attached script (GameObject must be active to access the attached method)
            wind_class.reset_wind_canvas();

            // reset the text on the az/distance text panel
            calculateAiming_class.reset_text();

            //close the SupportCanvas
            SupportingCanvas.SetActive(false);

            // close and reset the grid scale marker icon
            marker_class.isMoved_grid = false;
            marker_class.set_grid_marker_open(false);
            marker_class.set_grid_marker_position(Vector3.zero);
            marker_class.GetComponent<MarkerLocations>().reset_slider();

            // close and reset the gun marker icon
            marker_class.isMoved_gun = false;
            marker_class.set_gun_marker_open(false);
            marker_class.set_gun_marker_position(Vector3.zero);

            // close and reset the target marker icon and attached dispersion circle
            marker_class.isMoved_target = false;
            marker_class.set_target_marker_open(false);
            marker_class.set_target_marker_position(Vector3.zero);
            marker_class.set_dispersion_marker_open(false);
            marker_class.set_dispersion_marker_position(Vector3.zero);
            marker_class.set_dispersion_marker_scale(new Vector2(100.0f, 100.0f));
            
            // turn off the gun-target vector line
            marker_class.set_aimline_open(false);

            // reset visibility boolean and button sprite
            GameObject.Find("HideIconsButton").GetComponent<Visibility>().visReset(); 
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

        // display the GameObject at the center of the screen so it can be mouse dragged
        if (!marker_class.is_grid_marker_open())
        {
            marker_class.set_grid_marker_open(true);
        }
        else
        {
            // reset position to center, rescale, and hide
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

            if (OptionsMenuCanvas.GetComponent<DropdownController>().dispersion != 0.0)
            {
                marker_class.set_dispersion_marker_open(true);
            }
            else
            {
                marker_class.set_dispersion_marker_open(false); // make sure it's closed and definitely not open
            }
        }
        else
        {
            // reset position to center and hide
            marker_class.set_target_marker_open(false);
            marker_class.set_aimline_open(false);

            marker_class.set_dispersion_marker_open(false);
        }
    }
}
