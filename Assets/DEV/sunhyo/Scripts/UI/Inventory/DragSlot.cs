using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    public ItemData itemData;
    public ItemDummy itemDummy;

    public static DragSlot instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        gameObject.SetActive(false);
    }

    public void Set(ItemData _itemData, ItemDummy _itemScript = null)
    {
        itemDummy = _itemScript;

        itemData = _itemData; // InventorUI에서 넘어오니까 인스턴스 생성 없어도 가능할지도?
        icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
        SetColorA(1f);
    }

    public void Reset()
    {
        itemDummy = null;

        SetColorA(0f);
        icon.sprite = null;
        itemData = null;
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }
}