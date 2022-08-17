using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public Image icon;
    [SerializeField] public Text item_name;
    [SerializeField] public Text price;
    [SerializeField] public GameObject block;

    [SerializeField] private ShopUI shop;

    public bool clickable = true;
    public ItemData itemData = null;

    public void SetShopScript(ShopUI _shop)
    {
        shop = _shop;
    }

    public void Set(ItemData _itemData)
    {
        itemData = _itemData;
        icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
        item_name.text = _itemData.item_name;
        price.text = _itemData.price.ToString("N0") + "원";
    }

    public void SetActiveSlot(bool type)
    {
        if(type)
        {
            clickable = true;
            block.SetActive(false);
        }
        else
        {
            clickable = false;
            block.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemData == null || !clickable)
            return;
        print("구매");
        shop.Buy(itemData);
    }
}


// 인벤토리를 따로 만들어야 할듯