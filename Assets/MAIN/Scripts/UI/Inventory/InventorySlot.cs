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

    //public ItemData itemData = null;
    public Item itemScript = null;
    public string item_code;
    public int item_count;


    public void Set(string _item_code, Item _itemScript=null)
    {
        // itemData가 비어있거나 || 다른 아이템
        if (item_code == null || item_code != _item_code)
        {
            //if (itemScript == null && _itemScript == null)
            //{
            //    // print(_itemData.image_name.Substring(4));
            //    itemScript = gameObject.AddComponent(Type.GetType(_itemData.image_name.Substring(4))) as Item;
            //}

            item_code = _item_code;
            icon.sprite = DataManager.instance.LoadSpriteFile
                          (Application.dataPath + "/DEV/sunhyo/Assets/Items", item_code);

            SetColorA(1f);
        }

        SetCountObj();
    }

    public void Reset()
    {
        SetColorA(0f);
        icon.sprite = null;
        count_img.gameObject.SetActive(false);
        count_txt.text = string.Empty;

        itemScript = null;
        item_code = null;
        item_count = 0;
    }

    public void SetCountObj()
    {
        if (item_count > 1)
        {
            count_txt.text = item_count.ToString();
            count_img.gameObject.SetActive(true);
        }

        else if (item_count == 1)
            count_img.gameObject.SetActive(false);
        else // 아이템 소진
        {
            InventoryUI.instance.DeleteItem(item_code);
            Reset();
        }
    }

    void UseItem()
    {
        itemScript.Use();
        item_count--;
        count_txt.text = item_count.ToString("N0");

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

        /* clickCount는 일부 기기에서 씹히는 경우도 있다고 함. 시간 계산으로 변경하는 게 좋을 듯 */
        /* 좌클릭 및 드래그 : 아이템 이동 */
        if (item_code != null && eventData.button == PointerEventData.InputButton.Left)
        {
            /* 좌클릭 * 2 : 아이템 사용 / 장비 장착 */
            if (eventData.clickCount > 1)
            {
                UseItem();
            }
        }

        /* 우클릭 : 아이템 사용 / 장비 장착 */
        else if (item_code != null && eventData.button == PointerEventData.InputButton.Right)
        {
            UseItem();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item_code == null)
            return;

        SetColorA(0.3f);

        DragSlot.instance.Set(item_code, itemScript);
        DragSlot.instance.gameObject.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item_code == null || DragSlot.instance.item_code == null)
            return;

        DragSlot.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item_code == null)
            return;

        // 아이템 이동 완료
        if (DragSlot.instance.item_code == null)
        {
            Reset();
            return;
        }

        // Swap
        if (DragSlot.instance.item_code != item_code)
        {
            Set(DragSlot.instance.item_code, DragSlot.instance.itemScript);
            DragSlot.instance.Reset();
            DragSlot.instance.gameObject.SetActive(false);
            return;
        }

        //아이템 버리기
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            print("버리기");
            InventoryUI.instance.DeleteItem(item_code);
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
        if (DragSlot.instance.item_code == null)
            return;

        if (item_code == null) // 빈 슬롯인 경우
        {
            Set(DragSlot.instance.item_code, DragSlot.instance.itemScript);
            DragSlot.instance.Reset();
        }
        else if (item_code != null && DragSlot.instance.item_code != item_code) // Swap
        {
            Item tempItemScript = DragSlot.instance.itemScript;

            DragSlot.instance.Set(item_code, itemScript);
            Set(item_code, tempItemScript);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        return;


        //if (item_code == null)
        //    return;

        //DetailPanel.instance.Set(item_code);
        //DetailPanel.instance.transform.position = eventData.position;
        //DetailPanel.instance.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        return;


        //if (item_code == null)
        //    return;

        //DetailPanel.instance.Reset();
        //DetailPanel.instance.gameObject.SetActive(false);
    }
}