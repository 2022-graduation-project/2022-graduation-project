using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour
{

    [SerializeField] Image icon;
    [SerializeField] Image count_img;
    [SerializeField] Text count_txt;

    public ItemData itemData = null;

    public void Set(ItemData _itemData)
    {
        if (itemData != null)
        {
            itemData.count += _itemData.count;
            count_txt.text = itemData.count.ToString();
            if (itemData.count > 1)
                count_img.gameObject.SetActive(true);
            else
                count_img.gameObject.SetActive(false);
        }

        if (itemData.count < 1)
            Reset();
    }

    public void Reset()
    {
        SetColorA(0f);
        icon.sprite = null;
        count_txt.text = string.Empty;
        count_img.gameObject.SetActive(false);
        itemData = null;
    }

    void UseItem()
    {

    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }

    void UpdateItemCount()
    {

    }
}