using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private Text item_name;
    [SerializeField] private Text description;

    public static DetailPanel instance = null;
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

    public void Set(ItemData _itemData)
    {
        print($"{_itemData.item_name}");
        icon.sprite = DataManager.instance.LoadSpriteFile(Application.dataPath + "/DEV/sunhyo/Assets/Items", _itemData.image_name);
        item_name.text = _itemData.item_name;
        description.text = _itemData.description;
        SetColorA(1f);
    }
    
    public void Reset()
    {
        SetColorA(0f);
        icon.sprite = null;
        item_name.text = null;
        description.text = null;
    }

    void SetColorA(float _delta)
    {
        Color color = icon.color;
        color.a = _delta;
        icon.color = color;
    }
}