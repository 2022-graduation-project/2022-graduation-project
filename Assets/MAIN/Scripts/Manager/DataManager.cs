using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    public PlayerData playerData;
    public List<InventoryData> inventory;
    public Dictionary<string, ItemData> itemDict;
    public Dictionary<string, MonsterData> monsterDict;

    public static DataManager instance = null;
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

        SetData();
    }

    void SetData()
    {
        playerData = DataManager.instance.LoadJsonFile
                   <Dictionary<string, PlayerData>>
                   (Application.dataPath + "/MAIN/Data/", "player")
                   ["000_player"];

        inventory = DataManager.instance.LoadJsonFile
                    <List<InventoryData>>
                    (Application.dataPath + "/MAIN/Data/", "inventory");

        //foreach (InventoryData data in inventory)
        //{
        //    print($"{data.item_code}, {data.item_count}");
        //}

        //itemDict = DataManager.instance.LoadJsonFile
        //           <Dictionary<string, ItemData>>
        //           (Application.dataPath + "/MAIN/Data/", "item");

        //monsterDict = DataManager.instance.LoadJsonFile
        //              <Dictionary<string, MonsterData>>
        //              (Application.dataPath + "/MAIN/Data/", "monster");
    }

    public string ObjectToJson(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public T JsonToObject<T>(string jsonData)
    {
        return JsonConvert.DeserializeObject<T>(jsonData);
    }




    /*****************************************
     * Assets 폴더에서 동일한 이름의 json 찾아서 반환
     * 
     * @ params - 데이터 형식, 파일 경로, 파일 이름
     * @ return - T 형식으로 구조화 된 데이터
     * @ exception - X
    *****************************************/
    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        // 바이트 코드로 파일 불러오기
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        // 데이터 인코딩 및 T로 구조화하여 반환
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }

    public bool ObjectToJson<T>(string createPath, string fileName, T obj)
    {
        // 존재하는 파일이 아니면 입력값이 틀렸다는 것이므로 false 반환
        try
        {
            // filestram 인스턴스 생성 외에 다른 방법 있는지 찾아보기
            FileStream temp = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Open);
            temp.Close();
        }
        catch (FileNotFoundException e)
        {
            return false;
        }

        string jsonData = JsonConvert.SerializeObject(obj, Formatting.Indented);
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        fileStream.Write(data, 0, data.Length);
        fileStream.Close();
        return true;
    }




    /*****************************************
     * Assets 폴더에서 동일한 이름의 sprite를 찾아서 반환
     * 
     * @ param - 파일 경로, 파일 이름
     * @ return - sprite
     * @ exception - X
    *****************************************/
    public Sprite LoadSpriteFile(string loadPath, string fileName)
    {
        // 바이트 코드로 파일 불러오기
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.png", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        // sprite 형식 지정 및 바이트 코드를 텍스쳐로 변환
        int width = 100;
        int height = 100;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(data);

        // 텍스쳐 -> sprite 변환 및 반환
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
}