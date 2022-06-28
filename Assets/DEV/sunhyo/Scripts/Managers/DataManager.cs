using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Text;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
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
     * Assets �������� ������ �̸��� json ã�Ƽ� ��ȯ
     * 
     * @ params - ������ ����, ���� ���, ���� �̸�
     * @ return - T �������� ����ȭ �� ������
     * @ exception - X
    *****************************************/
    public T LoadJsonFile<T>(string loadPath, string fileName)
    {
        // ����Ʈ �ڵ�� ���� �ҷ�����
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        // ������ ���ڵ� �� T�� ����ȭ�Ͽ� ��ȯ
        string jsonData = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<T>(jsonData);
    }





    /*****************************************
     * Assets �������� ������ �̸��� sprite�� ã�Ƽ� ��ȯ
     * 
     * @ param - ���� ���, ���� �̸�
     * @ return - sprite
     * @ exception - X
    *****************************************/
    public Sprite LoadSpriteFile(string loadPath, string fileName)
    {
        // ����Ʈ �ڵ�� ���� �ҷ�����
        FileStream fileStream = new FileStream(string.Format("{0}/{1}.png", loadPath, fileName), FileMode.Open);
        byte[] data = new byte[fileStream.Length];
        fileStream.Read(data, 0, data.Length);
        fileStream.Close();

        // sprite ���� ���� �� ����Ʈ �ڵ带 �ؽ��ķ� ��ȯ
        int width = 100;
        int height = 100;
        Texture2D texture = new Texture2D(width, height);
        texture.LoadImage(data);

        // �ؽ��� -> sprite ��ȯ �� ��ȯ
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        return sprite;
    }
}