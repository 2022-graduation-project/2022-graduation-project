using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManage : MonoBehaviour
{
    // ��������Ʈ
    // array of spawn points
    public Transform spawnPoints;
    public Transform[] childrenSP;

    // ��������Ʈ �ߺ� ����
    // For Random Spawn without duplicate
    public int[] duplicate;

    // Prefabs
    public Object[] Chest;

    // Managing created chests
    private List <GameObject> chests = new List<GameObject>();

    // Random Number
    private int spawnNumber;
    private int chestNumber;

    // Initial Spawn
    bool initSpawn = false;

    // Read Json data to Dictionary
    private Dictionary<string, ChestData> chestDict;
    // get all item names and use as key for the Dictionary
    private List<string> keysOfItems = new List<string>();
    // random items in itembags
    private int countOfItems;
    // chest Prefab resource
    public GameObject tempChest;
    // temporary var of ItemData
    private ChestData tempItemData;

    // Start is called before the first frame update
    void Start()
    {

        chestDict = DataManager.instance
                    .LoadJsonFile<Dictionary<string, ChestData>>
                    (Application.dataPath + "/MAIN/Data", "chest");

        // get all item names and use as key for the Dictionary
        foreach (KeyValuePair<string, ChestData> q in chestDict)
        {
            keysOfItems.Add(q.Value.image_name);
        }

        countOfItems = keysOfItems.Count;
        print(countOfItems);

        // ��������Ʈ ���� ��������
        // bring every spawnpoints
        childrenSP = spawnPoints.gameObject.GetComponentsInChildren<Transform>();

        // �迭 0���� �ʱ�ȭ
        // initiate elements as 0
        duplicate = Enumerable.Repeat<int>(0, 46).ToArray<int>();

        // ó�� ���� �� ����
        // Start to Spawn item chests
        for (int i = 0; i < 20; i++)
        {
            //Instantiate(Chest[i], childrenSP[i].position, childrenSP[i].rotation);
            //Instantiate(Chest, childrenSP[i].position, Quaternion.identity);

            // ���� ������ ����
            // Random Chest Prefab (0 ~ 4)
            chestNumber = Random.Range(0, 5);

            // 20�� ����
            // Random spawn points (1 ~ 44)
            spawnNumber = Random.Range(1, 45);

            // ���� ������ �ߺ��� �ƴ��� Ȯ���ϱ�
            // Check duplicate
            if (repeatRandom(spawnNumber))
            {
                // �ߺ��� �ƴ� ���� ����
                tempChest = Instantiate(Chest[chestNumber], childrenSP[spawnNumber].position, childrenSP[spawnNumber].rotation) as GameObject;

                chests.Add(tempChest);

                // Random Item Counts (0 ~ 3)
                int countOfDrop = Random.Range(0, 4);

                // Create Random Items in ItemBag
                for (int j = 0; j < countOfDrop; j++)
                {
                    int randomIndex = Random.Range(0, countOfItems);
                    // �ش� itemBag�� ���� random item ���� �߰�
                    if (chestDict.TryGetValue(keysOfItems[randomIndex], out tempItemData))
                    {
                        print("tempItemName: "+tempItemData.item_name);
                        tempChest.GetComponent<ChestBag>().AddItem(tempItemData);
                    }
                }

            }
            else
                continue;
        }

        initSpawn = true;

        if (initSpawn)
        {
            Invoke("CheckChests", 10f);
        }
    }

    // �ߺ� �ƴ� ���� ��������Ʈ �̱�
    bool repeatRandom(int temp)
    {
        if (duplicate[temp] == 0)
        {
            duplicate[temp]++;
            return true;
        }
        else
        {
            spawnNumber = Random.Range(1, 45);
            return repeatRandom(spawnNumber);
        }
    }

    public void DeleteChest(GameObject chest)
    {
        GameObject temp = chests.Find(x => x == chest);
        chests.Remove(temp);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }

    public void CheckChests()
    {
        if (chests.Count < 20)
        {
            CreateChest();
        }
        Invoke("CheckChests", 10f);
    }

    public void CreateChest()
    {
        if (chests.Count < 20)
        {
            // ���� ������ ����
            // Random Chest Prefab (0 ~ 4)
            chestNumber = Random.Range(0, 5);

            // 20�� ����
            // Random spawn points (1 ~ 44)
            spawnNumber = Random.Range(1, 45);

            // ���� ������ �ߺ��� �ƴ��� Ȯ���ϱ�
            // Check duplicate
            if (repeatRandom(spawnNumber))
            {
                // �ߺ��� �ƴ� ���� ����
                tempChest = Instantiate(Chest[chestNumber], childrenSP[spawnNumber].position, childrenSP[spawnNumber].rotation) as GameObject;

                chests.Add(tempChest);

                // Random Item Counts (0 ~ 3)
                int countOfDrop = Random.Range(0, 4);

                // Create Random Items in ItemBag
                for (int j = 0; j < countOfDrop; j++)
                {
                    int randomIndex = Random.Range(0, countOfItems);
                    // �ش� itemBag�� ���� random item ���� �߰�
                    if (chestDict.TryGetValue(keysOfItems[randomIndex], out tempItemData))
                    {
                        tempChest.GetComponent<ChestBag>().AddItem(tempItemData);
                    }
                }

            }
        }
    }
}
