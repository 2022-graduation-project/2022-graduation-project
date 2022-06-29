using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    void Start()
    {
        PlayerData playerData = DataManager.instance.LoadJsonFile
                    <Dictionary<string, PlayerData>>
                    (Application.dataPath + "/MAIN/Data", "player")
                    ["000_player"];
        print(playerData.name);

        ItemData itemData = DataManager.instance.LoadJsonFile
                            <Dictionary<string, ItemData>>
                            (Application.dataPath + "/MAIN/Data", "item")
                            ["000_hpPotion"];
        print(itemData.item_name);
    }
}