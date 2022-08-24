using System.Collections;
using UnityEngine;


public class InventoryUtil : MonoBehaviour
{
    public InventoryWindow inventory;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if (!inventory.isActive)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            }
        }
    }

    public void OpenInventory()
    {
        inventory.OpenWindow();
    }

    public void CloseInventory()
    {
        inventory.CloseWindow();
    }
}