using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
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
            if(itemData.count > 1)
                count_img.gameObject.SetActive(true);
            else
                count_img.gameObject.SetActive(false);
        }
            
        else
        {
            itemData = _itemData.DeepCopy();
            icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
            icon.sprite.name = _itemData.image_name;

            count_txt.text = _itemData.count.ToString();
            if (itemData.count > 1)
                count_img.gameObject.SetActive(true);
            else
                count_img.gameObject.SetActive(false);

            SetColorA(1f);
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

    public void OnPointerClick(PointerEventData _eventData)
    {
        if(itemData != null)
        {
            print(itemData.image_name);
            UseItem();
            PlayerManager.instance.UseItem(itemData);
        }
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }

    void UseItem()
    {
        itemData.count--;
        Set();
    }

    void Set()
    {
        count_txt.text = itemData.count.ToString();
        if (itemData.count > 1)
            count_img.gameObject.SetActive(true);
        else if(itemData.count == 1)
            count_img.gameObject.SetActive(false);
        else
            Reset();
    }
}