using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpPotion : MonoBehaviour, ItemUse
{

    void ItemUse.Use()
    {
        // 아이템 이름 가져오기
        string img_name = GetComponent<Image>().sprite.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
