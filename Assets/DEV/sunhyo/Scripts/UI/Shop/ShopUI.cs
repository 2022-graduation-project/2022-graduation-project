using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ShopUI : MonoBehaviour, IDropHandler
{
    private List<ShopItemSlot> shopItemSlots;
    private List<ItemData> itemDatas = new List<ItemData>();

    [SerializeField] private Text money;
    [SerializeField] private GameObject countPanel;

    private float preminum = 1f;
    private float discount = 0.7f;

    public GameObject content;

    void Start()
    {
        /***************************************************************************/
        /******************************     Test     *******************************/
        /***************************************************************************/
        ItemData temp = new ItemData();
        temp.image_name = "000_hpPotion";
        temp.item_name = "체력 회복 포션";
        temp.count = 1;
        temp.description = "체력 10을 회복합니다.";
        temp.price = 10000;
        itemDatas.Add(temp);

        temp = new ItemData();
        temp.image_name = "001_mpPotion";
        temp.item_name = "마나 회복 포션";
        temp.count = 2;
        temp.description = "마나 10을 회복합니다.";
        temp.price = 20000;
        itemDatas.Add(temp);

        temp = new ItemData();
        temp.image_name = "002_apple";
        temp.item_name = "사과";
        temp.count = 3;
        temp.description = "맛있습니다.";
        temp.price = 30000;
        itemDatas.Add(temp);
        /***************************************************************************/
        /***************************************************************************/
        /***************************************************************************/

        shopItemSlots = new List<ShopItemSlot>(content.GetComponentsInChildren<ShopItemSlot>());
        // itemData 불러오기
        money = transform.Find("Money").GetComponent<Text>();
        countPanel = transform.Find("Count").gameObject;

        money.text = PlayerManager.instance.playerData.money.ToString("N0") + "원";

        foreach (ShopItemSlot slot in shopItemSlots)
        {
            slot.SetShopScript(this);
        }

        shopItemSlots[0].Set(itemDatas[0]);
        shopItemSlots[1].Set(itemDatas[1]);
        shopItemSlots[2].Set(itemDatas[2]);

        CheckActiveSlot(PlayerManager.instance.playerData.money);
    }

    public void Buy(ItemData _itemData)
    {
        int curMoney;
        // 수량 선택 창 생성
        int count = 1;

        if(UIManager.instance.BuyItem(_itemData, preminum, count))
        {
            curMoney = PlayerManager.instance.playerData.money;
            money.text = curMoney.ToString("N0") + "원";
            CheckActiveSlot(curMoney);
        }
    }

    public void Sell(ItemData _itemData)
    {
        int curMoney;
        // 수량 선택 창 생성
        int count = 1;

        if (UIManager.instance.SellItem(_itemData, discount, count))
        {
            curMoney = PlayerManager.instance.playerData.money;
            money.text = curMoney.ToString("N0") + "원";
            CheckActiveSlot(curMoney);
        }
    }

    void CheckActiveSlot(int _money)
    {
        foreach(ShopItemSlot slot in shopItemSlots)
        {
            if (slot.itemData == null)
                continue;

            if (slot.itemData.price > _money)
            {
                slot.SetActiveSlot(false);
            }
            else
            {
                slot.SetActiveSlot(true);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.itemData == null)
            return;

        Sell(DragSlot.instance.itemData);
    }
}



// ㅜㅜ 코드 망했어
// itemData 쓰는 코드들은 다시 짜야함
// itemData에 직접 접근할 수 있어서 생기는 문제...
// itemData 관리는 무조건 inventory에서만 하는 걸로 하자...!!!
// 그 외 shopUI 같은 곳에서는 그냥 함수 운용이랑 변경값 전달하는 역할만...