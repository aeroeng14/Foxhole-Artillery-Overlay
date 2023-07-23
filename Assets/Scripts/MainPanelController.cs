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
//
public class MainPanelController : MonoBehaviour
{
    // define the panel GameObjects we will be working with
    [SerializeField] Canvas GameWindowCanvas;

    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsMenuPanel;
    [SerializeField] GameObject SupportingCanvas;   

    [SerializeField] GameObject gridMarker;
    [SerializeField] GameObject gunMarker;
    [SerializeField] GameObject targetMarker;
    [SerializeField] GameObject aimingLine;

    
    public void OpenMainPanel()
    {
        // check if the panel is not already open
        if (!MainMenuPanel.activeSelf)
        {
            MainMenuPanel.SetActive(true); // open the main panel
            SupportingCanvas.SetActive(true); // open the aiming box and wind gauge
        }
        // if it's already open, close the panel
        else
        {
            CalculateAiming calculateaiming_var = SupportingCanvas.GetComponentInChildren<CalculateAiming>();

            MainMenuPanel.SetActive(false);

            //
            // close any other UI elements open and reset their positions if need be
            //
            if (OptionsMenuPanel.activeSelf)
            {
                GameObject zz = GameObject.Find("OptionsPanelCanvas");
                zz.GetComponent<DropdownController>().reset_dropdowns();
                OptionsMenuPanel.SetActive(false); 
            }
            
            // reset wind using the method in that attached script
            WindGauge wind = SupportingCanvas.GetComponentInChildren<WindGauge>();
            wind.reset_wind_canvas();

            // reset the text on the az/distance text panel
            calculateaiming_var.reset_text();

            //close the SupportCanvas
            SupportingCanvas.SetActive(false);

            // close and reset the grid scale marker icon
            if (gridMarker.activeSelf) { gridMarker.SetActive(false); }
            gridMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;
            calculateaiming_var.reset_slider();

            // close and reset the gun marker icon
            if (gunMarker.activeSelf) { gunMarker.SetActive(false); }
            gunMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;

            // close and reset the target marker icon
            if (targetMarker.activeSelf) { targetMarker.SetActive(false); }
            targetMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;

            // turn off the gun-target vector line
            aimingLine.SetActive(false);
            
            
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
        // display the GameObject at the center of the screen so it can be mouse dragged
        if (!gridMarker.activeSelf)
        {
            gridMarker.SetActive(true);

            // get an initial pixel scale so the variable is filled
            CalculateAiming zz = SupportingCanvas.GetComponent<CalculateAiming>();
            zz.get_scale();
        }
        else
        {
            // reset position to center, rescale, and hide
            gridMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;
            gridMarker.GetComponent<RectTransform>().localScale = Vector3.one;
            gridMarker.SetActive(false);
        }
    }

    public void GunMarkerOpen()
    {
        // display the GameObject at the center of the screen so it can be mouse dragged
        if (!gunMarker.activeSelf)
        { 
            gunMarker.SetActive(true); 
        }
        else
        {
            // reset position to center and hide
            gunMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;
            gunMarker.SetActive(false);

            aimingLine.SetActive(false);
        }
    }

    public void TargetMarkerOpen()
    {
        // display the GameObject at the center of the screen so it can be mouse dragged
        if (!targetMarker.activeSelf)
        {
            targetMarker.SetActive(true);
        }
        else
        {
            // reset position to center and hide
            targetMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;
            targetMarker.SetActive(false);

            aimingLine.SetActive(false);
        }
    }
}
