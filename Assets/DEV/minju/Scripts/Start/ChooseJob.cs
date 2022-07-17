using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChooseJob : MonoBehaviour
{
    
    private int select=0;
    public LoginManage logmanage;

    public void OnDropdownEvent(int index)
    {
        select=index;
        print("DDselect: "+select);
    }

    public void SelectConfirm()
    {
        string job = "";
        switch (select)
        {
            case 0:
                job = "Archer";
                break;
            case 1:
                job = "Warrior";
                break;
            case 2:
                job = "Wizard";
                break;
        } 
        logmanage.jobname = job;
    }
}
