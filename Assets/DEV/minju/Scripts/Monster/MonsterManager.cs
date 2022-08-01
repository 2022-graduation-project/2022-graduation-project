using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterManager : MonoBehaviour
{
    // 몬스터 매니저
    // This exists only in Object "MonsterManager"

    // 최대 몬스터 수
    // Max num of Monsters: 30
    int curNumMonsters = 0;

    // 몬스터 전체 관리 배열
    // List of all monsters
    List<GameObject> monsters = new List<GameObject>();

    // 스폰포인트
    // array of spawn points
    //public Transform spawnPoints;
    //public Transform[] childrenSP;

    // 스폰포인트 중복 관리
    // For Random Spawn without duplicate
    public int[] duplicate;

    // 스폰 위치
    // to spawn monsters on random points
    int spawnNumber;

    // 몬스터 프리펩 이름
    // names of monsters
    string[] kindsOfMonsters;

    public void Damaging()
    {
        //UI 버튼에서 몬스터 전체 데미지 입히기
        foreach (var monster in monsters)
        {
            monster.GetComponent<MonsterController>().Damaged(-10);
        }
    }

    // Read Json data to Dictionary
    private Dictionary<string, ItemData> itemDict;
    // get all item names and use as key for the Dictionary
    private List<string> keysOfItems = new List<string>();
    // random items in itembags
    private int countOfItems;
    // itemBag Prefab resource
    public GameObject item;
    // temporary var of ItemData
    private ItemData tempItemData;

    public void DropItem(Transform itemLocation)
    {
        // Item Bag
        var itemBag = Instantiate<GameObject>(item, itemLocation);
        itemBag.transform.SetParent(transform);

        // Random Item Counts (0 ~ 3)
        int countOfDrop = Random.Range(0, 4);

        // Create Random Items in ItemBag
        for (int i = 0; i < countOfDrop; i++)
        {
            int randomIndex = Random.Range(0, countOfItems);
            // 해당 itemBag에 넣은 random item 정보 추가
            if (itemDict.TryGetValue(keysOfItems[randomIndex], out tempItemData))
            {
                itemBag.GetComponent<ItemBag>().AddItem(tempItemData);
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

        // get all item names and use as key for the Dictionary
        foreach (KeyValuePair<string, ItemData> q in itemDict)
        {
            keysOfItems.Add(q.Value.image_name);
        }

        countOfItems = keysOfItems.Count;



        // 배열 0으로 초기화
        // initiate elements as 0
        duplicate = Enumerable.Repeat<int>(0, 52).ToArray<int>();


        // kindsOfMonsters = new string[] { "Ghost", "Goblin", "Skeleton" };

        // // 스폰포인트 전부 가져오기
        // // bring every spawnpoints
        // childrenSP = spawnPoints.gameObject.GetComponentsInChildren<Transform>();

        // // 처음 시작 시 스폰
        // // Start to Spawn monsters
        // for (int i = 0; i < 30; i++)
        // {   // 30마리 생성
        //     // Random spawn points (1 ~ 50)
        //     spawnNumber = Random.Range(1, 51);

        //     // 랜덤 생성이 중복이 아닌지 확인하기
        //     // Check duplicate
        //     if (repeatRandom(spawnNumber))
        //     {
        //         // 중복이 아닐 때만 생성
        //         CreateMonster(childrenSP[spawnNumber]);
        //     }
        //     else
        //         continue;
        // }

    }

    // 몬스터 생성
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
            GameObject objMonster = Instantiate(Resources.Load(kindsOfMonsters[Random.Range(0, 3)]),
                currentLocation.position, Quaternion.identity * Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))) as GameObject;

            // ���� ���� ����Ʈ�� �߰��ϱ�
            // add monster object in list
            monsters.Add(objMonster);

        }
    }

    // 몬스터 삭제
    // Delete Monster
    public void DeleteMonster(GameObject monster)
    {

        // ������ �ش� ���� ������Ʈ ����
        monster.SetActive(false);

        // ����Ʈ���� �ش� ���� ����
        GameObject temp = monsters.Find(x => x == monster);
        monsters.Remove(temp);

        // ���� �ο� �� Update
        // update current num of Monsters
        curNumMonsters--;
    }



    // 중복 아닌 랜덤 스폰포인트 뽑기
    bool repeatRandom(int temp)
    {
        if (duplicate[temp] == 0)
        {
            duplicate[temp]++;
            return true;
        }
        else
        {
            //spawnNumber = Random.Range(1, 51);
            spawnNumber = Random.Range(1, 6);
            return repeatRandom(spawnNumber);
        }
    }
}