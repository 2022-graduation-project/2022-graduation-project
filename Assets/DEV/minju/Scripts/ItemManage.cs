using System.Collections;
using System.Collections.Generic;
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

    // Prefab
    public Object Chest;

    private int spawnNumber;

    // Start is called before the first frame update
    void Start()
    {
        // ��������Ʈ ���� ��������
        // bring every spawnpoints
        childrenSP = spawnPoints.gameObject.GetComponentsInChildren<Transform>();

        // ó�� ���� �� ����
        // Start to Spawn item chests
        for (int i = 0; i < 44; i++)
        {
            Instantiate(Chest, childrenSP[i].position, Quaternion.identity);
            //// 20�� ����
            //// Random spawn points (1 ~ 44)
            //spawnNumber = Random.Range(1, 45);

            //// ���� ������ �ߺ��� �ƴ��� Ȯ���ϱ�
            //// Check duplicate
            //if (repeatRandom(spawnNumber))
            //{
            //    // �ߺ��� �ƴ� ���� ����
            //    Instantiate(Chest, childrenSP[spawnNumber], );
            //}
            //else
            //    continue;
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
            spawnNumber = Random.Range(1, 51);
            return repeatRandom(spawnNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
