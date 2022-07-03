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

    void Start()
    {
        magicCircle = GameObject.Find("/Player").transform.Find("MagicCircle");
    }

    public override void UseSkill()
    {
        print("스킬 사용");

        coroutine = Meteor();
        StartCoroutine(coroutine);
    }

    IEnumerator Meteor()
    {
        magicCircle.gameObject.SetActive(true);

        coroutine = SetMagicCirclePosition();
        yield return StartCoroutine(coroutine);

        if (placed)
        {
            coroutine = CallMeteors(1f);
            yield return StartCoroutine(coroutine);
        }

        print("끝끝끝");
        magicCircle.gameObject.SetActive(false);
    }

    IEnumerator SetMagicCirclePosition()
    {
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

        if(terminated) magicCircle.localPosition = originalPos;

        print("스킬 종료");
    }

    IEnumerator CallMeteors(float duration)
    {
        float curTime = 0f;

        GameObject meteor = magicCircle.Find("Meteor").gameObject;
        meteor.SetActive(true);

        while(curTime <= duration)
        {
            curTime += Time.deltaTime;
            yield return null;
        }

        magicCircle.localPosition = originalPos;
    }
}