using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChestUI : MonoBehaviour
{
    /**************************************************/
    /* 1. itemBagSlot 직접 할당 말고 ItemSlot 가변 할당 */
    /**************************************************/

    public GameObject[] chestBagSlots;

    public void Set(ChestBag _chestBag)
    {
        
        foreach (ChestData chest in _chestBag.chests)
        {
            GameObject chestBagSlot = GetItemSlotInPool();

            chestBagSlot?.GetComponent<ChestBagSlot>().Set(_chestBag, chest);
        }

        gameObject.SetActive(true);
    }

    public void CallResetWithDelay()
    {
        Invoke("Reset", 3.0f);
    }

    public void Reset(ChestBag _chestBag)
    {
        foreach (GameObject chestBagSlot in chestBagSlots)
        {
            if (chestBagSlot.activeSelf)
                chestBagSlot.GetComponent<ChestBagSlot>().Reset();
        }

        if(_chestBag.chests.Count <= 0)
            Destroy(_chestBag.gameObject);

        gameObject.SetActive(false);
    }

    GameObject GetItemSlotInPool()
    {
        foreach (GameObject chestBagSlot in chestBagSlots)
        {
            if (!chestBagSlot.activeSelf)
                return chestBagSlot;
        }

        return null;
    }

    void Test(ChestData item)
    {
        print(item);
        print(item.image_name);
        print(item.item_name);
        print(item.count);
        print(item.description);
    }
}