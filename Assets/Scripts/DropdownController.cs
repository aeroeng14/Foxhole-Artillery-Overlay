using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Newtonsoft.Json;


public class DropdownController : MonoBehaviour
{
    [SerializeField] TMP_Dropdown typeDropdown;
    [SerializeField] TMP_Dropdown weaponDropdown;

    // the image GameObject itself the sprites are loaded into
    [SerializeField] Image gunImage; 
    
    // the separate icons (private but editor accessible)
    [SerializeField] Sprite normal_mortar;
    [SerializeField] Sprite collie_mht;
    [SerializeField] Sprite warden_mtank;
    [SerializeField] Sprite collie_gb;
    [SerializeField] Sprite warden_gb;
    [SerializeField] Sprite collie_DD;
    [SerializeField] Sprite collie_BB;
    [SerializeField] Sprite warden_Frig;
    [SerializeField] Sprite warden_BB;
    [SerializeField] Sprite collie_sub;
    [SerializeField] Sprite collie_120;
    [SerializeField] Sprite warden_120;
    [SerializeField] Sprite collie_150;
    [SerializeField] Sprite warden_150;
    [SerializeField] Sprite collie_150_spg;
    [SerializeField] Sprite warden_150_spg;
    [SerializeField] Sprite stormcannon;
    [SerializeField] Sprite rail_sc;
    [SerializeField] Sprite collie_rockettankette;
    [SerializeField] Sprite collie_rockettruck;
    [SerializeField] Sprite collie_rocketbattery;
    [SerializeField] Sprite warden_rockettank;
    [SerializeField] Sprite warden_rocketpush;
    [SerializeField] Sprite warden_rocketht;
    [SerializeField] Sprite intelcenter;

    // accessible outputs to other methods
    public int gunType;
    public int gun;
    public float minRange;
    public float maxRange;
    public float dispersion;

    public class platformWindageStats
    {
        public string Name;
        public string Notes;
        public float minRange;
        public float maxRange;
        public float[] rangeTicks;
        public float[] baselineDispersion;     // f(range)
        public float[] windTierBias;           // f(wind tier)
        public float[] crossTrackReduction;    // f(wind tier)
        public float[] inTrackReduction;       // f(wind tier)
    }

    void Awake()
    {
        gunType = 0;
        gun = 0;
        minRange = 0f;
        maxRange = 0f;
        dispersion = 0f;

        // ***********************
        // load dispersions file
        // ***********************
        string folder_path = Directory.GetCurrentDirectory();
        string filename = "ArtyOverlayDispersionData.json";
        string fullpath = Path.Combine(folder_path, filename);

        // read the json file contents to a string
        string fileText = File.ReadAllText(fullpath);

        // deserialize the fileText string into a new List variable myData, of type platformWindageStats class
        var myData = JsonConvert.DeserializeObject<List<platformWindageStats>>(fileText);


        // load the class objects for every arty platform
        platformWindageStats mortar             = myData[0];

        platformWindageStats mortarCHt          = myData[1];
        platformWindageStats mortarWLt          = myData[2];
        platformWindageStats gunBoatC           = myData[3];
        platformWindageStats gunBoatW           = myData[4];
        platformWindageStats shipCDD            = myData[5];
        platformWindageStats shipWFrig          = myData[6];
        platformWindageStats shipCBB            = myData[7];
        platformWindageStats shipWBB            = myData[8];
        platformWindageStats artyC120           = myData[9];
        platformWindageStats artyW120           = myData[10];
        platformWindageStats artyC150           = myData[11];
        platformWindageStats artyW150           = myData[12];
        platformWindageStats artyC150SPG        = myData[13];
        platformWindageStats artyW150SPG        = myData[14];
        platformWindageStats arty300            = myData[15];
        platformWindageStats artyRails300       = myData[16];
        platformWindageStats rocketCTankette    = myData[17];
        platformWindageStats rocketCTruck       = myData[18];
        platformWindageStats rocketCEmplace     = myData[19];        
        platformWindageStats rocketWTank        = myData[20];
        platformWindageStats rocketWpush        = myData[21];
        platformWindageStats rocketWHt          = myData[22];      
        platformWindageStats artyIntelCenter    = myData[23];
        

        for (int i = 0; i < myData.Count; i++)
        {
            Debug.Log(myData[i].Name);
        }


        //Debug.Log("Name:" + mortar.Name);
        //Debug.Log("Notes:" + mortar.Notes);
        //Debug.Log("Min range:" + mortar.minRange);
        //Debug.Log("Max range:" + mortar.maxRange);

        //for (int i = 0; i < mortar.rangeTicks.Length; i++) 
        //{
        //    Debug.Log("Range:" + mortar.rangeTicks[i]);
        //    Debug.Log("Disp:" + mortar.baselineDispersion[i]);
        //    Debug.Log("Bias:" + mortar.windTierBias[i]);            
        //}

        //Debug.Log("Cross:" + mortar.crossTrackReduction[0]);
        //Debug.Log("InTrack:" + mortar.inTrackReduction[0]);


    }

