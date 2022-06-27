using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : PlayerController
{
    public Spere weapons;
    public List<GameObject> monsterList;
    RaycastHit hit;
    float MaxDistance = 100f;
    Vector3 offset;


    override public void UseSkill()
    {
        Debug.DrawRay(transform.position + offset, transform.forward * MaxDistance, Color.blue);
        if (Physics.Raycast(transform.position + offset, transform.forward, out hit, MaxDistance))
        {
            GameObject temp = hit.collider.gameObject;
            if (temp.tag == "Monster" && monsterList.Find(x => x == temp) == null)
            {
                monsterList.Add(temp);
            }
        };


        offset = new Vector3(0f, 0.7f, 0.5f);
        weapons.isSkill = true;
        weapons.monsterList = monsterList;
        Attack();
        weapons.isSkill = false;
    }
}