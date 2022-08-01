using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(TryThis(10f));
    }

    private IEnumerator TryThis(float time)
    {
        print(time);
        yield return new WaitForSeconds(time);
        print("The Time has taken");
        Spawn();
        yield return null;
    }

    private void Spawn()
    {
        print("SPAWN");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
