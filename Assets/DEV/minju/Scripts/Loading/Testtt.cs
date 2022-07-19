using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Testtt : MonoBehaviour
{
    public void loadTown()
    {
        PlayerManager.instance.SetJob("Wizard");
        PlayerManager.instance.CreatePlayer();
        SceneManager.LoadScene("Prototype");
    }

}
