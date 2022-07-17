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

    void Start()
    {
       playerData = DataManager.instance.LoadJsonFile
                   <Dictionary<string, PlayerData>>
                   (Application.dataPath + "/MAIN/Data", "player")
                   ["000_player"];

       //foreach(Dictionary<string, ItemData> dic in playerData.inventory)
       //{
       //    foreach (KeyValuePair<string, ItemData> itemData in dic)
       //        print(itemData.Key);
       //}

       //UIManager.instance.Set(playerData);
    }


    /************************************************/
    /*                   직업 선택                   */
    /************************************************/
    private string playerJob = "";

    public void SetJob(string select)
    {
        playerJob = select;
        print("Job selected = "+playerJob);
    }
    
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
        
        DontDestroyOnLoad(Instantiate(jobs[choose]));
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
}