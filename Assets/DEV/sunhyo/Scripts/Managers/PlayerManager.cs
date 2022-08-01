using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class PlayerManager : MonoBehaviour
{
    /* Data */
    public PlayerData playerData;

    /* Input */
    public bool keyMoveable = true;
    public bool mouseMoveable = true;


    /* Singleton */
    public static PlayerManager instance;

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

        playerData = DataManager.instance.LoadJsonFile
                    <Dictionary<string, PlayerData>>
                    (Application.dataPath + "/MAIN/Data", "player")
                    ["000_player"];

    }

    // void Start()
    // {
    //    playerData = DataManager.instance.LoadJsonFile
    //                <Dictionary<string, PlayerData>>
    //                (Application.dataPath + "/MAIN/Data", "player")
    //                ["000_player"];

    //    //foreach(Dictionary<string, ItemData> dic in playerData.inventory)
    //    //{
    //    //    foreach (KeyValuePair<string, ItemData> itemData in dic)
    //    //        print(itemData.Key);
    //    //}

    //    //UIManager.instance.Set(playerData);
    // }


    /************************************************/
    /*                   직업 저장                   */
    /************************************************/
    private string playerJob = "";

    /*  로그인 화면에서 로딩화면 부를 때 호출
     *  플레이어의 직업을 저장해준다.       */
    public void SetJob(string select)
    {
        playerJob = select;
        print("Job selected = "+playerJob);
    }

    /*  로딩 화면에서 Town씬 부를 때 호출
     *  저장해둔 직업의 플레이어를 첫 생성.  */
    public Object[] jobs;
    public void CreatePlayer()
    {
        int choose=0;
        switch(playerJob)
        {
            case "Archer":
                choose=0;
                break;
            case "Warrior":
                choose=1;
                break;
            case "Wizard":
                choose=2;
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

        if(DataManager.instance.ObjectToJson<Dictionary<string, PlayerData>>(Application.dataPath + "/MAIN/Data", "player", playerDict))
        {
            print("Player's job data has updated.");
        }
    }

    /************************************************/
    /*                   상태 이상                   */
    /************************************************/
    public IEnumerator Healing(float healAmount)
    {

        instance.playerData.curHp += healAmount;
        UIManager.instance.playerUI.UpdateHpBar(instance.playerData.maxHp, instance.playerData.curHp);
        yield break;
    }
    public IEnumerator RefillMana(float manaAmount)
    {

        instance.playerData.curHp += manaAmount;
        UIManager.instance.playerUI.UpdateMpBar(instance.playerData.maxMp, instance.playerData.curMp);
        yield break;
    }
    public IEnumerator Shielding(float shieldDuration)
    {
        PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        float duration = 0;
        while (duration < shieldDuration)
        {
           player.canDamage=true;
           yield return new WaitForSeconds(1f);
           duration++;
           if (duration >= shieldDuration)
           {
               player.canDamage=false;
               yield break;
           }
        }
        yield break;
    }
















    /**************************************************/
    /****************  아이템 관련 작업  ***************/
    /**************************************************/


    public bool AddItem(ItemData _itemData, int count=1)
    {
        return false;
    }

    public bool ReduceItem(ItemData _itemData, int count = 1)
    {
        return false;
    }

    public bool DeleteItem(ItemData itemData)
    {
        return false;
    }


    /**************************************************/
}