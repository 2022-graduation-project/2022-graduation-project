using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChestBagSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Text item_name;
    [SerializeField] Text count;

    public ChestData chestData = null;


    private ChestBag chestBag;

    public void Set(ChestBag _chestBag, ChestData _chestData)
    {
        chestBag = _chestBag;

        chestData = _chestData.DeepCopy();
        icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Chest", _chestData.image_name);
        item_name.text = _chestData.item_name;
        count.text = _chestData.count.ToString();
        SetColorA(1f);

        gameObject.SetActive(true);
    }

    public void Reset()
    {
        SetColorA(0f);
        icon.sprite = null;
        item_name.text = string.Empty;
        count.text = string.Empty;
        chestData = null;

        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        if(chestData != null)
        {
            chestBag.PopChest(chestData);
            UIManager.instance.inventoryUI.AddItem2(chestData);
            Reset();
        }
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }
}