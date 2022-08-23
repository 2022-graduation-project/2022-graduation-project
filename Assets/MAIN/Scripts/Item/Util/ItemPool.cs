using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPool : MonoBehaviour
{
    public Dictionary<string, IItem> pool = new Dictionary<string, IItem>();

    public static ItemPool instance;
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

        SetItemPool();
    }

    public void SetItemPool()
    {
        foreach (Transform child in transform)
        {
            pool.Add(child.name, child.GetComponent<IItem>());
        }
    }
}