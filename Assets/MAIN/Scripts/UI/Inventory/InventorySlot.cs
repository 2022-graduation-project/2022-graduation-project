using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, ISlot
{
    public IItem item_script;

    public Image item_icon;
    public GameObject count_obj;
    public Text count_txt;

    public string item_code;
    public int item_count;

    public void SetSlot(string _item_code, int _item_count)
    {
        SetColorA(1f);

        item_code = _item_code;
        item_count = _item_count;

        if (item_code != "")
        {
            item_icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/MAIN/Images/Item/", item_code);
            SetItemScript();
        }
        else
        {
            ResetSlot();
        }

        if (item_count > 1)
        {
            SetCountObj(item_count);
        }
        else if(item_count == 1)
        {
            ResetCountObj();
        }
        else
        {
            ResetSlot();
        }
    }

    public void ResetSlot()
    {
        SetColorA(0f);

        item_code = "";
        item_count = 0;
        item_icon.sprite = null;
        item_script = null;

        ResetCountObj();
    }

    public void SetColorA(float _delta)
    {
        Color col = item_icon.color;
        col.a = _delta;
        item_icon.color = col;
    }
    public void SetCountObj(int _count)
    {
        count_obj.SetActive(true);
        count_txt.text = _count.ToString();
    }

    public void ResetCountObj()
    {
        count_obj.SetActive(false);
        count_txt.text = null;
    }

    public void SetItemScript()
    {
        if (ItemPool.instance.pool.ContainsKey(item_code))
        {
            item_script = ItemPool.instance.pool[item_code];
        }
    }
}