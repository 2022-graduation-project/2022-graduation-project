using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;


public class Dragable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
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
        if(DragSlot.instance.item_code != "")
        {
            InventoryWindow.instance.SwapItem(DragSlot.instance.slot, slot);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.ResetSlot();
        if(slot.item_code != "")
        {
            slot.SetColorA(1f);
        }

        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            InventoryWindow.instance.RemoveItem(slot.item_code);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(lastClick <= 0)
        {
            lastClick = eventData.clickTime;
        }
        else
        {
            if (eventData.clickTime - lastClick < doubleClick)
            {
                InventoryWindow.instance.UseItem(slot.item_code);
            }

            lastClick = 0;
        }
    }
}