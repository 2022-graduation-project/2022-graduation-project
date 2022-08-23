using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    Scene scene;

    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        // 현재 씬 가져오기
        // Get current scene
        scene = SceneManager.GetActiveScene();

        // trigger with player
        if (other.tag=="Player")
        {
            switch(scene.name)
            {
                // if current scene is Monster, Load Boss scene
                case "Monster":
                    SceneManager.LoadScene("BossMonster");
                    break;
                case "Prototype":
                    player = GameObject.FindWithTag("Player");
                    SceneManager.LoadScene("Monster");
                    player.transform.position = new Vector3(22.5f, 25.49f, -1.48f);
                    break;
            }
        }
    }
}
