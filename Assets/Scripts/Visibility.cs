using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Visibility : MonoBehaviour
{
    [SerializeField] Sprite eyeImage;
    [SerializeField] Sprite noeyeImage;

    public bool ui_is_hidden;

    void Start()
    {
        ui_is_hidden = false;
        this.GetComponent<Image>().sprite = eyeImage;
    }

    public void ChangeVis()
    {
        MarkerLocations marker_class = GameObject.Find("GameWindowCanvas").GetComponent<MarkerLocations>();

        if (ui_is_hidden == false)
        {
            ui_is_hidden = true;
            this.GetComponent<Image>().sprite = noeyeImage;

            marker_class.set_grid_marker_open(false);
            marker_class.set_gun_marker_open(false);
            marker_class.set_target_marker_open(false);
            marker_class.set_dispersion_marker_open(false);
            marker_class.set_aimline_open(false);
        }
        else if (ui_is_hidden == true)
        {
            ui_is_hidden = false;
            this.GetComponent<Image>().sprite = eyeImage;

            if (marker_class.isMoved_grid == true) { marker_class.set_grid_marker_open(true); }
            if (marker_class.isMoved_gun == true) { marker_class.set_gun_marker_open(true); }
            if (marker_class.isMoved_target == true) { marker_class.set_target_marker_open(true); }
            //if (marker_class.isMoved_target == true && GameObject.Find("OptionsPanelCanvas").GetComponent<DropdownController>().dispersion != 0f) { marker_class.set_dispersion_marker_open(true); }
            if (marker_class.isMoved_gun == true && marker_class.isMoved_target == true) { marker_class.set_aimline_open(true); }
        }
    }

    public void visReset()
    {
        Start();
    }
}
