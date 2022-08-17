using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{
    //public bool playAura = true;    // 파티클 제어 bool
    public ParticleSystem particleObject;   // 파티클 시스템



    void Awake()
    {
        particleObject = GameObject.Find("Energy explosion").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        if (particleObject != null)
            Debug.Log("Particle System successfully connected");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            particleObject.Play();
        else
            particleObject.Stop();
    }
}
