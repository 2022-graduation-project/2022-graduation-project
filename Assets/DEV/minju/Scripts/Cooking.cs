using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cooking : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }

    public Image firstItem;
    public Image secondItem;
    private bool all;

    public void ItemOn()
    {
        if(firstItem.sprite != null && secondItem.sprite != null)
        {
            all = true;
        }
        else
        {
            all = false;
            print("There's no image");
        }

        if (all == false)
        {
            return;
        }

        else
        {
            switch (firstItem.sprite.name)
            {
                case "":
                    break;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ItemOn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
