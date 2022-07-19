using System.Collections;
using UnityEngine;

public class SelfDesMonster : NormalMonster
{
    /* (SelfDes Monster) PRIVATE DATA - 공격 */
    private SelfDesWeapon damageRange;  // 폭발 시 플레이어에게 데미지를 입힐 수 있는 범위
    private bool damagePlayer;    // 플레이어가 공격 범위 내에 들어와 있는지 여부


    protected override void Awake()
    {
        base.Awake();

        /* 공격 범위 & 공격 지연 시간 */
        attackRange = 3.0f;
        attackDelay = 2.0f;

        /* Damage Range 콜라이더 지정 */
        damageRange = GameObject.Find("DamageRange").GetComponent<SelfDesWeapon>();
    }


    protected override void Update()
    {
        base.Update();
        damagePlayer = damageRange.player_in;
    }


    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);

        if (damagePlayer)
            print("Player damaged");

        /* 자폭 몬스터 폭, 비활성화 */
        gameObject.SetActive(false);
    }
}
