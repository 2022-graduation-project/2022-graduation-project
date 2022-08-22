using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragSlot : MonoBehaviour
{
    [SerializeField] private Image icon;

    //public ItemData itemData;
    public string item_code;
    public int item_count;
    public Item itemScript;

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

    public void Set(string _item_code, Item _itemScript = null)
    {
        itemScript = _itemScript;

        item_code = _item_code;
        icon.sprite = DataManager.instance.LoadSpriteFile
                      (Application.dataPath + "/DEV/sunhyo/Assets/Items", _item_code);
        SetColorA(1f);
    }

    public void Reset()
    {
        itemScript = null;

        SetColorA(0f);
        icon.sprite = null;
        item_code = null;
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }
}