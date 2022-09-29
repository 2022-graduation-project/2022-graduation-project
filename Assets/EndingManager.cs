using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public Text game_clear;
    public Text credit;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float delayTime = 1f;
        float curTime = 0f;
        Color color;

        while (curTime <= delayTime)
        {
            curTime += Time.deltaTime;
            yield return null;
        }

        delayTime = 2f;
        curTime = 0f;
        while (curTime <= delayTime)
        {
            curTime += Time.deltaTime;
            color = game_clear.color;
            color.a += Time.deltaTime / delayTime;
            game_clear.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        delayTime = 2f;
        curTime = 0f;
        while (curTime <= delayTime)
        {
            curTime += Time.deltaTime;
            color = game_clear.color;
            color.a -= Time.deltaTime / delayTime;
            game_clear.color = color;
            yield return null;
        }
    }
}
