using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooking : MonoBehaviour
{

    public string firstItem;
    public string secondItem;
    private bool all;
    private string result;
    public Image resultImg;

    public void ItemOn()
    {
        if(firstItem != "" && secondItem != "")
        {
            all = true;
            print("All == true");
        }
        else
        {
            all = false;
            print("There's no image");
        }

        if (all == false)
        {
            return;
        }

        else
        {
            switch (firstItem)
            {
                case "":
                    all = false;
                    return;
                case "000_hpPotion":
                    switch (secondItem)
                    {
                        case "000_hpPotion":
                            result = "000_hpPotion";
                            break;
                        case "001_mpPotion":
                            result = "007_pudding";
                            break;
                        case "002_apple":
                            result = "008_fruitN";
                            break;
                        case "003_meat":
                            result = "009_weirdMeat";
                            break;
                        case "004_gosu":
                            result = "010_grass01";
                            break;
                        case "005_bugs":
                            result = "011_eggs";
                            break;
                        case "006_book":
                            result = "";
                            break;
                    }
                    break;
                case "001_mpPotion":
                    switch (secondItem)
                    {
                        case "000_hpPotion":
                            result = "007_pudding";
                            break;
                        case "001_mpPotion":
                            result = "001_mpPotion";
                            break;
                        case "002_apple":
                            result = "012_mana_fruit";
                            break;
                        case "003_meat":
                            result = "009_weirdMeat";
                            break;
                        case "004_gosu":
                            result = "010_grass01";
                            break;
                        case "005_bugs":
                            result = "011_eggs";
                            break;
                        case "006_book":
                            result = "";
                            break;
                    }
                    break;
                case "002_apple":
                    switch (secondItem)
                    {
                        case "000_hpPotion":
                            result = "008_fruitN";
                            break;
                        case "001_mpPotion":
                            result = "012_mana_fruit";
                            break;
                        case "002_apple":
                            result = "002_apple";
                            break;
                        case "003_meat":
                            result = "013_meat03";
                            break;
                        case "004_gosu":
                            result = "014_grass01";
                            break;
                        case "005_bugs":
                            result = "015_agility_fruit";
                            break;
                        case "006_book":
                            result = "";
                            break;
                    }
                    break;
                case "003_meat":
                    switch (secondItem)
                    {
                        case "000_hpPotion":
                            result = "009_weirdMeat";
                            break;
                        case "001_mpPotion":
                            result = "009_weirdMeat";
                            break;
                        case "002_apple":
                            result = "013_meat03";
                            break;
                        case "003_meat":
                            result = "003_meat";
                            break;
                        case "004_gosu":
                            result = "016_meat08";
                            break;
                        case "005_bugs":
                            result = "017_meat07";
                            break;
                        case "006_book":
                            result = "";
                            break;
                    }
                    break;
                case "004_gosu":
                    switch (secondItem)
                    {
                        case "000_hpPotion":
                            result = "010_grass01";
                            break;
                        case "001_mpPotion":
                            result = "010_grass01";
                            break;
                        case "002_apple":
                            result = "014_grass01";
                            break;
                        case "003_meat":
                            result = "016_meat08";
                            break;
                        case "004_gosu":
                            result = "004_gosu";
                            break;
                        case "005_bugs":
                            result = "018_species03";
                            break;
                        case "006_book":
                            result = "";
                            break;
                    }
                    break;
                case "005_bugs":
                    switch (secondItem)
                    {
                        case "000_hpPotion":
                            result = "010_grass01";
                            break;
                        case "001_mpPotion":
                            result = "010_grass01";
                            break;
                        case "002_apple":
                            result = "014_grass01";
                            break;
                        case "003_meat":
                            result = "016_meat08";
                            break;
                        case "004_gosu":
                            result = "004_gosu";
                            break;
                        case "005_bugs":
                            result = "018_species03";
                            break;
                        case "006_book":
                            result = "";
                            break;
                    }
                    break;
                case "006_book":
                    switch (secondItem)
                    {
                        case "000_hpPotion":
                            result = "";
                            break;
                        case "001_mpPotion":
                            result = "";
                            break;
                        case "002_apple":
                            result = "";
                            break;
                        case "003_meat":
                            result = "";
                            break;
                        case "004_gosu":
                            result = "";
                            break;
                        case "005_bugs":
                            result = "";
                            break;
                        case "006_book":
                            result = "";
                            break;
                    }
                    break;
            }
            //resultImg.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
    }

    public void setFirstItem(string item)
    {
        firstItem = item;
        print("firstItem: " + firstItem);
    }
    
    public void setSecondItem(string item)
    {
        secondItem = item;
        print("secondItem: " + secondItem);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
