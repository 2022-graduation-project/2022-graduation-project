using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : MonoBehaviour
{
    public GameObject QuestUI;

    private SphereCollider detecter;

    void Start()
    {
        detecter = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Player")
            QuestUI.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {
        if(other.name == "Player")
            QuestUI.gameObject.SetActive(false);
    }

}