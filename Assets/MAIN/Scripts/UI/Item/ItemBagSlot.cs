using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemBagSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text item_name;
    [SerializeField] Text count;

    public ItemData itemData = null;


    private ItemBag itemBag;

    public void Set(ItemBag _itemBag, ItemData _itemData)
    {
        itemBag = _itemBag;

        itemData = _itemData.DeepCopy();
        icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
        item_name.text = _itemData.item_name;
        count.text = _itemData.count.ToString();
        SetColorA(1f);

        gameObject.SetActive(true);
    }
    
    public void Reset()
    {
        SetColorA(0f);
        icon.sprite = null;
        item_name.text = string.Empty;
        count.text = string.Empty;
        itemData = null;

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        if(itemData != null)
        {
            itemBag.PopItem(itemData);
            //UIManager.instance.inventoryUI.AddItem(itemData);
            Reset();
        }
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }
}