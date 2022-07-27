using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public Transform[] A, B, C;

    public bool[] ADeath, BDeath, CDeath;

    public Object Melee, Range, SelfDes;
    
    private Object[] monsters;

    // Start is called before the first frame update
    void Start()
    {
        monsters = new Object[3]{Melee, Range, SelfDes};
        InitialSpawn();
        ADeath = new bool[2]{false, false};
        BDeath = new bool[3]{false, false, false};
        CDeath = new bool[7]{false, false, false, false, false, false, false};
    }

    private void InitialSpawn()
    {
        // A01 ~ A07
        foreach (Transform spawnPoint in A)
        {
            GameObject monster;
            monster = Instantiate(Melee, spawnPoint.position, 
                        Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))) as GameObject;
            // Intro Monster HP setting for Beginner
            monster.GetComponent<NormalMonster>().monsterData.maxHp = 80;
            monster.GetComponent<NormalMonster>().monsterData.curHp = 80;
        }
            
        
        // B01
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Melee, B[0].position, 
                        Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        }

        // B02
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Range, B[1].position, 
                        Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        }

        // B03
        for (int i = 0; i < 3; i++)
        {
            Instantiate(SelfDes, B[2].position, 
                        Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        }

        // C01 ~ C07
        foreach (Transform spawnPoint in C)
            Instantiate(monsters[Random.Range(0,3)], spawnPoint.position, 
                        Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
    }

    private GameObject Respawn(Object kindOfMonster, Transform spawnPoint)
    {
        return Instantiate(kindOfMonster, spawnPoint.position, 
                        Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))) as GameObject;
    }

    public void Decode(string spawnLoc)
    {
        switch (spawnLoc)
        {
            case "A01":
                ADeath[0] = true;
                break;
            case "A07":
                ADeath[1] = true;
                break;

                
            case "B01":
                BDeath[0] = true;
                break;
            case "B02":
                BDeath[0] = true;
                break;
            case "B03":
                BDeath[0] = true;
                break;

                
            case "C01":
                CDeath[0] = true;
                break;
            case "C02":
                CDeath[1] = true;
                break;
            case "C03":
                CDeath[2] = true;
                break;
            case "C04":
                CDeath[3] = true;
                break;
            case "C05":
                CDeath[4] = true;
                break;
            case "C06":
                CDeath[5] = true;
                break;
            case "C07":
                CDeath[6] = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i=0; i<2; i++)
        {
            bool isDead = ADeath[i];
            if (isDead)
            {
                // 여기서 쿨타임 새기
                float coolTime=0;
                while(coolTime < 180)
                {
                    coolTime += 1 * Time.deltaTime;
                }
                GameObject monster = Respawn(Melee, A[i]);
                // Intro Monster HP setting for Beginner
                monster.GetComponent<NormalMonster>().monsterData.maxHp = 80;
                monster.GetComponent<NormalMonster>().monsterData.curHp = 80;
                ADeath[i] = false;
            }
        }

        for (int i=0; i<3; i++)
        {
            bool isDead = BDeath[i];
            if (isDead)
            {
                // 쿨타임
                GameObject monster = Respawn(monsters[i], B[i]);
                BDeath[i] = false;
            }
        }

        for (int i=0; i<7; i++)
        {
            bool isDead = CDeath[i];
            if (isDead)
            {
                GameObject monster = Respawn(monsters[Random.Range(0,3)], C[i]);
                CDeath[i] = false;
            }
        }
    }
}
