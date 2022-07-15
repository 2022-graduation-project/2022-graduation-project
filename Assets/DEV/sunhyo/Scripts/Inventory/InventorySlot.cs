using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
<<<<<<< .merge_file_a09464

    [SerializeField] private Image icon;
    [SerializeField] private Image count_img;
    [SerializeField] private Text count_txt;
=======
    [SerializeField] Image icon;
    [SerializeField] Image count_img;
    [SerializeField] Text count_txt;
    GameObject iconObject;
>>>>>>> .merge_file_a14144

    public ItemData itemData = null;

    public void Set(ItemData _itemData)
    {
<<<<<<< .merge_file_a09464
        // itemData가 비어있거나 || 다른 아이템
        if (itemData == null || itemData.item_name != _itemData.item_name)
=======
        iconObject = transform.GetChild(0).gameObject;

        if (itemData != null)
>>>>>>> .merge_file_a14144
        {
            // itemData = _itemData.DeepCopy();
            itemData = _itemData; // InventorUI에서 넘어오니까 인스턴스 생성 없어도 가능할지도?
            icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
            icon.sprite.name = _itemData.image_name;

            // 자식 오브젝트 Icon에 해당 아이템 종류의 스크립트 컴포넌트 생성
            CreateItemComponent();

            count_txt.text = _itemData.count.ToString();
            if (itemData.count > 1)
                count_img.gameObject.SetActive(true);
            else
                count_img.gameObject.SetActive(false);

            SetColorA(1f);
        }
        else // 동일 아이템
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
<<<<<<< .merge_file_a09464
        //print("리셋");
=======
        // 자식 오브젝트 Icon에서 아이템 스크립트 컴포넌트 삭제
        DeleteItemComponent();

>>>>>>> .merge_file_a14144
        SetColorA(0f);
        icon.sprite = null;
        count_txt.text = string.Empty;
        count_img.gameObject.SetActive(false);
        itemData = null;
    }

    void UseItem()
    {
        if(InventoryUI.instance.UseItem(this.itemData))
        {
<<<<<<< .merge_file_a09464

=======
            print(itemData.image_name);
            UseItem();
            //PlayerManager.instance.UseItem(itemData);
>>>>>>> .merge_file_a14144
        }
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }

    void UpdateItemCount()
    {
<<<<<<< .merge_file_a09464

=======
        // 자식 오브젝트 Icon의 
        // 아이템 스크립트 Use() 호출하기
        CallEachItemUse();
        itemData.count--;
        Set();
>>>>>>> .merge_file_a14144
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //print("button : " + eventData.button);
        //print("clickCount : " + eventData.clickCount);
        //print("name : " + gameObject.name);

        /* clickCount는 일부 기기에서 씹히는 경우도 있다고 함. 시간 계산으로 변경하는 게 좋을 듯 */
        /* 좌클릭 및 드래그 : 아이템 이동 */
        if (itemData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            /* 좌클릭 * 2 : 아이템 사용 / 장비 장착 */
            if(eventData.clickCount > 1)
            {
                UseItem();
            }
        }

        /* 우클릭 : 아이템 사용 / 장비 장착 */
        else if(itemData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemData == null)
            return;
        
        SetColorA(0.3f);

        DragSlot.instance.Set(itemData);
        DragSlot.instance.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemData == null || DragSlot.instance.itemData == null)
            return;

        DragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemData == null)
            return;

        // 아이템 이동 완료
        if(DragSlot.instance.itemData == null)
        {
            Reset();
            return;
        }

        // Swap
        if (DragSlot.instance.itemData != itemData)
        {
            Set(DragSlot.instance.itemData);
            DragSlot.instance.Reset();
            DragSlot.instance.gameObject.SetActive(false);
            return;
        }

        //아이템 버리기
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            Reset();
            DragSlot.instance.Reset();
            DragSlot.instance.gameObject.SetActive(false);
            return;
        }
        //try
        //{
        //    print(eventData.pointerCurrentRaycast.gameObject);
        //    print(eventData.pointerCurrentRaycast.gameObject.name);
        //}
        //catch (NullReferenceException e)
        //{
        //    print("Null exception");
        //}
        

        SetColorA(1f);
        DragSlot.instance.Reset();
        DragSlot.instance.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.itemData == null)
            return;

        if (itemData == null) // 빈 슬롯인 경우
        {
            Set(DragSlot.instance.itemData);
            DragSlot.instance.Reset();
        }
        else if(DragSlot.instance.itemData != itemData) // Swap
        {
            Set(DragSlot.instance.itemData);
        }
    }

    /*****************************************
     * (아이템 추가 시)
     * 해당 아이템 역할 부여
     * 
     * @ param - X
     * @ return - X
     * @ exception - X
    ******************************************/
    void CreateItemComponent()
    {
        switch (itemData.image_name)
            {
            case "000_hpPotion":
                iconObject.AddComponent<HpPotion>();
                break;
            case "001_mpPotion":
                iconObject.AddComponent<MpPotion>();
                break;
            case "002_apple":
                iconObject.AddComponent<HpPotion>();
                break;
            case "003_meat":
                iconObject.AddComponent<HpPotion>();
                break;
            case "004_gosu":
                iconObject.AddComponent<MpPotion>();
                break;
            case "005_sheildPotion":
                iconObject.AddComponent<ShieldPotion>();
                break;
            case "006_bomb":
                iconObject.AddComponent<Bomb>();
                break;
            }
    }

    /*****************************************
     * (아이템 삭제 시)
     * 해당 아이템 역할 삭제
     * 
     * @ param - X
     * @ return - X
     * @ exception - X
    ******************************************/
    void DeleteItemComponent()
    {
        switch (itemData.image_name)
        {
            case "000_hpPotion":
                Destroy(iconObject.GetComponent<HpPotion>());
                break;
            case "001_mpPotion":
                Destroy(iconObject.GetComponent<MpPotion>());
                break;
            case "002_apple":
                Destroy(iconObject.GetComponent<HpPotion>());
                break;
            case "003_meat":
                Destroy(iconObject.GetComponent<HpPotion>());
                break;
            case "004_gosu":
                Destroy(iconObject.GetComponent<MpPotion>());
                break;
            case "005_sheildPotion":
                Destroy(iconObject.GetComponent<ShieldPotion>());
                break;
            case "006_bomb":
                Destroy(iconObject.GetComponent<Bomb>());
                break;
        }
    }
    
    /*****************************************
     * (아이템 클릭 시)
     * 해당 아이템 역할 부르기
     * 
     * @ param - X
     * @ return - X
     * @ exception - X
    ******************************************/
    void CallEachItemUse()
    {
        switch (itemData.image_name)
        {
            case "000_hpPotion":
                iconObject.GetComponent<HpPotion>().Use();
                break;
            case "001_mpPotion":
                iconObject.GetComponent<MpPotion>().Use();
                break;
            case "002_apple":
                iconObject.GetComponent<HpPotion>().Use();
                break;
            case "003_meat":
                iconObject.GetComponent<HpPotion>().Use();
                break;
            case "004_gosu":
                iconObject.GetComponent<MpPotion>().Use();
                break;
            case "005_sheildPotion":
                iconObject.GetComponent<ShieldPotion>().Use();
                break;
            case "006_bomb":
                iconObject.GetComponent<Bomb>().Use();
                break;
        }
    }
}