using System.Collections;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    void Start()
    {
        PlayerData playerData = PlayerManager.instance.playerData;
        
        // DataManager.instance.JsonToObject<PlayerData>(playerData);
    }
}