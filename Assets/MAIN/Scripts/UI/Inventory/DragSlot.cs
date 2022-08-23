using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour, ISlot
{
    public Image item_icon;
    public string item_code;
    public int item_count;

    public InventorySlot slot;

    public static DragSlot instance = null;
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

    public void ResetSlot()
    {
        item_code = "";
        item_count = 0;
        SetColorA(0f);
        item_icon.sprite = null;
        slot = null;
    }

    public void SetSlot(string _item_code, int _item_count)
    {
        item_code = _item_code;
        item_count = _item_count;

        if (item_code != "")
        {
            item_icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/MAIN/Images", item_code);
            SetColorA(1f);
        }
    }

    public void SetSlot(InventorySlot _slot)
    {
        slot = _slot;
        SetSlot(slot.item_code, slot.item_count);
    }

    public void SetColorA(float _delta)
    {
        if (item_icon == null)
            return;

        Color col = item_icon.color;
        col.a = _delta;
        item_icon.color = col;
    }
}
