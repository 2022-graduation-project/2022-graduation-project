using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    /* Data */
    public PlayerData playerData;
    public PlayerUI playerUI;
    public PlayerCombat playerCombat;

    public Object[] jobs;

    private string playerJob = "";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        playerData = DataManager.instance.playerData;
    }





    /* 수정 : 한 번 쓰는 애니까 인스턴스로 생성하면 좋을 듯 한데 */




    /************************************************/
    /*                   직업 저장                   */
    /************************************************/

    /*  로그인 화면에서 로딩화면 부를 때 호출
     *  플레이어의 직업을 저장해준다.       */
    public void SetJob(string select)
    {
        playerJob = select;
        print("Job selected = " + playerJob);
    }

    /*  로딩 화면에서 Town씬 부를 때 호출
     *  저장해둔 직업의 플레이어를 첫 생성.  */
    public void CreatePlayer()
    {
        int choose = 0;
        switch (playerJob)
        {
            case "Archer":
                choose = 0;
                break;
            case "Warrior":
                choose = 1;
                break;
            case "Wizard":
                choose = 2;
                break;
        }

        PlayerJsonUpdate(playerJob);

        DontDestroyOnLoad(Instantiate(jobs[choose]));
    }

    /*  캐릭터 생성될 때 호출
     *  플레이어 직업을 Json 파일 Update   */
    private void PlayerJsonUpdate(string job_loc)
    {
        // Load previous Player Json File
        Dictionary<string, PlayerData> playerDict =
                    DataManager.instance.LoadJsonFile
                    <Dictionary<string, PlayerData>>
                    (Application.dataPath + "/MAIN/Data", "player");

        playerData = playerDict["000_player"];

        playerData.job = job_loc;

        if (DataManager.instance.ObjectToJson<Dictionary<string, PlayerData>>(Application.dataPath + "/MAIN/Data", "player", playerDict))
        {
            print("Player's job data has updated.");
        }
    }



    /********************************************/
    /************* PlayerUI 업데이트 *************/
    /********************************************/

    public void UpdateHp(float _delta)
    {
        playerData.curHp += _delta;
        playerUI.UpdateHpBar(playerData.maxHp, playerData.curHp);
    }

    public void UpdateMp(float _delta)
    {
        playerData.curMp += _delta;
        playerUI.UpdateMpBar(playerData.maxMp, playerData.curMp);
    }





    /********************************************/
    /**************** 아이템 사용 ****************/
    /********************************************/

    public void UseItem(int _type)
    {
        playerUI.ItemQuickSlot(_type);
    }
}