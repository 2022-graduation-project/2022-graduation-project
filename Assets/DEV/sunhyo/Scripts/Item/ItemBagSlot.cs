using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemBagSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text item_name;
    [SerializeField] Text count;

    public void Set(ItemData _itemData)
    {
        // icon = null; // asset 에서 번호 기준으로 찾아오기
        item_name.text = _itemData.item_name;
        count.text = _itemData.count.ToString();
    }

    public void Reset()
    {
        icon = null;
        item_name.text = string.Empty;
        count.text = string.Empty;
    }
}