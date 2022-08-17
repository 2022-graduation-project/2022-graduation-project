using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    public int level;
    public string job;

	public float maxHp;
	public float maxMp;
    public float curHp;
    public float curMp;

    public float moveSpeed;
    public float jumpForce;

    public float STR;
    public float DEX;
    public float INT;

    public Dictionary<string, ItemData> inventory;
    public Dictionary<string, SkillData> skills;

    public int money;
}