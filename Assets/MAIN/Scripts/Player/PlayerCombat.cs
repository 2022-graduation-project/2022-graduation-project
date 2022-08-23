using System.Collections;
using UnityEngine;


public class PlayerCombat : MonoBehaviour, ICombat
{
    public PlayerController controller;
    public IRole role;

    public Transform weapon_tr;
    public Weapon weapon;

    public PlayerData playerData; // readonly가 가능할지

    public Animator animator;

    /* runtime data */
    private bool shield = false;
    private bool canUseSkill = true;
    private bool jumpable = true;

    private enum skills { A, B, C };

    void Start()
    {
        controller = GetComponent<PlayerController>();
        role = GetComponent<IRole>();
        playerData = DataManager.instance.playerData;
        weapon = weapon_tr.GetChild(0).GetComponent<Weapon>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Attack(playerData.STR);
        }

        if (Input.GetKeyDown(KeyCode.G)) ConsumeMP(role.UseSkill((int)skills.A));
        else if (Input.GetKeyDown(KeyCode.H)) ConsumeMP(role.UseSkill((int)skills.B));
        else if (Input.GetKeyDown(KeyCode.T)) ConsumeMP(role.UseSkill((int)skills.C));
    }

    public void Attack(float _damage)
    {
        // 일반 공격
        weapon.Attack(_damage, controller);
        animator.SetTrigger("Attack");
    }

    public void Damaged(float _damage, string _type = null, float _amount = 0, float _time = 0)
    {
        if (shield) return;

        PlayerManager.instance.UpdateHp(_damage * -1);
        
        if(_type != null)
        {
            AddEffect(_type, _amount, _time);
        }

        if(playerData.curHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        gameObject.transform.position = Vector3.zero;
        controller.SetAnimation("Dead");
        // 리스폰 장소로 소환
    }

    public void ConsumeMP(float _abs)
    {
        PlayerManager.instance.UpdateMp(_abs * -1);
    }


    public void AddEffect(string _type, float _amount, float _time)
    {
        switch (_type)
        {
            case "Heal":
                StartCoroutine(Healing(_time, _amount));
                break;
            case "RefillMp":
                StartCoroutine(RefillMana(_time, _amount));
                break;
            case "Shield":
                StartCoroutine(Shielding(_time));
                break;
        }
    }


    /************************************************/
    /*                   상태 이상                   */
    /************************************************/
    public void StartHeal(float _time, float _delta)
    {
        StartCoroutine(Healing(_time, _delta));
    }

    public void StartRefillMana(float _time, float _delta)
    {
        StartCoroutine(RefillMana(_time, _delta));
    }

    public IEnumerator Healing(float _time, float _heal_amount)
    {
        float curTime = 0;

        while (curTime < _time)
        {
            curTime += 1f;

            playerData.curHp += _heal_amount;
            yield return GameManager.instance.GetWaitForSeconds(1f);
        }
    }

    public IEnumerator RefillMana(float _time, float _add_amount)
    {
        float curTime = 0;

        while (curTime < _time)
        {
            curTime += 1f;

            PlayerManager.instance.UpdateMp(_add_amount);
            yield return GameManager.instance.GetWaitForSeconds(1f);
        }

    }

    public IEnumerator Shielding(float shieldDuration)
    {
        float duration = 0;
        while (duration < shieldDuration)
        {
            shield = true;
            yield return GameManager.instance.GetWaitForSeconds(1f);

            duration++;
            if (duration >= shieldDuration)
            {
                shield = false;
                yield break;
            }
        }
    }
}