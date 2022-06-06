using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TipsManager : MonoBehaviour
{
    [Header("Tips Text")]
    public TMP_Text tipsText;

    private string[] tips = new string[]{ 
    "플레이어의 배고픔 상태가 일정 수준 이하로 내려가면, 체력이 조금씩 깎입니다. 아이템 파밍이나 몬스터 사냥을 통해 음식을 획득하세요.",
    "보스몬스터들은 성격이 까다롭기에, 플레이어가 이전에 먹은 음식에 따라 체력과 공격력이 변합니다.",
    "파밍으로 얻은 식재료를 요리시설에서 조합하여 새로운 음식을 획득할 수 있습니다.",
    "플레이어의 체력이 다 닳아 죽으면 마을로 소환되며, 아이템을 랜덤으로 잃게 됩니다.",
    "플레이어가 한 번 죽으면, 몬스터 던전은 모두 초기화됩니다."};

    private int randIdx;

    // Start is called before the first frame update
    void Start()
    {
        randIdx = Random.Range(0, 5);
        tipsText.text += tips[randIdx];
        Invoke("loadTown", 4.0f);
    }

    public void loadTown()
    {
        SceneManager.LoadScene("Town");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
