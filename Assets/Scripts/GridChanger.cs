using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridChanger : MonoBehaviour
{
    [SerializeField] GameObject smallToggle;
    [SerializeField] GameObject largeToggle;
    [SerializeField] GameObject gridMarker;
    [SerializeField] Sprite smallGridImage;
    [SerializeField] Sprite largeGridImage;

    public bool grid_is_small;

    void Start()
    {
        grid_is_small = true;
    }

    public void ChangeGrid()
    {
        if (smallToggle.GetComponent<Toggle>().isOn == true)
        {
            grid_is_small = true;
            gridMarker.GetComponent<Image>().sprite = smallGridImage;
            Debug.Log("Now Small");
        }
        else if (largeToggle.GetComponent<Toggle>().isOn == true)
        {
            grid_is_small = false;
            gridMarker.GetComponent<Image>().sprite = largeGridImage;
            Debug.Log("Now Large");
        }
    }


}
