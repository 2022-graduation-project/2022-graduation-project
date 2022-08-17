using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIMove : MonoBehaviour, IDragHandler
{
    [SerializeField] public RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = rectTransform.position;
        pos.y += eventData.delta.y;
        pos.x += eventData.delta.x;
        rectTransform.position = pos;
    }
}