    public void populate_gunmodels()
    {
        int weapontype_idx = typeDropdown.value;

        // clear any previous dropdown items    
        weaponDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Gun Model" });
        weaponDropdown.value = 0;
        weaponDropdown.options.Clear();
        gunImage.GetComponent<Image>().sprite = null;

        // list of subcategories
        List<string> items = new List<string>();
        switch (weapontype_idx)
        {
            case 1:
                items.Add("Choose a platform");
                items.Add("\"Cremari\" Mortar");
                items.Add("Colonial \"Pelta\" Mortar Halftrack");
                items.Add("Warden \"Devitte-Cain\" Mortar Tank");
                break;
            case 2:
                items.Add("Choose a platform");
                items.Add("Colonial \"Charon\" Gunboat");
                items.Add("Warden \"Ronan\" Gunboat");
                break;
            case 3:
                items.Add("Choose a platform");
                items.Add("Colonial \"Conqueror\" Destroyer");
                items.Add("Warden \"Blacksteele\" Frigate");
                items.Add("Colonial \"Titan\" Battleship");
                items.Add("Warden \"Callahan\" Battleship");
                break;
            case 4:
                items.Add("Choose a platform");
                items.Add("Colonial \"Koronides\" Field Gun");
                items.Add("Warden \"Huber Lariat\" Light Artillery");
                break;
            case 5:
                items.Add("Choose a platform");
                items.Add("Colonial \"Thunderbolt\" Cannon");
                items.Add("Warden \"Huber Exalt\" Cannon");
                items.Add("Colonial Lance-46 \"Sarissa\" SPG");
                items.Add("Warden Flood Mk. IX \"Stain\" SPG");

                break;
            case 6:
                items.Add("Choose a platform");
                items.Add("Storm Cannon");
                items.Add("\"Tempest\" Rail Cannon");
                break;
            case 7:
                items.Add("Choose a platform");
                items.Add("Colonial \"Deioneus\" Rocket Tankette");
                items.Add("Colonial \"Retiarius\" Rocket Truck");
                items.Add("Colonial \"Hades' Net\" Rocket Battery");
                items.Add("Warden \"King Jester\" Rocket Scout Tank");
                items.Add("Warden \"Wasp Nest\" Rocket Launcher");
                items.Add("Warden \"Skycaller\" Rocket Halftrack");
                break;
            case 8:
                items.Add("Choose a platform");
                items.Add("Intelligence Center");
                break;
        }

        // populate with the new list
        foreach (string item in items)
        {
            weaponDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
    }

    public void get_gunplatform_selections() // platform related data goes here. eventually convert to file driven system
    {
        gunType = typeDropdown.value;
        gun = weaponDropdown.value;

        // change the image sprite
        switch (gunType)
        {
            case 0: // None (default)
                dispersion = 0f; // reset this value if you switch from anything else back to distance only
                break;

            case 1: // mortars
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = normal_mortar;
                        minRange = 45f;
                        maxRange = 80f;
                        dispersion = 0f; // radius
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_mht;
                        minRange = 45f;
                        maxRange = 80f;
                        dispersion = 0f; // radius
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = warden_mtank;
                        minRange = 45f;
                        maxRange = 80f;
                        dispersion = 0f; // radius
                        break;
                }
                break;

            case 2: // gunboats
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_gb;
                        minRange = 50f;
                        maxRange = 100f;
                        dispersion = 0f; // radius
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_gb;
                        minRange = 50f;
                        maxRange = 100f;
                        dispersion = 0f; // radius
                        break;
                }
                break;

            case 3: // large ships
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_DD;
                        minRange = 50f;
                        maxRange = 100f;
                        dispersion = 0f; // radius
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_Frig;
                        minRange = 50f;
                        maxRange = 100f;
                        dispersion = 0f; // radius
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_BB;
                        minRange = 50f;
                        maxRange = 100f;
                        dispersion = 0f; // radius
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_BB;
                        minRange = 50f;
                        maxRange = 100f;
                        dispersion = 0f; // radius
                        break;
                }
                break;

