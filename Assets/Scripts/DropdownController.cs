using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Sprite warden_gb2;
    [SerializeField] Sprite collie_120;
    [SerializeField] Sprite warden_120;
    [SerializeField] Sprite collie_150;
    [SerializeField] Sprite warden_150;
    [SerializeField] Sprite stormcannon;
    [SerializeField] Sprite rail_sc;
    [SerializeField] Sprite collie_rockettankette;
    [SerializeField] Sprite collie_rockettruck;
    [SerializeField] Sprite collie_rocketbattery;
    [SerializeField] Sprite warden_rockettank;
    [SerializeField] Sprite warden_rocketpush;
    [SerializeField] Sprite warden_rocketht;

    // accessible outputs to other methods
    public int gunType;
    public int gun;
    public float minRange;
    public float maxRange;

    void Awake()
    {
        gunType = 0;
        gun = 0;
        minRange = 0f;
        maxRange = 0f;
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
                items.Add("Warden \"Ronan Meteora\" Gunship");
                break;
            case 3:
                items.Add("Choose a platform");
                items.Add("Colonial \"Koronides\" Field Gun");
                items.Add("Warden \"Huber Lariat\" Light Artillery");
                break;
            case 4:
                items.Add("Choose a platform");
                items.Add("Colonial \"Thunderbolt\" Cannon");
                items.Add("Warden \"Huber Exalt\" Cannon");
                break;
            case 5:
                items.Add("Choose a platform");
                items.Add("Storm Cannon");
                items.Add("\"Tempest\" Rail Cannon");
                break;
            case 6:
                items.Add("Choose a platform");
                items.Add("Colonial \"Deioneus\" Rocket Tankette");
                items.Add("Colonial \"Retiarius\" Rocket Truck");
                items.Add("Colonial \"Hades' Net\" Rocket Battery");
                items.Add("Warden \"King Jester\" Rocket Scout Tank");
                items.Add("Warden \"Wasp Nest\" Rocket Launcher");
                items.Add("Warden \"Skycaller\" Rocket Halftrack");
                break;
        }

        // populate with the new list
        foreach (string item in items)
        {
            weaponDropdown.options.Add(new TMP_Dropdown.OptionData() { text = item });
        }
    }

    public void get_gunplatform_selections()
    {
        gunType = typeDropdown.value;
        gun = weaponDropdown.value;

        // change the image sprite
        switch (gunType)
        {
            case 1: // mortars
                switch(gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = normal_mortar;
                        minRange = 45f;
                        maxRange = 80f;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_mht;
                        minRange = 45f;
                        maxRange = 80f;
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = warden_mtank;
                        minRange = 45f;
                        maxRange = 80f;
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
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_gb;
                        minRange = 50f;
                        maxRange = 100f;
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = warden_gb2;
                        minRange = 50f;
                        maxRange = 100f;
                        break;
                }
                break;

            case 3: // 120mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_120;
                        minRange = 100f;
                        maxRange = 250f;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_120;
                        minRange = 100f;
                        maxRange = 300f;
                        break;
                }
                break;

            case 4: // 150mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_150;
                        minRange = 200f;
                        maxRange = 350f;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_150;
                        minRange = 100f;
                        maxRange = 300f;
                        break;
                }
                break;

            case 5: // 300mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = stormcannon;
                        minRange = 400f;
                        maxRange = 1000f;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = rail_sc;
                        minRange = 350f;
                        maxRange = 500f;
                        break;
                }
                break;

            case 6: // rockets
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_rockettankette;
                        minRange = 225f;
                        maxRange = 400f;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_rockettruck;
                        minRange = 225f;
                        maxRange = 500f;
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_rocketbattery;
                        minRange = 200f;
                        maxRange = 425f;
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_rockettank;
                        minRange = 225f;
                        maxRange = 475f;
                        break;
                    case 5:
                        gunImage.GetComponent<Image>().sprite = warden_rocketpush;
                        minRange = 225f;
                        maxRange = 450f;
                        break;
                    case 6:
                        gunImage.GetComponent<Image>().sprite = warden_rocketht;
                        minRange = 200f;
                        maxRange = 225f;
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
        typeDropdown.value = 0;

        // clear any previous dropdown items      
        weaponDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Gun Model" });
        weaponDropdown.value = 0;
        weaponDropdown.options.Clear();
        gunImage.GetComponent<Image>().sprite = null;
    }

}
