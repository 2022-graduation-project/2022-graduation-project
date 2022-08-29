using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCharacter : MonoBehaviour
{
    public void Clicked_Character()
    {
        GameObject.Find("Login Canvas").GetComponent<LoginManager>().SetCharInfo(gameObject);
    }
}
