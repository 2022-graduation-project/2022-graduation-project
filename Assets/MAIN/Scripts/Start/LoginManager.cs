using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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
            공통 전광판
    ***************************/
    // Message 에 2초간 메시지 띄우기
    public TMP_Text MsgText;
    private IEnumerator sendMessageDisplay(string message, Color color)
    {
        MsgText.color = color;
        MsgText.text = message;
        yield return new WaitForSeconds(5.0f);
        MsgText.text = "";
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
            StartCoroutine(sendMessageDisplay("LOGIN FIRST", Color.red));
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
    // 로그인 버튼 클릭 시
    // 계정의 캐릭터 정보 로드
    public void Clicked_Arcus()
    {
        GetCharacters();
    }

    private int lastCharacter = 0;
    Dictionary<string, Characters> characters;
    
    [Header("Character UI")]
    public UnityEngine.Object winPrefab;
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
                GameObject characterWin = Instantiate(winPrefab, characterUI) as GameObject;
                // 개별 캐릭터 정보 setting (0: name, 1: job, 2: level)
                char_Info = characterWin.GetComponentsInChildren<TMP_Text>();
                char_Info[0].text = characters[i.ToString()].charname;
                char_Info[1].text = characters[i.ToString()].job;
                char_Info[2].text = "LV."
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
    public TMP_Text info_name;
    public TMP_Text info_job;
    public TMP_Text info_level;
    public void Clicked_Character()
    {
        // info_name.text = name.text;
        // info_job.text = job.text;
        // info_level.text = level.text;
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
    private string newJob = "Archer";
    public void Clicked_NewConfirm()
    {
        // 기본 캐릭터 보유 수를 초과하려 할 때, 캐릭터 생성 불가
        if(loginData.counts >= 5)
        {
            new_ErrTxt.text = "EXCEED DEFAULT NUMBER OF CHARACTERS\n"+
            "YOU SHOULD PAY FOR MORE CHARACTERS";
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
    
    // 새 캐릭터 직업 선택
    public void OnDropdownEvent(int index)
    {
        switch(index)
        {
            case 0: newJob = "Archer"; break;
            case 1: newJob = "Warrior"; break;
            case 2: newJob = "Wizard"; break;
        }
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
    
    [Header("Account / Login Window")]
    public GameObject accountWindow;
    public GameObject loginWindow;
    public TMP_Text profileTxt;

    [Header("Login / Logout Button")]
    public GameObject loginBtn;
    public GameObject logoutBtn;

    // 로그인 성공 시
    // 아이디 저장 및 로그아웃 버튼 활성화, 메시지 띄우기
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

        StartCoroutine(sendMessageDisplay("LOGGED IN", Color.green));
        profileTxt.text = loggedInUsername;
        print(loggedInUsername+" Login Success");
    }

    // Logout Window -> Confirm Button
    // 로그아웃 시 씬 다시 시작
    public void RequestLogout()
    {
        SceneManager.LoadScene("Start");
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
                    StartCoroutine(sendMessageDisplay("ACCOUNT CREATED", Color.green));
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
