using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    Dictionary<string, LoginData> loginDict;
    LoginData loginData;


    /**************************
        로그인 데이터 로드
    ***************************/
    // WHEN: Login, Sign Up
    // DO: Load previous Login Json File
    private void LoadLoginJson()
    {
        loginDict =
                DataManager.instance.
                LoadJsonFile<Dictionary<string, LoginData>>
                (Application.dataPath + "/MAIN/Data", "login");
    }

    /**************************
        로그인 데이터 업데이트
    ***************************/
    // WHEN: Sign Up, Create new character
    // DO: Update Login Json File
    private void UpdateLoginJson()
    {
        if(DataManager.instance.ObjectToJson<Dictionary<string, LoginData>>(Application.dataPath + "/MAIN/Data", "login", loginDict))
        {
            print("Login data has updated.");
        }
    }

    /**************************
            메시지 띄우기
    ***************************/
    // 해당 Message 에 5초간 메시지 띄우기
    public TMP_Text MsgText;
    private IEnumerator sendMessageDisplay(TMP_Text msg, string message, Color color)
    {
        msg.color = color;
        msg.text = message;
        yield return new WaitForSeconds(5.0f);
        msg.text = "";
    }

    // Terminate Window
    // 창 앞으로 띄우기
    Transform tempParent;
    public void MoveToFront(GameObject currentObj)
    {
        tempParent = currentObj.transform;
        tempParent.SetAsLastSibling();
    }

    /**************************
            서버 선택창
    ***************************/
    // 서버 선택 버튼 클릭 시
    // 로그인 여부 먼저 확인
    private bool isLoggedIn = false;
    public GameObject serverWindow;
    public void Clicked_SelectServer()
    {
        if(!isLoggedIn)
        {
            StartCoroutine(sendMessageDisplay(MsgText, "LOGIN FIRST", Color.red));
            return;
        }
        else
        {
            serverWindow.SetActive(true);
        }
    }

    /**************************
            캐릭터 선택창
    ***************************/
    // 서버 버튼 클릭 시
    // 계정의 캐릭터 정보 로드하여, 캐릭터 선택창 띄우기
    public void Clicked_Arcus()
    {
        GetCharacters();
    }

    private int lastCharacter = 0;
    Dictionary<string, Characters> characters;

    [Header("Character UI")]
    public UnityEngine.Object charPrefab;
    public Transform characterUI;
    private void GetCharacters()
    {
        characters = loginData.characters;

        // 보유 캐릭터 없을 때
        if (loginData.counts == 0)
        {
            print("No Characters");
        }

        else
        {
            TMP_Text[] char_Info;
            for (int i = lastCharacter; i < loginData.counts; i++)
            {
                // 반복문 -> 프리펩을 스크롤뷰 안(characterUI)에 캐릭터창 생성
                GameObject characterWin = Instantiate(charPrefab, characterUI) as GameObject;
                // 개별 캐릭터 정보 setting (0: name, 1: job, 2: level)
                char_Info = characterWin.GetComponentsInChildren<TMP_Text>();
                char_Info[0].text = characters[i.ToString()].charname;
                char_Info[1].text = characters[i.ToString()].job;
                char_Info[2].text = "lv."
                                    + characters[i.ToString()].level.ToString();
            }
            lastCharacter = loginData.counts;
        }
    }

    /**************************
            캐릭터 선택
    ***************************/
    // 해당 캐릭터 버튼 선택 시
    // 해당 캐릭터 정보 로드
    [Header("Character Select")]
    public TMP_Text info_name;
    public TMP_Text info_job;
    public TMP_Text info_level;
    public void SetCharInfo(GameObject btn)
    {
        TMP_Text [] btn_info = btn.GetComponentsInChildren<TMP_Text>();
        TMP_Text name = btn_info[0];
        TMP_Text job = btn_info[1];
        TMP_Text level = btn_info[2];

        info_name.text = name.text;
        info_job.text = job.text;
        info_level.text = level.text;

        isSelected=true;
    }

    /**************************
            플레이 시작
    ***************************/
    // 플레이 버튼 선택 시
    // 해당 캐릭터로 다음 씬 로드
    public TMP_Text charErrMsg;
    private bool isSelected = false;
    public void Clicked_Play()
    {
        if(!isSelected)
        {
            StartCoroutine(sendMessageDisplay(charErrMsg, "SELECT CHARACTER FIRST", Color.red));
            return;
        }
        PlayerManager.instance.SetJob(info_job.text);
        SceneManager.LoadScene("2. Loading");
    }

    /**************************
            캐릭터 생성창
    ***************************/
    // NewCharacter Confirm -> OnClick()
    // 새 캐릭터 정보 저장 후, 로드
    [Header("New Character UI")]
    public GameObject newCharWindow;
    public GameObject charWindow;
    public TMP_InputField newName;
    public TMP_Text new_ErrTxt;
    private string newJob = "";
    public void Clicked_NewConfirm()
    {
        // 기본 캐릭터 보유 수를 초과하려 할 때, 캐릭터 생성 불가
        if(loginData.counts >= 5)
        {
            new_ErrTxt.text = "EXCEED DEFAULT NUMBER OF CHARACTERS\n"+
            "YOU SHOULD PAY FOR MORE CHARACTERS";
            return;
        }

        // 새 이름 입력하지 않았을 때, 캐릭터 생성 불가
        if(newName.text == "")
        {
            new_ErrTxt.text = "ENTER NAME FIRST";
            return;
        }

        // 직업을 선택하지 않았을 때, 캐릭터 생성 불가
        if(newJob == "")
        {
            new_ErrTxt.text = "SELECT JOB FIRST";
            return;
        }

        // 새 캐릭터 정보
        Characters newChracter = new Characters();
        newChracter.charname = newName.text;
        newChracter.job = newJob;
        newChracter.level = 1;

        if(characters == null)
            characters = new Dictionary<string, Characters>();
        else
            // 이름이 중복된 캐릭터가 이미 존재하면, 캐릭터 생성 불가능
            foreach(KeyValuePair<string, Characters> eachChar in characters)
            {
                if(eachChar.Value.charname==newChracter.charname)
                {
                    new_ErrTxt.text = "THE NAME HAS ALREADY TAKEN";
                    return;
                }
            }

        // 정상적으로 새 캐릭터 정보 추가
        characters.Add(loginData.counts.ToString(), newChracter);
        loginData.counts++;
        loginData.characters = characters;

        UpdateLoginJson();
        GetCharacters();
        newCharWindow.SetActive(false);
        charWindow.SetActive(true);
        // UI 창의 입력정보 비우기
        newName.text = "";
        new_ErrTxt.text = "";
    }
    
    [Header("New Character Image")]
    public Image newCharImg;
    public Sprite archerImg;
    public Sprite warriorImg;
    public Sprite wizardImg;
    // 새 캐릭터 직업 선택
    public void OnDropdownEvent(int index)
    {
        switch(index)
        {
            case 0: newJob = "Archer"; newCharImg.sprite = archerImg; break;
            case 1: newJob = "Warrior"; newCharImg.sprite = warriorImg; break;
            case 2: newJob = "Wizard"; newCharImg.sprite = wizardImg; break;
        }
        newCharImg.preserveAspect = true;
        print("Select Job: "+newJob);
    }


    /**************************
              로그인창
    ***************************/
    // 로그인 창에서 로그인 버튼 클릭 시
    // 해당 Login Data Json File에서 찾기s
    private string loggedInUsername = "";
    [Header("Login UI")]
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
            LoadLoginJson();

            if(!loginDict.ContainsKey(inputUN))
            {
                errText.text = "INVALID USERNAME";
                return;
            }
            else
            {
                loginData = loginDict[inputUN];
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
    
    [Header("Login / Account Window")]
    public GameObject loginWindow;
    public GameObject accountWindow;
    public TMP_Text profileTxt;


    // 로그인 성공 시
    // 계정 화면 활성화, 메시지 띄우기
    private void LoginSucceess(string inputUN)
    {
        isLoggedIn = true;
        loggedInUsername = inputUN;
        username.text = "";
        password.text = "";
        errText.text = "";
    
        accountWindow.SetActive(true);
        loginWindow.SetActive(false);

        StartCoroutine(sendMessageDisplay(MsgText, "LOGGED IN", Color.green));
        profileTxt.text += loggedInUsername;
    }

    // Account Window -> Logout Button
    // 로그아웃 시 씬 다시 시작
    public void Clicked_Logout()
    {
        SceneManager.LoadScene("1. Login");
    }

    /**************************
            회원가입창
    ***************************/
    [Header("SignUp UI")]
    public GameObject NewAccountWindow;
    public TMP_InputField c_username;
    public TMP_InputField c_password;
    public TMP_InputField conf_password;
    public TMP_Text c_errText;
    
    public void Clicked_CreateNewAccount()
    {
        // Empty check
        bool hasUS = false;
        bool hasPW = false;
        bool hasCPW = false;
        string inputUN = c_username.text;
        string inputPW = c_password.text;
        string inputCPW = conf_password.text;
        
        if(inputUN=="")
        {
            c_errText.text = "INVALID USERNAME";
            return;
        }
        else    hasUS = true;

        if(inputPW=="")
        {
            c_errText.text = "INVALID PASSWORD";
            return;
        }
        else    hasPW = true;
        
        if(inputCPW=="")
        {
            c_errText.text = "INVALID PASSWORD";
            return;
        }
        else    hasCPW = true;

        // 정상적으로 아이디와 비밀번호가 입력됐을 때
        if(hasUS && hasPW && hasCPW)
        {
            // 로그인 Json Data 찾기
            LoadLoginJson();

            // 이미 존재하는 아이디일 때
            if(loginDict.ContainsKey(inputUN))
            {
                c_errText.text = "USERNAME HAS ALREADY TAKEN";
                return;
            }
            else
            {
                // 비밀번호가 너무 짧을 때
                if (inputPW.Length < 5)
                {
                    c_errText.text = "PASSWORD IS TOO SHORT";
                    return;
                }

                // 확인 비밀번호가 일치하지 않을 때
                else if(inputPW != inputCPW)
                {
                    c_errText.text = "PASSWORD MUST MATCH";
                    return;
                }

                // 모두 정상일 때
                else
                {
                    // 새 계정 정보 Json File 저장
                    UpdateNewAccount(inputUN, inputPW);
                    // 회원가입창 닫고
                    NewAccountWindow.SetActive(false);
                    // 새 계정 추가 되었다고 메시지 띄우기
                    StartCoroutine(sendMessageDisplay(MsgText, "ACCOUNT CREATED", Color.green));

                    // 입력칸 초기화
                    c_username.text="";
                    c_password.text="";
                    conf_password.text="";
                }
            }
        }
    }

    // 회원가입 정상적으로 승인될 때 호출
    // 새 계정 정보 추가하여 json file에 업데이트
    private void UpdateNewAccount(string inputUN, string inputPW)
    {
        // Make new account data
        LoginData newAccount = new LoginData();
        newAccount.username = inputUN;
        newAccount.password = inputPW;
        newAccount.counts = 0;

        // Add new data into loginDict
        loginDict.Add(inputUN, newAccount);

        UpdateLoginJson();
    }

    

    /**************************
              종료 창
    ***************************/
    public void Clicked_Terminate()
    {
#if UNITY_EDITOR
        // exit game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
