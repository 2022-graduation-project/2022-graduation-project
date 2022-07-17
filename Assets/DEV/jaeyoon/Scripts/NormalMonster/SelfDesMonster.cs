using System.Collections;
using UnityEngine;

public class SelfDesMonster : NormalMonster
{
    private SelfDesWeapon controller;
    private bool v_damagePlayer;


    protected override void Awake()
    {
        base.Awake();
        attackRange = 2.5f;
        attackDelay = 1.5f;
    }

    private void Start()
    {
        controller = GameObject.Find("DamageRange").GetComponent<SelfDesWeapon>();
    }

    protected override void Update()
    {
        base.Update();
        v_damagePlayer = controller.damagePlayer;
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);

        if (v_damagePlayer)
            print("Player damaged");

        gameObject.SetActive(false);
    }
}
