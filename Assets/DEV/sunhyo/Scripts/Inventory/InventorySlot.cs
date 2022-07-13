using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] Image count_img;
    [SerializeField] Text count_txt;
    GameObject iconObject;

    public ItemData itemData = null;

    public void Set(ItemData _itemData)
    {
        iconObject = transform.GetChild(0).gameObject;

        if (itemData != null)
        {
            itemData.count += _itemData.count;
            count_txt.text = itemData.count.ToString();
            if(itemData.count > 1)
                count_img.gameObject.SetActive(true);
            else
                count_img.gameObject.SetActive(false);
        }
            
        else
        {
            itemData = _itemData.DeepCopy();
            icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
            icon.sprite.name = _itemData.image_name;

            switch (itemData.image_name)
            {
            case "000_hpPotion":
                iconObject.AddComponent<HpPotion>();
                break;
            case "001_mpPotion":
                iconObject.AddComponent<MpPotion>();
                break;
            case "002_apple":
                iconObject.AddComponent<HpPotion>();
                break;
            case "003_meat":
                iconObject.AddComponent<HpPotion>();
                break;
            case "004_gosu":
                iconObject.AddComponent<MpPotion>();
                break;
            case "005_sheildPotion":
                iconObject.AddComponent<ShieldPotion>();
                break;
            case "006_bomb":
                iconObject.AddComponent<Bomb>();
                break;
            }

            count_txt.text = _itemData.count.ToString();
            if (itemData.count > 1)
                count_img.gameObject.SetActive(true);
            else
                count_img.gameObject.SetActive(false);

            SetColorA(1f);
        }

        if (itemData.count < 1)
            Reset();
    }

    public void Reset()
    {
        switch (itemData.image_name)
        {
            case "000_hpPotion":
                Destroy(iconObject.GetComponent<HpPotion>());
                break;
            case "001_mpPotion":
                Destroy(iconObject.GetComponent<MpPotion>());
                break;
            case "002_apple":
                Destroy(iconObject.GetComponent<HpPotion>());
                break;
            case "003_meat":
                Destroy(iconObject.GetComponent<HpPotion>());
                break;
            case "004_gosu":
                Destroy(iconObject.GetComponent<MpPotion>());
                break;
            case "005_sheildPotion":
                Destroy(iconObject.GetComponent<ShieldPotion>());
                break;
            case "006_bomb":
                Destroy(iconObject.GetComponent<Bomb>());
                break;
        }

        SetColorA(0f);
        icon.sprite = null;
        count_txt.text = string.Empty;
        count_img.gameObject.SetActive(false);
        itemData = null;
    }

    public void OnPointerClick(PointerEventData _eventData)
    {
        if(itemData != null)
        {
            print(itemData.image_name);
            UseItem();
            //PlayerManager.instance.UseItem(itemData);
        }
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }

    void UseItem()
    {
        switch (itemData.image_name)
        {
            case "000_hpPotion":
                iconObject.GetComponent<HpPotion>().Use();
                break;
            case "001_mpPotion":
                iconObject.GetComponent<MpPotion>().Use();
                break;
            case "002_apple":
                iconObject.GetComponent<HpPotion>().Use();
                break;
            case "003_meat":
                iconObject.GetComponent<HpPotion>().Use();
                break;
            case "004_gosu":
                iconObject.GetComponent<MpPotion>().Use();
                break;
            case "005_sheildPotion":
                iconObject.GetComponent<ShieldPotion>().Use();
                break;
            case "006_bomb":
                iconObject.GetComponent<Bomb>().Use();
                break;
        }
        itemData.count--;
        Set();
    }

    void Set()
    {
        count_txt.text = itemData.count.ToString();
        if (itemData.count > 1)
            count_img.gameObject.SetActive(true);
        else if(itemData.count == 1)
            count_img.gameObject.SetActive(false);
        else
            Reset();
    }
}