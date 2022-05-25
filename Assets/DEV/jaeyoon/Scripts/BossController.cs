using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public Animator animator;
    public Slider HpBar;
    public Button Attack;
    float maxHealth = 100;
    float minHealth = 0;
    public float hp;
    public float damage;

    void Start()
    {
        animator.SetBool("Dead", false);

        hp = maxHealth;
        HpBar.value = (hp / maxHealth);

        if (Attack != null)
        {
            Attack.onClick.AddListener(onDamage);
        }
        else
        {
            Debug.Log("[NullReferenceException] Button is not allocated.");
        }
    }

    void Update()
    {
        HpBar.value = (hp / maxHealth);

        if (HpBar.value <= minHealth)
            transform.Find("Fill Area").gameObject.SetActive(false);
        else
            transform.Find("Fill Area").gameObject.SetActive(true);
    }

    void onDamage()
    {
        if (hp > 0) // 한 번 더 조건문으로 묶어준 이유 : 한 번 죽고 나면 메시지 출력 그만
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