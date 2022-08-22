using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMonster_TestHP : MonoBehaviour
{
    public NormalMonster monster;
    public GameObject prefab_HpBar;
    public Canvas canvas;
    public Slider slider;





    void Start()
    {
        SetHpBar();
    }

    void Update()
    {
        UpdateHpBar();
    }




    protected void SetHpBar()
    {
        monster = transform.GetComponent<NormalMonster>();
        prefab_HpBar = GameObject.Find("HP Bar");
        canvas = monster.GetComponentInChildren<Canvas>();
        slider = prefab_HpBar.GetComponentInChildren<Slider>();

        canvas.worldCamera = GameObject.Find("Player_Archer").GetComponentInChildren<Camera>();
    }

    protected void UpdateHpBar()
    {
        prefab_HpBar.transform.position = monster.transform.position + new Vector3(0, 2.2f, 0);
        canvas.transform.rotation = canvas.worldCamera.transform.rotation;  // 빌보드

        if (monster.monsterData.curHp > 0)
        {
            slider.value = monster.monsterData.curHp / monster.monsterData.maxHp;
        }
        else
        {
            prefab_HpBar.SetActive(false);
        }
    }
}
