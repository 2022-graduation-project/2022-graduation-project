using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject shop;

    /* Singleton */
    public static UIManager instance;
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

        inventory.SetActive(false);
        shop.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeInHierarchy == true)
            {
                PlayerManager.instance.mouseMoveable = true;
                inventory.SetActive(false);
            }
            else
            {
                PlayerManager.instance.mouseMoveable = false;
                inventory.SetActive(true);
            }
                
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (shop.activeInHierarchy == true)
            {
                PlayerManager.instance.mouseMoveable = true;
                shop.SetActive(false);
            }
                
            else
            {
                PlayerManager.instance.mouseMoveable = false;
                shop.SetActive(true);
            }
        }
    }
}