using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterItem : MonoBehaviour
{
    public ItemData item;
    public ItemBag itemBag;
    private string[] itemName = { "000_apple", "001_meat", "002_gem", "003_book", "004_hpPotion", "005_sword", "006_mpPotion" };



    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            transform.gameObject.SetActive(false);

            int index = Random.Range(0, 7);
            item = DataManager.instance.LoadJsonFile
                <Dictionary<string, ItemData>>
                (Application.dataPath + "/MAIN/Data", "item")
                [itemName[index]];

            itemBag.AddItem(item);
            Debug.Log(item.item_name + " 획득!");
        }
    }
}
