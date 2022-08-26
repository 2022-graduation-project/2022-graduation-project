using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] public Image icon;
    [SerializeField] public Text item_name;
    [SerializeField] public Text price_txt;
    [SerializeField] public GameObject block;

    public bool clickable = true;
    public string item_code;
    public int price;

    private float lastClick = 0;
    private float doubleClick = 0.5f;

    private ShopUI shop;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (lastClick <= 0)
        {
            lastClick = eventData.clickTime;
        }
        else
        {
            if (eventData.clickTime - lastClick < doubleClick)
            {
                shop.Buy(item_code);
            }

            lastClick = 0;
        }
    }

    public void Set(string _item_code, ShopUI _shop)
    {
        item_code = _item_code;

        shop = _shop;

        icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/MAIN/Images/Item/", item_code);
        item_name.text = DataManager.instance.itemDict[item_code].item_name;

        price = DataManager.instance.itemDict[item_code].price;
        price_txt.text = price.ToString("N0") + "원";

        SetColorA(1f);
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

    public void SetColorA(float _delta)
    {
        Color col = icon.color;
        col.a = _delta;
        icon.color = col;
    }
}