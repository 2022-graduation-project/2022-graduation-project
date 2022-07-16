using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public QuestUI questUI;

    private List<QuestData> quest = new List<QuestData>();
    
    // raycast로 변경 필
    private SphereCollider detecter;

    void Start()
    {
        detecter = GetComponent<SphereCollider>();

        //debug.log((mathf.abs(1000) % 1000 + 1).tostring("icon000"));
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player")
        {
            //questUI.gameObject.SetActive(true);
            //other.gameObject.GetComponent<PlayerController>().mouseMoveable = false;
            print("하이");
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.name == "Player")
        {
            //questUI.gameObject.SetActive(false);
            //other.gameObject.GetComponent<PlayerController>().mouseMoveable = true;
            print("바이");
        }
    }
}