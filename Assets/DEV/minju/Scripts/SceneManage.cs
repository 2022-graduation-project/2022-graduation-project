using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���� �� ��������
        // Get current scene
        scene = SceneManager.GetActiveScene();

        // trigger with player
        if (other.tag=="Player")
        {
            // if current scene is Monster, Load next scene
            if(scene.name == "Monster")
            {
                SceneManager.LoadScene("BossMonster");
            }
        }
    }
}
