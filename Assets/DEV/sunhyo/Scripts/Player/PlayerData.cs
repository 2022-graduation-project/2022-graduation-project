using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    // Info
    public string playerName;
    public int level;
    public string cls;
    public float maxHP;
    public float maxMP;
    public float hp;
    public float mp;

    public float moveSpeed = 5.0f;
    public float turnSpeed = 3.0f;
    public float jumpForce = 5.0f;
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