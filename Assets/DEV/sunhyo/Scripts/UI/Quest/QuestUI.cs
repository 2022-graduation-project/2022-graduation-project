using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class QuestUI : MonoBehaviour
{
    public GameObject questSlot;
    public Transform content;
    public List<GameObject> questPool = new List<GameObject>();
    private Dictionary<string, QuestData> questDict;
    private int maxQuests = 10;

    void Start()
    {
        CreateQuestPool();

        questDict = DataManager.instance
                    .LoadJsonFile<Dictionary<string, QuestData>>
                    (Application.dataPath + "/MAIN/Data", "quest");

        foreach(KeyValuePair<string, QuestData> q in questDict)
        {
            List<Goal> temp = new List<Goal>(q.Value.goal);
            for(int i=0; i<temp.Count; i++)
                Debug.Log($"{temp[i].type}, {temp[i].content}");
        }

        foreach(KeyValuePair<string, QuestData> q in questDict)
        {
            Reward temp = q.Value.reward;
            Debug.Log($"{temp.money}, {temp.exp}");
        }

        foreach(KeyValuePair<string, QuestData> q in questDict)
            CreatQuest(q.Value);
    }

    void CreatQuest(QuestData _questData)
    {
        GameObject _quest = GetQuestInPool();
        _quest.GetComponent<QuestSlot>().Set(_questData);
        _quest?.SetActive(true);
    }

    void CreateQuestPool()
    {
        for(int i=0; i<maxQuests; i++)
        {
            var _quest = Instantiate<GameObject>(questSlot);
            _quest.transform.SetParent(content, false);
            _quest.name = $"QuestSlot_{i:00}";
            _quest.SetActive(false);
            questPool.Add(_quest);
        }
    }

    GameObject GetQuestInPool()
    {
        foreach(var _quest in questPool)
        {
            if(_quest.activeSelf == false)
                return _quest;
        }

        return  null;
    }
}