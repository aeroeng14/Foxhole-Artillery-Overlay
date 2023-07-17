using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanelOpener : MonoBehaviour
{
    // what game object are we accessing?
    public GameObject options_panel;

    // method to open the associated panel
    public void OpenOptionsPanel()
    {
        // check the panel status
        bool options_menu_is_open = options_panel.activeSelf;

        // check if there is a panel assigned and it isn't already open
        if (options_panel != null && !options_menu_is_open)
        {
            options_panel.SetActive(true);
            options_menu_is_open = true;
        }
        // if it's already open, close the panel
        else
        {
            options_panel.SetActive(false);
            options_menu_is_open = false;
        }
    }

}
