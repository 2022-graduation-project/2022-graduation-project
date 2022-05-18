using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text count;

    public ItemData itemData;

    public void Set()
    {
    }

    public void Reset()
    {
        icon = null;
        count.text = string.Empty;
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        print("클릭");
    }
}