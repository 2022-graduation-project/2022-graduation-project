using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    // 설정 버튼을 눌렀을 때
    public void ClickedSetting()
    {
        // Stop the Game
        Time.timeScale = 0;
    }

    // 설정 창에서 X버튼을 눌렀을 때
    // 설정 창에서 재생 버튼을 눌렀을 때
    public void RestartGame()
    {
        // Restart the Game
        Time.timeScale = 1;
    }

    // 설정 창에서 되돌아가기 버튼을 눌렀을 때
    public void GotoStart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
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
