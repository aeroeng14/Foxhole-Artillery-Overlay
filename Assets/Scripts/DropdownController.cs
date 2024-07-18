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
    public platformWindageStats chosenPlatform;
    public List<platformWindageStats> platformWindages;

    // json Object Class Definition
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
        // load the json data on first start
        load_json_data();
    }

    public void load_json_data()
    {
        
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

        // populate the global List of all parsed platform json values
        platformWindages = myData;

        // re-grab the dropdown box selections A) for the first time on init or B) if data file being reloaded 
        get_gunplatform_selections();
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
        int weapontype = typeDropdown.value;
        int gunPlatform = weaponDropdown.value;

        // change the image sprite
        switch (weapontype)
        {
            case 0: // None (default)

                // setting global variable to default
                platformWindageStats temp_var = new platformWindageStats
                {
                    Name = "None",
                    Notes = "None",
                    minRange = 0f,
                    maxRange = 0f,
                    rangeTicks = new float[] { 0f, 0f },
                    baselineDispersion = new float[] { 0f, 0f },
                    windTierBias = new float[] { 0f, 0f },
                    crossTrackReduction = new float[] { 0f, 0f },
                    inTrackReduction = new float[] { 0f, 0f },
                };
                chosenPlatform = temp_var;
                break;

            case 1: // mortars
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = normal_mortar;
                        chosenPlatform = platformWindages[0];
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_mht;
                        chosenPlatform = platformWindages[1];
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = warden_mtank;
                        chosenPlatform = platformWindages[2];
                        break;
                }
                break;

            case 2: // gunboats
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_gb;
                        chosenPlatform = platformWindages[3];
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_gb;
                        chosenPlatform = platformWindages[4];
                        break;
                }
                break;

            case 3: // large ships
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_DD;
                        chosenPlatform = platformWindages[5];
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_Frig;
                        chosenPlatform = platformWindages[6];
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_BB;
                        chosenPlatform = platformWindages[7];
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_BB;
                        chosenPlatform = platformWindages[8];
                        break;
                }
                break;

            case 4: // 120mm
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_120;
                        chosenPlatform = platformWindages[9];
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_120;
                        chosenPlatform = platformWindages[10];
                        break;
                }
                break;

            case 5: // 150mm
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_150;
                        chosenPlatform = platformWindages[11];
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_150;
                        chosenPlatform = platformWindages[12];
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_150_spg;
                        chosenPlatform = platformWindages[13];
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_150_spg;
                        chosenPlatform = platformWindages[14];
                        break;
                }
                break;

            case 6: // 300mm
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = stormcannon;
                        chosenPlatform = platformWindages[15];
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = rail_sc;
                        chosenPlatform = platformWindages[16];
                        break;
                }
                break;

            case 7: // rockets
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_rockettankette;
                        chosenPlatform = platformWindages[17];
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_rockettruck;
                        chosenPlatform = platformWindages[18];
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_rocketbattery;
                        chosenPlatform = platformWindages[19];
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_rockettank;
                        chosenPlatform = platformWindages[20];
                        break;
                    case 5:
                        gunImage.GetComponent<Image>().sprite = warden_rocketpush;
                        chosenPlatform = platformWindages[21];
                        break;
                    case 6:
                        gunImage.GetComponent<Image>().sprite = warden_rocketht;
                        chosenPlatform = platformWindages[22];
                        break;
                }
                break;

            case 8: // aimed infrastructure
                switch (gunPlatform)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = intelcenter;
                        chosenPlatform = platformWindages[23];
                        break;
                }
                break;
        }

    }

    public void reset_dropdowns()
    {

        // setting global variable to default
        platformWindageStats temp_var = new platformWindageStats
        {
            Name = "None",
            Notes = "None",
            minRange = 0f,
            maxRange = 0f,
            rangeTicks = new float[] { 0f, 0f },
            baselineDispersion = new float[] { 0f, 0f },
            windTierBias = new float[] { 0f, 0f },
            crossTrackReduction = new float[] { 0f, 0f },
            inTrackReduction = new float[] { 0f, 0f },
        };
        chosenPlatform = temp_var;

        // clear any previous dropdown items      
        typeDropdown.value = 0;

        weaponDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Gun Model" });
        weaponDropdown.value = 0;
        weaponDropdown.options.Clear();
        
        gunImage.GetComponent<Image>().sprite = null;
    }

}
