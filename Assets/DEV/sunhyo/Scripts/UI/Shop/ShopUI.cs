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
        temp.count = 1;
        temp.description = "마나 10을 회복합니다.";
        temp.price = 20000;
        itemDatas.Add(temp);
        /***************************************************************************/
        /***************************************************************************/
        /***************************************************************************/

        shopItemSlots = new List<ShopItemSlot>(content.GetComponentsInChildren<ShopItemSlot>());
        // itemData 불러오기
        //money = transform.Find("Money").GetComponent<Text>();
        //countPanel = transform.Find("Count").gameObject;

        money.text = PlayerManager.instance.playerData.money.ToString("N0") + "원";

        foreach (ShopItemSlot slot in shopItemSlots)
        {
            slot.SetShopScript(this);
        }

        shopItemSlots[0].Set(itemDatas[0]);
        shopItemSlots[1].Set(itemDatas[1]);

        CheckActiveSlot(PlayerManager.instance.playerData.money);
    }

    public void Buy(ItemData _itemData)
    {
        // 얘는 shopitemslot 에서 넘어오는 shopUI의 itemData임
        // 그래서 판매를 하는 경우, 얘의 count가 --되는데
        // 이게 이후 구매에서도 0개를 구매하게 만드는 상황이 발생한 것
        // 해결을 위해 일단은 인스턴스를 만들지만...
        // 다른 방법을 강구해봐야한다
        // 전체적으로 코드가 꼬인 느낌인걸 ㅋㅋ ~~ ㅜㅜ

        ItemData newItemData = _itemData.DeepCopy();
        int count = 1;
        int curMoney;
        // 수량 선택 창 생성

        InventoryUI.instance.GetMoney(-newItemData.price * count);
        curMoney = PlayerManager.instance.playerData.money;
        money.text = curMoney.ToString("N0") + "원";
        InventoryUI.instance.AddItem(newItemData, count);
        CheckActiveSlot(curMoney);
    }

    public void Sell(ItemData _itemData)
    {
        // 수량 선택 창 생성
        // 수량 * 가격(0.7) 만큼 플레이어 돈 추가
        // 플레이어 인벤토리에 수량만큼 아이템 제거
        // itemSlot들 접근해서 구매 가능 여부 확인 및 설정
        print("판매");

        int count = 1;
        int curMoney;
        // 수량 선택 창 생성

        if(InventoryUI.instance.UseItem(_itemData, count))
        {
            InventoryUI.instance.GetMoney((int)(_itemData.price * 0.7f) * count);
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
        if(DragSlot.instance.itemData.count == 0)
            DragSlot.instance.Reset();
    }
}



// ㅜㅜ 코드 망했어
// itemData 쓰는 코드들은 다시 짜야함
// itemData에 직접 접근할 수 있어서 생기는 문제...
// itemData 관리는 무조건 inventory에서만 하는 걸로 하자...!!!
// 그 외 shopUI 같은 곳에서는 그냥 함수 운용이랑 변경값 전달하는 역할만...