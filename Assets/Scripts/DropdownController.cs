using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    public TMP_Dropdown typeDropdown;
    public TMP_Dropdown weaponDropdown;

    public Image gunImage;   
    public Sprite mortar;
    public Sprite collie_mht;
    public Sprite warden_mt;
    public Sprite collie_gb;
    public Sprite warden_gb;
    public Sprite warden_gb2;
    public Sprite collie_120;
    public Sprite warden_120;
    public Sprite collie_150;
    public Sprite warden_150;
    public Sprite sc;
    public Sprite rail_sc;
    public Sprite collie_rockettankette;
    public Sprite collie_rockettruck;
    public Sprite collie_rocketbatt;
    public Sprite warden_rockettank;
    public Sprite warden_rocketpush;
    public Sprite warden_rocketht;


    public int gunType;
    public int gun;

    void Start()
    {
        gunType = 0;
        gun = 0;
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
                        gunImage.GetComponent<Image>().sprite = mortar;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_mht;
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = warden_mt;
                        break;
                }
                break;

            case 2: // gunboats
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_gb;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_gb;
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = warden_gb2;
                        break;
                }
                break;

            case 3: // 120mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_120;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_120;
                        break;
                }
                break;

            case 4: // 150mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_150;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = warden_150;
                        break;
                }
                break;

            case 5: // 300mm
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = sc;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = rail_sc;
                        break;
                }
                break;

            case 6: // rockets
                switch (gun)
                {
                    case 1:
                        gunImage.GetComponent<Image>().sprite = collie_rockettankette;
                        break;
                    case 2:
                        gunImage.GetComponent<Image>().sprite = collie_rockettruck;
                        break;
                    case 3:
                        gunImage.GetComponent<Image>().sprite = collie_rocketbatt;
                        break;
                    case 4:
                        gunImage.GetComponent<Image>().sprite = warden_rockettank;
                        break;
                    case 5:
                        gunImage.GetComponent<Image>().sprite = warden_rocketpush;
                        break;
                    case 6:
                        gunImage.GetComponent<Image>().sprite = warden_rocketht;
                        break;
                }
                break;
        }

    }

    public void reset_dropdowns()
    {
        gunType = 0;
        gun = 0;
        typeDropdown.value = 0;

        // clear any previous dropdown items      
        weaponDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Gun Model" });
        weaponDropdown.value = 0;
        weaponDropdown.options.Clear();
        gunImage.GetComponent<Image>().sprite = null;
    }

}
