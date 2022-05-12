using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    // Info
    public string playerName;
    public int level;
    public string cls;
    public int money;
    public float maxHP;
    public float maxMP;
    public float hp;
    public float mp;

    public float moveSpeed = 5.0f;
    public float turnSpeed = 3.0f;
    public float jumpForce = 5.0f;

    // 이 부분 json으로 받아오기
    public void Set()
    {
        playerName = "Script Test";
        level = 10;
        cls = "Soldier";
        maxHP = 100.0f;
        maxMP = 50.0f;
        hp = maxHP - 10;
        mp = maxMP - 20;
    }
}

public class Inventory
{
    public int HpPotion;
    public int MpPotion;
    public int MasterPotion;
}

public class Skill
{
    public int skill1;
    public int skill2;
}