using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanelOpener : MonoBehaviour
{
    // what game object are we accessing?
    public GameObject main_panel;
    public GameObject options_panel;

    // method to open the associated panel
    public void OpenMainPanel()
    {
        // check the panel status
        bool main_menu_is_open = main_panel.activeSelf;
        
        // check if there is a panel assigned and it isn't already open
        if(main_panel != null && !main_menu_is_open)
        {
            main_panel.SetActive(true);
            main_menu_is_open = true;
        }
        // if it's already open, close the panel
        else
        {
            main_panel.SetActive(false);
            main_menu_is_open = false;

            bool options_menu_is_open = options_panel.activeSelf;
            if (options_menu_is_open) { options_panel.SetActive(false);}
            
        }
    }

}
