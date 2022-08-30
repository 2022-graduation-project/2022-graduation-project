using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [Header("Tips Text")]
    public TMP_Text tipsText;

    private string[] tips = new string[]{
    "플레이어의 체력이 다 닳아 죽으면 마을로 소환되며, 아이템을 랜덤으로 잃게 됩니다.",
    "플레이어가 한 번 죽으면, 몬스터 던전은 모두 초기화됩니다."};

    private int randIdx;

    // Start is called before the first frame update
    void Start()
    {
        randIdx = Random.Range(0, 2);
        tipsText.text += tips[randIdx];
        timer = 0;
        Invoke("loadTown", 4.0f);
    }


    public void loadTown()
    {
        PlayerManager.instance.CreatePlayer();
        SceneManager.LoadScene("Gate");
    }

    public Image loadingBar;
    private float timer;
    public void loadingProgress()
    {
        if (timer <= 4.0f)
        {
            loadingBar.fillAmount = timer / 4.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 4.0f)
        {
            timer += 1 * Time.deltaTime;
        }
        loadingProgress();
    }
}
