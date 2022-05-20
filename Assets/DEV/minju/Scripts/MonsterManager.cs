using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    // GameManager ������Ʈ �ȿ��� �����ϴ� ��ũ��Ʈ
    // This exists only in Object "GameManager"

    // ���� �ο�
    // Max num of Monsters: 30
    int curNumMonsters = 0;

    // ��� ���� ���� ����Ʈ
    // List of all monsters
    List<GameObject> monsters = new List<GameObject>();

    // ��������Ʈ �迭
    // array of spawn points
    public Transform spawnPoints;
    public Transform[] childrenSP;

    // For Random Spawn without duplicate
    public int[] duplicate;

    // to spawn monsters on random points
    int spawnNumber;

    // names of monsters
    string[] kindsOfMonsters; 

    public void Damaging()
    {
        print("Clicked");
        for(int i = 0; i < 30; i++)
        {
            monsters[i].GetComponent<MonsterAI>().Damage(-10);
        }
    }

    private Dictionary<string, ItemData> itemDict;
    private List<string> keysOfItems = new List<string>();
    private int countOfItems;
    public GameObject item;

    private ItemData temps;
    public void DropItem(Transform itemLocation)
    {
        // Item Bag
        var itemBag = Instantiate<GameObject>(item, itemLocation);
        itemBag.transform.SetParent(transform);

        // Random Item Counts
        int countOfDrop = Random.Range(0, countOfItems + 1);

        // Create Random Items in ItemBag
        for(int i = 0; i < countOfDrop; i++)
        {
            int randomIndex = Random.Range(0, countOfItems + 1);
            var temp = Instantiate(Resources.Load(keysOfItems[randomIndex]), itemLocation) as GameObject;
            temp.transform.SetParent(itemBag.transform);
            // 해당 itemBag에 넣은 random item 정보 추가
            if(itemDict.TryGetValue(keysOfItems[randomIndex], out temps))
            {
                itemBag.GetComponent<ItemBag>().AddItem(temps);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // for DropItem()
        itemDict = DataManager.instance
                    .LoadJsonFile<Dictionary<string, ItemData>>
                    (Application.dataPath + "/MAIN/Data", "item");

        foreach (KeyValuePair<string, ItemData> q in itemDict)
        {
            Debug.Log(q.Value.image_name);
            print(q.Value.image_name.GetType());
            keysOfItems.Add(q.Value.image_name);
        }
        countOfItems = keysOfItems.Count;

        // 배열 0으로 초기화
        // initiate elements as 0
        duplicate = Enumerable.Repeat<int>(0, 50).ToArray<int>();


        kindsOfMonsters = new string[] { "Characters", "Characters (2)", "Characters (3)", "Characters (4)", "Characters (5)",
                                         "Characters (6)", "Characters (7)", "Characters (8)", "Characters (9)", "Characters (10)",
                                         "Characters (11)", "Characters (12)", "Characters (13)", "Characters (14)", "Characters (15)"};

        // 스폰포인트 전부 가져오기
        // bring every spawnpoints
        childrenSP = spawnPoints.gameObject.GetComponentsInChildren<Transform>();

        // 처음 시작 시 스폰
        // Start to Spawn monsters
        for (int i = 0; i < 30; i++)
        {   // 30마리 생성
            // Random spawn points (0 ~ 49)
            spawnNumber = Random.Range(0, 50);
            
            // 랜덤 생성이 중복이 아닌지 확인하기
            // Check duplicate
            if (repeatRandom(spawnNumber))
            {
                // 중복이 아닐 때만 생성
                CreateMonster(childrenSP[spawnNumber]);
            }
            else
                continue;
        }

        //monsters[1].GetComponent<MonsterAI>().Damage(-20);
    }

    // ���� ���� �Լ�
    // Create Monster
    public void CreateMonster(Transform currentLocation)
    {
        // ���� 30���� ���� ����
        // Num of Monsters should be lower than 30
        if (curNumMonsters <= 30)
        {
            // ���� ���� �ο� update
            // update current num of Monsters
            curNumMonsters++;

            // ���� ���� �����ϱ�
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load(kindsOfMonsters[Random.Range(0, 15)]), 
                currentLocation.position, Quaternion.identity * Quaternion.Euler(new Vector3(0,Random.Range(0,360),0))) as GameObject;

            // ���� ���� ����Ʈ�� �߰��ϱ�
            // add monster object in list
            monsters.Add(objMonster);

            // ���� ��ũ��Ʈ�� ���� ������ȣ�� ����Ʈ�� index��ȣ�� ����
            // make monster script remember its own index (as List's index)
            objMonster.GetComponent<MonsterAI>().monsterIdx
                = monsters.IndexOf(objMonster);
        }
    }

    // ���� ���� �Լ�
    // Delete Monster
    public void DeleteMonster(int indexOfMonster)
    {
        // ������ �ش� ���� ������Ʈ ����
        monsters[indexOfMonster].SetActive(false);
        Destroy(monsters[indexOfMonster]);

        // ����Ʈ���� �ش� ���� ����
        monsters.RemoveAt(indexOfMonster);

        // ���� �ο� �� Update
        // update current num of Monsters
        curNumMonsters--;
    }

    // ���� ���� ���� �Լ�
    // Create Boss Monster
    public void CreateBossMonster(Transform currentLocation)
    {
        // ���� 30���� ���� ����
        // Num of Monsters should be lower than 30
        if (curNumMonsters <= 30)
        {
            // ���� ���� �ο� update
            // update current num of Monsters
            curNumMonsters++;

            // ���� ���� �����ϱ�
            // add new monster to curruent location of scene
            GameObject objMonster = Instantiate(Resources.Load(kindsOfMonsters[Random.Range(0, 16)]),
                currentLocation.position, Quaternion.identity) as GameObject;

            // ���� ���� ����Ʈ�� �߰��ϱ�
            // add monster object in list
            monsters.Add(objMonster);

            // ���� ��ũ��Ʈ�� ���� ������ȣ�� ����Ʈ�� index��ȣ�� ����
            // make monster script remember its own index (as List's index)
            objMonster.GetComponent<MonsterAI>().monsterIdx
                = monsters.IndexOf(objMonster);

            // ���� ��ũ��Ʈ�� ���� ���� ���� ����
            // make monster script remember it's boss
            objMonster.GetComponent<MonsterAI>().isBossMonster
                = true;
        }
    }

    bool repeatRandom(int temp)
    {
        if (duplicate[temp] == 0)
        {
            duplicate[temp]++;
            return true;
        }
        else//중복확인해서 랜덤 다시 뽑는 거 재귀함수로 다시 고쳐야함!!
        {
            spawnNumber = Random.Range(0, 50);
            return repeatRandom(spawnNumber);
        }
    }
}
