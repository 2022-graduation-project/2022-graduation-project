using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    private IEnumerator coroutine;
    private WaitForSeconds waitforseconds = new WaitForSeconds(1f);
    private float delay = 3f;

    private Vector3 originalPos = new Vector3(0f, 0f, -15f);

    // private List<MonsterDummy> monsters = new List<MonsterDummy>();
    private MonsterDummy monster;

    void Update()
    {
        if (transform.localPosition.z >= 0)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<MeshRenderer>().enabled = false;
            transform.localPosition = originalPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        monster = other.GetComponent<MonsterDummy>();

        if(monster)
        {
            monster.Damaged(50f); // 일시적 데미지
            coroutine = Attack(monster);
            StartCoroutine(coroutine); // 지속 데미지
        }
    }

    IEnumerator Attack(MonsterDummy _monster)
    {
        float curTime = 0f;
        while(curTime < delay)
        {
            _monster.Damaged(5f);
            curTime += 1f;
            yield return waitforseconds;
        }

        gameObject.SetActive(false);
    }
}