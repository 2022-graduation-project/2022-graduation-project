using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : MonoBehaviour
{
    public PlayerManager playerManager;
    public void StartTown()
    {
        playerManager = PlayerManager.instance;
        TMP_Text jobname = transform.GetChild(0).GetComponent<TMP_Text>();
        playerManager.SetJob(jobname.text);
        SceneManager.LoadScene("Loading");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
