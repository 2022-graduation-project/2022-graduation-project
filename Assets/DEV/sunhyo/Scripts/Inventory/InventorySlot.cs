using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text count;

    public ItemData itemData = null;

    public void Set(ItemData _itemData)
    {
        if (itemData != null)
            count.text = itemData.count++.ToString();
        else
        {
            itemData = _itemData;
            print(_itemData.image_name);
            count.text = _itemData.count.ToString();
            icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
            SetColorA(1f);
        }
    }

    public void Reset()
    {
        SetColorA(0f);
        icon.sprite = null;
        count.text = string.Empty;
        itemData = null;
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        if(itemData != null)
            print("아이템 사용");
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }
}