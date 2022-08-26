using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDesMonster : NormalMonster
{
    /*----------------------------------------------------------------
     *              [SelfDes Monster] LOCAL DATA
     * --------------------------------------------------------------*/

    public SelfDesWeapon weapon;  // 폭발 시 플레이어 데미지 여부를 판단할 콜라이더 오브젝트
    public bool damagePlayer;    // 현재 플레이어가 공격 범위(collider) 내에 들어와 있는가?


    /*----------------------------------------------------------------
     *              Inherited Methods (SetMonsterData, Set)
     * --------------------------------------------------------------*/

    public override void SetMonsterData()
    {
        monsterData = DataManager.instance.LoadJsonFile
              <Dictionary<string, MonsterData>>
              (Application.dataPath + "/MAIN/Data", "monster")
              ["004_soul"];
        monsterData.curHp = monsterData.maxHp;
    }

    public override void Set()
    {
        base.Set();

        SetMonsterData();

        /* Protected Variables */
        attackDistance = 2.0f;
        attackCool = 2.0f;

        /* Damage Range 콜라이더 지정 */
        weapon = transform.Find("Explosion").gameObject.GetComponent<SelfDesWeapon>();
    }
    


    /*----------------------------------------------------------------
     *              [SelfDes Monster] 몬스터 별 공격 코루틴
     * --------------------------------------------------------------*/

    public override IEnumerator coAttack()
    {
        yield return new WaitForSeconds(attackCool);

        if (weapon.player_in)
            print("Player damaged");

        // 자폭 몬스터 폭발, 비활성화
        gameObject.SetActive(false);
    }


    /*----------------------------------------------------------------
     *              [SelfDes Monster] 추격 메소드
     * --------------------------------------------------------------*/

    public override void StartChasing()
    {
        Chase(monsterData.moveSpeed);
    }
    public override void StopChasing()
    {
        Debug.Log("몬스터 멈춤. " + attackCool + "초 뒤에 몬스터 폭발합니다.");
    }
}
