using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGauge : MonoBehaviour
{
    [SerializeField] GameObject wind_t1;
    [SerializeField] GameObject wind_t2;
    [SerializeField] GameObject wind_t3;
    [SerializeField] GameObject wind_t4;
    [SerializeField] GameObject wind_t5;
    [SerializeField] GameObject wind_gauge;

    int wind_count;

    void Awake()
    {
        wind_count = 1;
    }

    public void increment_wind()
    {
        // turn the various scaled images on and off depending on the current wind tier
        switch(wind_count)
        {
            case 1: // always start with T1 wind active in the UI
                wind_count ++;
                wind_t1.SetActive(false);
                wind_t2.SetActive(true);
                break;
            case 2:
                wind_count++;
                wind_t2.SetActive(false);
                wind_t3.SetActive(true);
                break;
            case 3:
                wind_count++;
                wind_t3.SetActive(false);
                wind_t4.SetActive(true);
                break;
            case 4:
                wind_count++;
                wind_t4.SetActive(false);
                wind_t5.SetActive(true);
                break;
            case 5: // cycle back to T1 and begin again
                wind_count = 1;
                wind_t5.SetActive(false);
                wind_t1.SetActive(true);
                break;
        }
    }

    public void rotate_windgauge()
    {
        
        
        
        //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Vector2 direction = mousePosition - wind_gauge.transform.position;

        
    }    

        public float get_wind_direction()
    {
        float wind_dir;

        Vector3 local_pos;
        Quaternion local_rot;
        wind_gauge.transform.GetLocalPositionAndRotation(out local_pos, out local_rot);

        Debug.Log(local_pos.x + ", " + local_pos.y + ", " + local_pos.z);
        Debug.Log(local_rot.x + ", " + local_rot.y + ", " + local_rot.z + ", " + local_rot.w);





        wind_dir = 5.0f;

        return wind_dir;
    }
}
