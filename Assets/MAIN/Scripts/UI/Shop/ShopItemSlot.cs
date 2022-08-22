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
    //public ItemData itemData = null;
    public string item_code;
    public int price;

    public void Set(string _item_code)
    {
        item_code = _item_code;
        icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", item_code);
        //item_name.text = _itemData.item_name;
        //price.text = _itemData.price.ToString("N0") + "원";

        // datamanager에 있는 item 목록에 이름을 key로 접근해서 받아오는 형식
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
        //if (item_code == null || !clickable)
        //    return;
        //print("구매");
        //shop.Buy(itemData);
    }
}