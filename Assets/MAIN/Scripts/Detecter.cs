using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detecter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player")
        {
            StartCoroutine(count());
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    IEnumerator count()
    {
        float curTime = 0f;
        float spawnTime = 3f;

        while(curTime < spawnTime)
        {
            curTime += Time.deltaTime;
            yield return null;
        }

        // 씬 넘기기
        SceneLoader.instance.LoadScene("Field");
    }
}
