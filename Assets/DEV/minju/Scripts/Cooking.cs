using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooking : MonoBehaviour
{
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
