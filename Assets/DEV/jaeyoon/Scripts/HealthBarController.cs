using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    Slider HPbar;
    Button Attack;  // ~
    float hp = 100;
    float maxHealth = 100;
    float minHealth = 0;

    void Start()
    {
        HPbar = GetComponent<Slider>();
        Attack = GetComponent<Button>();    // ~
        hp = maxHealth;
        HPbar.value = hp / maxHealth;

        Attack.onClick.AddListener(() => onDamage());   // ~
    }

    void Update()
    {
        if (HPbar.value <= minHealth)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }

    public void onDamage()  // ~
    {
        hp -= 1;
        HPbar.value = hp / maxHealth;
    }
}