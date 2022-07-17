using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    void Start()
    {
        Dictionary<string, PlayerData> playerDict =
                    DataManager.instance.LoadJsonFile
                    <Dictionary<string, PlayerData>>
                    (Application.dataPath + "/MAIN/Data", "player");

        PlayerData playerData = playerDict["000_player"];

        foreach (KeyValuePair<string, ItemData> item in playerData.inventory)
        {
            print($"{item}, {item.Key}, {item.Value.item_name}, {item.Value.count}");
        }

        playerData.inventory["000_hpPotion"].count = 9999;

        if (DataManager.instance.ObjectToJson<Dictionary<string, PlayerData>>(Application.dataPath + "/MAIN/Data", "player", playerDict))
        {
            print("objectToJson!!");
        }
    }
}