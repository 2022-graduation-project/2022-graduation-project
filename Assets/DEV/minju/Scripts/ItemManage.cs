using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManage : MonoBehaviour
{
    // 스폰포인트
    // array of spawn points
    public Transform spawnPoints;
    public Transform[] childrenSP;

    // 스폰포인트 중복 관리
    // For Random Spawn without duplicate
    public int[] duplicate;

    // Prefab
    public Object Chest;

    private int spawnNumber;

    // Start is called before the first frame update
    void Start()
    {
        // 스폰포인트 전부 가져오기
        // bring every spawnpoints
        childrenSP = spawnPoints.gameObject.GetComponentsInChildren<Transform>();

        // 처음 시작 시 스폰
        // Start to Spawn item chests
        for (int i = 0; i < 44; i++)
        {
            Instantiate(Chest, childrenSP[i].position, Quaternion.identity);
            //// 20개 생성
            //// Random spawn points (1 ~ 44)
            //spawnNumber = Random.Range(1, 45);

            //// 랜덤 생성이 중복이 아닌지 확인하기
            //// Check duplicate
            //if (repeatRandom(spawnNumber))
            //{
            //    // 중복이 아닐 때만 생성
            //    Instantiate(Chest, childrenSP[spawnNumber], );
            //}
            //else
            //    continue;
        }
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
            spawnNumber = Random.Range(1, 51);
            return repeatRandom(spawnNumber);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
