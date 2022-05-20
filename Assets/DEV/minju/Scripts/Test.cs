using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Dictionary<string, ItemData> itemDict;
    // Start is called before the first frame update
    void Start()
    {
        itemDict = DataManager.instance
                    .LoadJsonFile<Dictionary<string, ItemData>>
                    (Application.dataPath + "/MAIN/Data", "item");

        foreach (KeyValuePair<string, ItemData> q in itemDict)
        {
            Debug.Log(q.Value.item_name);
            //List<Goal> temp = new List<Goal>(q.Value.goal);
            //for (int i = 0; i < temp.Count; i++)
            //    Debug.Log($"{temp[i].type}, {temp[i].content}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
