using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class QuestUI : MonoBehaviour
{
    public GameObject questSlot;
    Dictionary<string, QuestData> questDict;

    void Start()
    {
        questDict = DataManager.instance.LoadJsonFile<Dictionary<string, QuestData>>(Application.dataPath + "/MAIN/Data", "quest");
    }
}