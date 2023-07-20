using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleGrid : MonoBehaviour
{

    [SerializeField] GameObject scaleSlider;

    public void scaleicon()
    {
        Vector2 grid_scale;
        float scale_factor;

        grid_scale = GetComponent<RectTransform>().sizeDelta;
        scale_factor = scaleSlider.GetComponent<Slider>().value;

        // apply the scale factor
        GetComponent<RectTransform>().sizeDelta = new Vector2 (grid_scale.x*scale_factor, grid_scale.y*scale_factor);

        // reset the slider after applying the new scale so it's easier to manipulate each time
        scaleSlider.GetComponent<Slider>().value = 1.0f;
    }
}
