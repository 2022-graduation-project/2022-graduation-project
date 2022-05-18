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
        icon = null;
        count.text = string.Empty;
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        print("클릭");
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }
}