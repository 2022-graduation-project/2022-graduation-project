using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class exPlayerUI : MonoBehaviour
{
    public Text nameTxt, levelTxt, classTxt,  moneyTxt;
    // public Text HpPotion, MpPotion, MasterPotion;
    public List<Text> items = new List<Text>();
    public Image hpBar, mpBar;

    void Awake()
    {
        nameTxt = GameObject.Find("Player Info/Name").GetComponent<Text>();
        levelTxt = GameObject.Find("Player Info/Level").GetComponent<Text>();
        classTxt = GameObject.Find("Player Info/Class").GetComponent<Text>();
        moneyTxt = GameObject.Find("Player Info/Money").GetComponent<Text>();

        Transform temp = GameObject.Find("Quick Item Slot")?.transform;
        foreach(Transform tr in temp) items.Add(tr.Find("Number")?.GetChild(0).GetComponent<Text>());

        hpBar = GameObject.Find("HP").transform.GetChild(0).GetComponent<Image>();
        mpBar = GameObject.Find("MP").transform.GetChild(0).GetComponent<Image>();
    }
}