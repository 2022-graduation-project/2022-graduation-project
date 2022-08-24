using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    // ���� ��ư�� ������ ��
    public void ClickedSetting()
    {
        // Stop the Game
        Time.timeScale = 0;
    }

    // ���� â���� X��ư�� ������ ��
    // ���� â���� ��� ��ư�� ������ ��
    public void RestartGame()
    {
        // Restart the Game
        Time.timeScale = 1;
    }

    // ���� â���� �ǵ��ư��� ��ư�� ������ ��
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
