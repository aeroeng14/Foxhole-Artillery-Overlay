using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseApp : MonoBehaviour
{

    [SerializeField] GameObject HelpPanel;
    
    public void close_app()
    {
        Application.Quit();

    }    

    public void OpenHelpMenu()
    {
        // check if the panel is not already open
        if (!HelpPanel.activeSelf)
        {
            HelpPanel.SetActive(true); // open the help menu
        }
        else
        {
            HelpPanel.SetActive(false);
        }
    }
}
