using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : PlayerController
{
    public Spere weapons;
    public List<GameObject> monsterList;
    RaycastHit[] hits;
    float MaxDistance = 10f;
    Vector3 offset;


    override public void UseSkill()
    {
        offset = new Vector3(0f, 0.7f, 0.5f);

        hits = Physics.RaycastAll(transform.position + offset, transform.forward, MaxDistance);
        if (hits.Length != 0)
        {
            for (int i = 0; i<hits.Length; i++)
            {
                GameObject temp = hits[i].collider.gameObject;
                if (temp.tag == "Monster" && monsterList.Find(x => x == temp) == null)
                {
                    monsterList.Add(temp);
                }
            }
        };
        for (int i = 0; i < monsterList.Count; i++)
        {
            print("monsterList -> " + monsterList[i].gameObject.name);
        }

        weapons.isSkill = true;
        weapons.monsterList = monsterList;
        Attack();
        weapons.isSkill = false;
        monsterList.Clear();
    }
}