using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonsterUI : MonoBehaviour
{
    public Animator animator;
    //int[] userKeyCodes = new int[] { 37, 38 };   // 37:LeftArrow, 38:RightArrow
    //string[] userParameters = new string[] { "RunLeft", "RunRight" };

    public Slider HpBar;
    public Button DamageBtn;

    public Text HpText;
    float maxHealth = 100;
    float minHealth = 0;
    public float hp;
    public float damage;





    void Start()
    {
        hp = maxHealth;
        HpBar.value = (hp / maxHealth);

        if (DamageBtn != null)
        {
            DamageBtn.onClick.AddListener(onDamage);
        }
    }

    void Update()
    {
        if (hp <= 10)
        {
            HpText.color = Color.red;
        }
        HpText.text = hp.ToString();
        HpBar.value = (hp / maxHealth);

        if (HpBar.value <= minHealth)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }

    //void Run(int userKey, string Parameter) { }

    void onDamage()
    {
        if (hp > 0)
        {
            hp -= damage;

            if (hp > 0)
            {
                Debug.Log("HP : " + hp);
                animator.SetTrigger("Damaged");
            }
            else
            {
                hp = 0;
                Debug.Log("Boss Monster died!");
                animator.SetBool("Dead", true);
            }
        }
    }
}