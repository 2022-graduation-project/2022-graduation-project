using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginManage : MonoBehaviour
{
    public GameObject LoginWindow;
    string m_Path;

    [Header("ADVANDED - Panels")]
    [Tooltip("The UI Panel holding the New Account Screen elements")]
    public GameObject newAccountScreen;
    [Tooltip("The UI Panel holding the Delete Account Screen elements")]
    public GameObject deleteAccountScreen;
    [Tooltip("The UI Panel holding Log-In Buttons")]
    public GameObject loginScreen;
    [Tooltip("The UI Panel holding account and load menu")]
    public GameObject databaseScreen;
    [Tooltip("The UI Menu Bar at the edge of the screen")]
    public GameObject menuBar;

    [Header("Register Account")]
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField confPassword;
    public TMP_Text error_NewAccount;
    public TMP_Text messageDisplayDatabase;
    public string newAccountMessageDisplay = "ACCOUNT CREATED";
    private string Username;
    private string Password;
    private string ConfPassword;
    private string form;
    private string[] Characters = {"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                   "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
                                   "1","2","3","4","5","6","7","8","9","0","_","-"};

    [Header("Login Account")]
    public TMP_InputField logUsername;
    public TMP_InputField logPassword;
    private string logUsernameString; // the input in logUsername
    private string logPasswordString; // the input in logPassword
    private String[] Lines;
    private string DecryptedPass;
    public TMP_Text error_LogIn;
    public TMP_Text profileDisplay;
    public string loginMessageDisplay = "LOGGED IN";
    [HideInInspector]
    public bool isLoggedIn = false;

    // 맨 첫 로그인 버튼
    public void ClickLoginButton()
    {
        LoginWindow.SetActive(true);
    }

    [Header("Profile Status")]
    public TMP_Text profileName;

    [Header("Server Panel")]
    public GameObject serverPanel;

    // Load Last Save 버튼
    public void ClickLoadLastSave()
    {
        if (!isLoggedIn)
        {
            MessageDisplayDatabase("Login First", Color.red);
            return;
        }
        else
        {
            serverPanel.SetActive(true);
        }
    }


    // 회원가입 버튼 누르고 나서
    public void UpdateAccountValues()
    {
        // Register
        Username = username.text;
        Password = password.text;
        ConfPassword = confPassword.text;

        // Log In
        logUsernameString = logUsername.text;
        logPasswordString = logPassword.text;

        //// Delete
        //delUsernameString = delUsername.text;
        //delPasswordString = delPassword.text;
    }

    // 회원가입 버튼 눌렀을 때
    public void ConfirmNewAccount()
    {

        UpdateAccountValues();

        bool UN = false;
        bool PW = false;
        bool CPW = false;

        if (Username != "")
        {
            if (!System.IO.File.Exists(m_Path + "_" + Username + ".txt"))
            {
                UN = true;
            }
            else
            {
                error_NewAccount.text = "USERNAME ALREADY TAKEN";
            }
        }
        else
        {
            error_NewAccount.text = "INVALID USERNAME";
        }
        if (Password != "")
        {
            if (Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                error_NewAccount.text = "PASSWORD IS TOO SHORT";
            }
        }
        else
        {
            error_NewAccount.text = "INVALID PASSWORD";
        }
        if (ConfPassword != "")
        {
            if (ConfPassword == Password)
            {
                CPW = true;
            }
            else
            {
                error_NewAccount.text = "PASSWORDS MUST MATCH";
            }
        }
        else
        {
            error_NewAccount.text = "INVALID PASSWORD";
        }
        if (UN == true && PW == true && CPW == true)
        {
            bool Clear = true;
            int i = 1;
            foreach (char c in Password)
            {
                if (Clear)
                {
                    Password = "";
                    Clear = false;
                }
                i++;
                char Encrypted = (char)(c * i);
                Password += Encrypted.ToString();
            }
            form = (Username + Environment.NewLine + Password + Environment.NewLine + "counts0");
            System.IO.File.WriteAllText(m_Path + "_" + Username + ".txt", form);
            Username = "";
            Password = "";
            username.text = "";
            password.text = "";
            confPassword.text = "";
            error_NewAccount.text = "";
            DecryptedPass = "";

            MessageDisplayDatabase(newAccountMessageDisplay, Color.green);
            print("Registration Complete");
            databaseScreen.SetActive(true);
            newAccountScreen.SetActive(false);
        }
    }

    // 메시지 전달
    // called when returned back to the database menu, confirmation message displays temporarily
    public void MessageDisplayDatabase(string message, Color col)
    {
        StartCoroutine(MessageDisplay(message, col));
    }

    // 메시지 띄우기
    IEnumerator MessageDisplay(string message, Color col)
    { // Display and then clear
        messageDisplayDatabase.color = col;
        messageDisplayDatabase.text = message;
        yield return new WaitForSeconds(2.0f);
        messageDisplayDatabase.text = "";
    }

    // 로그인 창의 로그인 버튼
    public void ClickAcceptButton()
    {
        logUsernameString = logUsername.text;
        logPasswordString = logPassword.text;

        bool UN = false;
        bool PW = false;



        if (logUsernameString != "")
        {
            if (System.IO.File.Exists(m_Path + "_" + logUsernameString + ".txt"))
            {
                UN = true;
                Lines = System.IO.File.ReadAllLines(m_Path + "_" + logUsernameString + ".txt");
            }

            else
            {
                error_LogIn.text = "INVALID USERNAME";
            }
        }
        else
        {
            error_LogIn.text = "PLEASE ENTER USERNAME";
        }

        if (logPasswordString != "")
        {
            if (System.IO.File.Exists(m_Path + "_" + logUsernameString + ".txt"))
            {
                int i = 1;
                foreach (char c in Lines[1])
                {
                    i++;
                    char Decrypted = (char)(c / i);
                    DecryptedPass += Decrypted.ToString();
                }
                if (logPasswordString == DecryptedPass)
                {
                    PW = true;
                }
                else
                {
                    error_LogIn.text = "PASSWORD INCORRECT";
                }
            }
            else
            {
                error_LogIn.text = "PASSWORD INCORRECT";
            }
        }
        else
        {
            error_LogIn.text = "PLEASE ENTER PASSWORD";
        }
        if (UN == true && PW == true)
        {
            profileName.text = logUsernameString;
            logUsernameString = "";
            logPasswordString = "";
            logUsername.text = "";
            logPassword.text = "";
            error_LogIn.text = "";

            MessageDisplayDatabase(loginMessageDisplay, Color.green);
            print("Login Successful");
            
            // 새 로그인 시 캐선창 초기화
            lastCharacter = 1;
            Transform[] childList = contents.GetComponentsInChildren<Transform>();
            if (childList != null)
            {
                // contents 부모 자신은 피하기 위해 i=1부터
                for(int i = 1; i < childList.Length; i++)
                {
                    // contents 부모 자신 한 번 더 피하기
                    if (childList[i] != transform)
                    {
                        Destroy(childList[i].gameObject);
                    }
                }
            }

            isLoggedIn = true;

            databaseScreen.SetActive(true);
            loginScreen.SetActive(false);
        }
    }

    // 창 앞으로 띄우기
    Transform tempParent;
    public void MoveToFront(GameObject currentObj)
    {
        //tempParent = currentObj.transform.parent;
        tempParent = currentObj.transform;
        tempParent.SetAsLastSibling();
    }

    public TMP_Text charName;
    private string newForm;
    private string newName;
    // 캐릭터 생성
    public void SetCharacter()
    {
        newName = charName.text;
        newForm = "";

        foreach(string s in Lines)
        {
            if (s.Contains("counts"))
            {
                print("counts" + (int.Parse(Regex.Replace(s, @"\D", "")) + 1).ToString());
                newForm += "counts" + (int.Parse(Regex.Replace(s, @"\D", "")) + 1).ToString();
                newForm += Environment.NewLine;
            }
            else
            {
                newForm += s;
                newForm += Environment.NewLine;
            }
        }

        newForm += newName;

        //System.IO.File.Delete(m_Path + "_" + profileName.text + ".txt");
        //System.IO.File.WriteAllText(m_Path + "_" + profileName.text + ".txt", newForm);
        
        //overwrite
        using (var writer = new StreamWriter(m_Path + "_" + profileName.text + ".txt", append: false))
        {
            writer.WriteLine(newForm);
        }

    }

    public Transform contents;
    public UnityEngine.Object characterPrefab;
    private int lastCharacter = 1;

    // 로그인 정보에서 캐릭터 정보 가져오기
    public void GetCharacters()
    {
        print("lastCharacter: " + lastCharacter);
        // 유저 정보 갱신
        Lines = System.IO.File.ReadAllLines(m_Path + "_" + profileName.text + ".txt");

        // 보유 캐릭터 없을 때
        // "counts0" -> (int) 0
        int countsOfCharacters = int.Parse(Regex.Replace(Lines[2], @"\D", ""));
        if (countsOfCharacters == 0)
        {
            print("No Characters");
        }

        else
        {
            for(int i = lastCharacter; i <= countsOfCharacters; i++)
            {
                //반복문 -> 프리펩 스크롤뷰 안에 캐릭터창 생성
                GameObject characterWin = Instantiate(characterPrefab, contents) as GameObject;
                characterWin.transform.FindChild("Name").GetComponent<TMP_Text>().text = Lines[2 + i];
            }
            lastCharacter = countsOfCharacters + 1;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // 로그인 데이터 저장소 경로
        m_Path = Application.dataPath;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
