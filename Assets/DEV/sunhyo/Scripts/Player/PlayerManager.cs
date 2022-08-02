using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerData playerData;

    void Start()
    {
        playerData = DataManager.instance.LoadJsonFile
            <Dictionary<string, PlayerData>>
            (Application.dataPath + "/MAIN/Data", "player-test")
            ["000_player"];

        foreach (KeyValuePair<string, int> item in playerData.inventory)
        {
            print($"{item.Key}, {item.Value}");
        }
    }
}