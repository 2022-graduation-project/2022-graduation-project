using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD

public class BossMonster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
=======
using UnityEngine.UI;

public class BossMonsterUI : MonoBehaviour
{
    public Animator animator;
    public Slider HpBar;
    public Button DamageBtn;
    public Button AttackBtn;
    public Text HpText;
    float maxHealth = 100;
    float minHealth = 0;
    public float hp;
    public float damage;

    void Start()
    {
        animator.SetBool("Dead", false);

        hp = maxHealth;
        HpBar.value = (hp / maxHealth);

        if (DamageBtn != null && AttackBtn != null)
        {
            DamageBtn.onClick.AddListener(onDamage);
            AttackBtn.onClick.AddListener(Attack);
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

    void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
>>>>>>> d9f30a8f0abf183421d87495d8623f54a43fff60
