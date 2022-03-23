using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PurchaseUI : MonoBehaviour
{
    public void ClickItem()
    {
        print("아이템 클릭");

        // GameObject clickObject = EventSystem.current.currentSelectedGameObject;

        // print(clickObject.name + ", " + clickObject.GetComponentInChildren<Text>().text);
    }
}
