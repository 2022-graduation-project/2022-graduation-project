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

        Debug.Log((Mathf.Abs(1000) % 30 + 1).ToString("icon000"));
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player")
            questUI.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        if(other.name == "Player")
            questUI.gameObject.SetActive(false);
    }

}