using System.Collections;
using UnityEngine;

public class Wizard : PlayerController
{
    private IEnumerator coroutine;
    private RaycastHit hit;
    private Transform magicCircle;

    private Vector3 originalPos = new Vector3(0f, 0.01f, 0f);
    private bool placed, terminated;

    // private float duration = 1f;
    private float scale = 1f;
    private float maxDistance = 10f;
    private float minDistance = 0f;

    private bool meteorIsAvailable = true;

    void Start()
    {
        magicCircle = GameObject.Find("/Player(Clone)").transform.Find("MagicCircle");
    }

    public override void UseSkill()
    {
        if(meteorIsAvailable)
        {
            // print("스킬 운석 떨어트리기 사용");

            meteorIsAvailable = false;

            coroutine = SetMagicCircleCoolTime(10f);
            StartCoroutine(coroutine);

            coroutine = Meteor();
            StartCoroutine(coroutine);
        }
    }


    /*****************************************
     * 스킬 수행을 위한 코루틴을 [마법진 생성] -> [운석 생성] 순서로 호출
     * 
     * @ param - X
     * @ return - X
     * @ exception - X
    ******************************************/
    IEnumerator Meteor()
    {
        coroutine = SetMagicCirclePosition();
        yield return StartCoroutine(coroutine);

        if (placed)
        {
            coroutine = SetActiveMagicCircleFalse();
            StartCoroutine(coroutine);

            CallMeteors();
        }

        print("끝끝끝");
    }


    /*****************************************
     * 마법진 생성
     * 마우스 휠로 거리를 조정하고
     * 설치가 완료된 경우 placed 변수를 true로 변경
     * 
     * @ param - X
     * @ return - X
     * @ exception - X
    ******************************************/
    IEnumerator SetMagicCirclePosition()
    {
        magicCircle.gameObject.SetActive(true);

        Vector3 pos;

        placed = false;
        terminated = false;

        while (!placed && !terminated)
        {
            if (Input.GetMouseButtonUp(0)) placed = true;
            else if (Input.GetMouseButtonUp(1)) terminated = true;

            pos = magicCircle.localPosition;
            pos.z += Input.mouseScrollDelta.y * scale;
            if (pos.z < minDistance) pos.z = minDistance;
            else if (pos.z > maxDistance) pos.z = maxDistance;
            magicCircle.localPosition = pos;

            yield return null;
        }
        if (placed) magicCircle.SetParent(GameObject.Find("Playable").transform);
        if (terminated) magicCircle.localPosition = originalPos;

        print("스킬 종료");
    }


    /*****************************************
     * 운석 생성
     * meteor 오브젝트를 활성화하고 지속 시간 동안 마법진을 유지함
     * 
     * @ param - X
     * @ return - X
     * @ exception - X
    ******************************************/
    void CallMeteors()
    {
        GameObject meteor = magicCircle.Find("Meteor").gameObject;
        meteor.transform.position = magicCircle.position + new Vector3(0, 15f, 0);
        meteor.GetComponent<MeshRenderer>().enabled = true;
        meteor.GetComponent<Rigidbody>().isKinematic = false;
        meteor.SetActive(true);
    }


    IEnumerator SetActiveMagicCircleFalse()
    {
        float curTime = 0f;
        while(curTime < 4f)
        {
            curTime += Time.deltaTime;
            yield return null;
        }
        
        
        magicCircle.gameObject.SetActive(false);
        magicCircle.SetParent(transform);
        magicCircle.localPosition = originalPos;
    }

    IEnumerator SetMagicCircleCoolTime(float _coolTime)
    {
        yield return new WaitForSeconds(_coolTime); // T.T
        meteorIsAvailable = true;
    }
}