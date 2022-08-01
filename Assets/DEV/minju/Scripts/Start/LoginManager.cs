using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.SceneManagement;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;

public class LoginManager : MonoBehaviour
{
    // Message 에 2초간 메시지 띄우기
    public TMP_Text MsgText;
    private IEnumerator sendMessageOnProfile(string message, Color color)
    {
        MsgText.color = color;
        MsgText.text = message;
        yield return new WaitForSeconds(2.0f);
        MsgText.text = "";
    }

    // 서버 선택 버튼 클릭 시
    // 로그인 여부 먼저 확인
    private bool isLoggedIn = false;
    public GameObject serverWindow;
    public void Clicked_SelectServer()
    {
        if(!isLoggedIn)
        {
            StartCoroutine(sendMessageOnProfile("LOGIN FIRST", Color.red));
            return;
        }
        else
        {
            serverWindow.SetActive(true);
        }
    }

    // 로그인 창에서 로그인 버튼 클릭 시
    // 해당 Login Data Json File에서 찾기
    private string loggedInUsername = "";
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_Text errText;
    public void Clicked_Login()
    {
        // Empty check
        bool hasUS = false;
        bool hasPW = false;
        string inputUN = username.text;
        string inputPW = password.text;
        if(inputUN=="")
        {
            errText.text = "ENTER USERNAME";
            return;
        }
        else    hasUS = true;

        if(inputPW=="")
        {
            errText.text = "ENTER PASSWORD";
            return;
        }
        else    hasPW = true;

        // 정상적으로 아이디와 비밀번호가 입력됐을 때
        if(hasUS&&hasPW)
        {
            // 로그인 Json Data 찾기
            Dictionary<string, LoginData> loginDict;
            loginDict =
                DataManager.instance.
                LoadJsonFile<Dictionary<string, LoginData>>
                (Application.dataPath + "/MAIN/Data", "login");

            if(!loginDict.ContainsKey(inputUN))
            {
                errText.text = "INVALID USERNAME";
                return;
            }
            else
            {
                LoginData loginData = loginDict[inputUN];
                print(loginData.password+" / "+inputPW);
                if(inputPW != loginData.password)
                {
                    errText.text = "PASSWORD INCORRECT";
                    return;
                }
                else
                {
                    LoginSucceess(inputUN);
                }
            }
        }
        
    }
    
    [Header("Account / Login Window")]
    public GameObject accountWindow;
    public GameObject loginWindow;

    [Header("Login / Logout Button")]
    public GameObject loginBtn;
    public GameObject logoutBtn;

    // 로그인 성공 시
    // 아이디 저장 및 로그아웃 버튼 활성화
    private void LoginSucceess(string inputUN)
    {
        isLoggedIn = true;
        loggedInUsername = inputUN;
        username.text = "";
        password.text = "";
        errText.text = "";
    
        accountWindow.SetActive(true);
        loginWindow.SetActive(false);
        loginBtn.SetActive(false);
        logoutBtn.SetActive(true);

        print(loggedInUsername+" Login Success");
    }

}
