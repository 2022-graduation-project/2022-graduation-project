using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class QuickSlotDragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    private InventorySlot slot;

    private float lastClick;
    private float doubleClick = 0.5f;

    void Start()
    {
        slot = GetComponent<InventorySlot>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (slot.item_code != "")
        {
            slot.SetColorA(0.5f);
            DragSlot.instance.SetSlot(slot);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        DragSlot.instance.transform.position = eventData.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        string code = DragSlot.instance.item_code;
        int count = DragSlot.instance.item_count;

        if (code != "" && InventoryWindow.instance.FindItemSlot(code).item_script != null)
        {
            IItem item_script = InventoryWindow.instance.FindItemSlot(code).item_script;
            if(item_script != null && item_script is Consumable)
            {
                if(!PlayerManager.instance.playerUI.FindItemQuickSlot(code))
                {
                    slot.SetSlot(code, count);
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.ResetSlot();
        if (slot.item_code != "")
        {
            slot.SetColorA(1f);
        }

        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            slot.ResetSlot();
        }
    }

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
                UseItem();
            }

            lastClick = 0;
        }
    }

    public void UseItem()
    {
        InventoryWindow.instance.UseItem(slot.item_code);

        InventorySlot item = InventoryWindow.instance.FindItemSlot(slot.item_code);
        if(item == null)
        {
            slot.ResetSlot();
        }
        else
        {
            slot.SetSlot(item.item_code, item.item_count);
        }
    }
}