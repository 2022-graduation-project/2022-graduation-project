using System.Collections;
using UnityEngine;

public class SelfDesMonster : NormalMonster
{
    public override void SetMonsterData()
    {
        /*
        monsterData = DataManager.instance.LoadJsonFile
              <Dictionary<string, MonsterData>>
              (Application.dataPath + "/MAIN/Data", "monster")
              ["004_soul"];
        */
    }


    /* (SelfDes Monster) PRIVATE DATA - 공격 */
    private SelfDesWeapon weapon;  // 폭발 시 플레이어에게 데미지를 입힐 수 있는 범위
    private bool damagePlayer;    // 플레이어가 공격 범위 내에 들어와 있는지 여부


    public override void Set()
    {
        SetMonsterData();



        /* Protected Variables */
        attackDistance = 3f;
        attackCool = 2.0f;


        /* Damage Range 콜라이더 지정 */
        weapon = transform.Find("Explosion").gameObject.GetComponent<SelfDesWeapon>();

        if (weapon == null)
            Debug.Log("응 안 들어가;");

    }



    private void Update()
    {
        //Debug.Log(weapon.player_in);


        // 추적 범위 내에서 플레이어 발견!
        if (target != null)
        {
            isFound = true;
            distance = Vector3.Distance(transform.position, target.position);   // 현재 몬스터-플레이어 사이 거리 측정

            // 공격 범위보다 더 멀리 떨어져 있는 경우 -> 추적 계속
            if (distance >/*monsterData.attackDistance*/1.5f)
            {
                Chase(/*monsterData.moveSpeed*/1.5f);
            }

            // 공격 범위 진입 -> 추적 중지, 공격 시작
            else
            {
                StartCoroutine(coAttack());
            }
        }

        else
        {
            animator.SetBool("Walk", false);
            isFound = false;
        }
    }




    private IEnumerator coAttack()
    {
        yield return new WaitForSeconds(attackCool);

        if (weapon.player_in)
            print("Player damaged");

        /* 자폭 몬스터 폭발, 비활성화 */
        gameObject.SetActive(false);
    }
}
