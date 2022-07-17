using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private Image icon;
    [SerializeField] private Image count_img;
    [SerializeField] private Text count_txt;

    public ItemData itemData = null;
    public Item itemScript = null;

    public void Set(ItemData _itemData, Item _itemScript=null)
    {
        // itemData가 비어있거나 || 다른 아이템
        if (itemData == null || itemData.item_name != _itemData.item_name)
        {
            if (itemScript == null && _itemScript == null)
            {
                print(_itemData.image_name.Substring(4));
                itemScript = gameObject.AddComponent(Type.GetType(_itemData.image_name.Substring(4))) as Item;
            }

            print(itemScript);
            // itemData = _itemData.DeepCopy();
            itemData = _itemData; // InventorUI에서 넘어오니까 인스턴스 생성 없어도 가능할지도?
            icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);

            SetColorA(1f);
        }

        SetCountObj();
    }

    public void Reset()
    {
        itemScript = null;

        //print("리셋");
        SetColorA(0f);
        icon.sprite = null;
        count_txt.text = string.Empty;
        count_img.gameObject.SetActive(false);
        itemData = null;
    }

    public void SetCountObj()
    {
        if (itemData.count > 1)
        {
            count_txt.text = itemData.count.ToString();
            count_img.gameObject.SetActive(true);
        }
            
        else if (itemData.count == 1)
            count_img.gameObject.SetActive(false);
        else // 아이템 소진
        {
            InventoryUI.instance.DeleteItem(itemData);
            Reset();
        }
    }

    void UseItem()
    {
        itemScript.Use();
        itemData.count--;
        count_txt.text = itemData.count.ToString("N0");

        SetCountObj();
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }

    void UpdateItemCount()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        print("button : " + eventData.button);
        //print("clickCount : " + eventData.clickCount);
        //print("name : " + gameObject.name);

        /* clickCount는 일부 기기에서 씹히는 경우도 있다고 함. 시간 계산으로 변경하는 게 좋을 듯 */
        /* 좌클릭 및 드래그 : 아이템 이동 */
        if (itemData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            /* 좌클릭 * 2 : 아이템 사용 / 장비 장착 */
            if (eventData.clickCount > 1)
            {
                UseItem();
            }
        }

        /* 우클릭 : 아이템 사용 / 장비 장착 */
        else if (itemData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemData == null)
            return;

        SetColorA(0.3f);

        DragSlot.instance.Set(itemData, itemScript);
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
        if (DragSlot.instance.itemData == null)
        {
            Reset();
            return;
        }

        // Swap
        if (DragSlot.instance.itemData.item_name != itemData.item_name)
        {
            Set(DragSlot.instance.itemData, DragSlot.instance.itemScript);
            DragSlot.instance.Reset();
            DragSlot.instance.gameObject.SetActive(false);
            return;
        }

        //아이템 버리기
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            print("버리기");
            InventoryUI.instance.DeleteItem(itemData);
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
            Set(DragSlot.instance.itemData, DragSlot.instance.itemScript);
            DragSlot.instance.Reset();
        }
        else if (itemData != null && DragSlot.instance.itemData != itemData) // Swap
        {
            ItemData tempItemData = DragSlot.instance.itemData;
            Item tempItemScript = DragSlot.instance.itemScript;

            DragSlot.instance.Set(itemData, itemScript);
            Set(tempItemData, tempItemScript);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemData == null)
            return;

        DetailPanel.instance.Set(itemData);
        DetailPanel.instance.transform.position = eventData.position;
        DetailPanel.instance.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (itemData == null)
            return;

        DetailPanel.instance.Reset();
        DetailPanel.instance.gameObject.SetActive(false);
    }
}