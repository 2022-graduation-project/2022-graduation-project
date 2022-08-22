using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMonster_TestHP : MonoBehaviour
{
    public NormalMonster monster;
    public Slider slider;
    public float maxHP;
    public float curHP;



    void Update()
    {
        slider.value = curHP / maxHP;
    }
}