            case 4: // 120mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_120;
                        minRange = 100f;
                        maxRange = 250f;
                        dispersion = 0f; // radius
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_120;
                        minRange = 100f;
                        maxRange = 300f;
                        dispersion = 0f; // radius
                        break;
                }
                break;

            case 5: // 150mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_150;
                        minRange = 200f;
                        maxRange = 350f;
                        dispersion = 0f; // radius
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_150;
                        minRange = 100f;
                        maxRange = 300f;
                        dispersion = 0f; // radius
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_150_spg;
                        minRange = 200f;
                        maxRange = 350f;
                        dispersion = 0f; // radius
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_150_spg;
                        minRange = 100f;
                        maxRange = 300f;
                        dispersion = 0f; // radius
                        break;
                }
                break;

            case 6: // 300mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = stormcannon;
                        minRange = 400f;
                        maxRange = 1000f;
                        dispersion = 0f; // radius
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = rail_sc;
                        minRange = 350f;
                        maxRange = 500f;
                        dispersion = 0f; // radius
                        break;
                }
                break;

            case 7: // rockets
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_rockettankette;
                        minRange = 225f;
                        maxRange = 400f;
                        dispersion = 0f; // radius
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_rockettruck;
                        minRange = 225f;
                        maxRange = 500f;
                        dispersion = 0f; // radius
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_rocketbattery;
                        minRange = 200f;
                        maxRange = 425f;
                        dispersion = 0f; // radius
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_rockettank;
                        minRange = 225f;
                        maxRange = 475f;
                        dispersion = 0f; // radius
                        break;
                    case 5:
                        gunImage.GetComponent<Image>().sprite = warden_rocketpush;
                        minRange = 225f;
                        maxRange = 450f;
                        dispersion = 0f; // radius
                        break;
                    case 6:
                        gunImage.GetComponent<Image>().sprite = warden_rocketht;
                        minRange = 200f;
                        maxRange = 225f;
                        dispersion = 0f; // radius
                        break;
                }
                break;

            case 8: // aimed infrastructure
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = intelcenter;
                        minRange = 500f;
                        maxRange = 2000f;
                        dispersion = 240f; // radius
                        break;
                }
                break;
        }

    }

    public void reset_dropdowns()
    {
        gunType = 0;
        gun = 0;
        minRange = 0f;
        maxRange = 0f;
        dispersion = 0f;
        typeDropdown.value = 0;

        // clear any previous dropdown items      
        weaponDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Gun Model" });
        weaponDropdown.value = 0;
        weaponDropdown.options.Clear();
        gunImage.GetComponent<Image>().sprite = null;
    }

}
