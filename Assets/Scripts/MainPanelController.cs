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
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsMenuPanel;

    [SerializeField] Canvas GameWindowCanvas;

    //[SerializeField] GameObject gridMarker;
    [SerializeField] GameObject gunMarker;
    [SerializeField] GameObject targetMarker;
    

    public void OpenMainPanel()
    {
        // check the panel status
        bool main_menu_is_open = MainMenuPanel.activeSelf;

        // check if the panel is not already open
        if (!main_menu_is_open)
        {
            MainMenuPanel.SetActive(true);
            main_menu_is_open = true;
        }
        // if it's already open, close the panel
        else
        {
            MainMenuPanel.SetActive(false);
            main_menu_is_open = false;

            // close any other UI elements open and reset their positions if need be
            if (OptionsMenuPanel.activeSelf) { OptionsMenuPanel.SetActive(false); }

            //if (gridMarker.activeSelf) { gridMarker.SetActive(false); }
            //gridMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;

            if (gunMarker.activeSelf) { gunMarker.SetActive(false); }
            gunMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;
            
            if (targetMarker.activeSelf) { targetMarker.SetActive(false); }
            targetMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }

    public void OpenOptionsPanel()
    {
        // check the panel status
        bool options_menu_is_open = OptionsMenuPanel.activeSelf;

        // check if the panel is not already open
        if (!options_menu_is_open)
        {
            OptionsMenuPanel.SetActive(true);
            options_menu_is_open = true;
        }
        // if it's already open, close the panel
        else
        {
            OptionsMenuPanel.SetActive(false);
            options_menu_is_open = false;
        }
    }

    //public void GridMarkerOpen()
    //{
    //    // display the GameObject at the center of the screen so it can be mouse dragged
    //    if (!gridMarker.activeSelf)
    //    {
    //        gridMarker.SetActive(true);
    //    }
    //    else
    //    {
    //        // reset position to center and hide
    //        gridMarker.GetComponent<RectTransform>().localPosition = Vector3.zero;
    //        gridMarker.SetActive(false);
    //    }
    //}

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
        }
    }















    //public void GridInstantiate()
    //{
    //    Debug.Log("Grid button pressed, spawn GameObject when there's a Prefab for it.");

    //    //GameObject markericon;

    //    //// check if a marker exists in the GameWindowCanvas and destroy if needed then instantiate a new one
    //    //markericon = GameObject.Find("GridMarkerObject");
    //    //if (markericon != null) { Destroy(markericon); }

    //    //// instantiate a new one and set it's transform to the center of the canvas after parenting it
    //    //markericon = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("GameWindowCanvas").transform);
    //    //markericon.name = "GridMarkerObject";
    //    //markericon.GetComponent<RectTransform>().pivot = new Vector2 (0.0f, 0.0f);
    //    //markericon.GetComponent<RectTransform>().localPosition = Vector3.zero;
    //}

    //public void GunInstantiate()
    //{
    //    //Debug.Log("Gun marker button pressed, spawn GameObject.");

    //    GameObject markericon;

    //    // check if a marker exists in the GameWindowCanvas and destroy if needed then instantiate a new one
    //    markericon = GameObject.Find("GunMarkerObject");
    //    if (markericon != null) { Destroy(markericon); }

    //    // instantiate a new one and set it's transform to the center of the canvas after parenting it
    //    markericon = Instantiate(gunPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("GameWindowCanvas").transform);
    //    markericon.name = "GunMarkerObject";
    //    markericon.GetComponent<RectTransform>().pivot = new Vector2 (0.0f, 0.0f);
    //    markericon.GetComponent<RectTransform>().localPosition = Vector3.zero;



    //}

    //public void TargetInstantiate()
    //{
    //    //Debug.Log("Target marker button pressed, spawn GameObject.");

    //    GameObject markericon;

    //    // check if a marker exists in the GameWindowCanvas and destroy if needed then instantiate a new one
    //    markericon = GameObject.Find("TargetMarkerObject");
    //    if (markericon != null) { Destroy(markericon); }

    //    // instantiate a new one and set it's transform to the center of the canvas after parenting it
    //    markericon = Instantiate(targetPrefab, Vector3.zero, Quaternion.identity, GameObject.Find("GameWindowCanvas").transform);
    //    markericon.name = "TargetMarkerObject";
    //    markericon.GetComponent<RectTransform>().pivot = new Vector2 (0.0f, 0.0f);
    //    markericon.GetComponent<RectTransform>().localPosition = Vector3.zero;
    //}
}
