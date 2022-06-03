using System;
using System.Collections;
using System.Collections.Generic;
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

    // 맨 첫 로그인 버튼
    public void ClickLoginButton()
    {
        LoginWindow.SetActive(true);
    }

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
            form = (Username + Environment.NewLine + Environment.NewLine + Password);
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

    // called when returned back to the database menu, confirmation message displays temporarily
    public void MessageDisplayDatabase(string message, Color col)
    {
        StartCoroutine(MessageDisplay(message, col));
    }

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
                foreach (char c in Lines[2])
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
            logUsernameString = "";
            logPasswordString = "";
            logUsername.text = "";
            logPassword.text = "";
            error_LogIn.text = "";

            MessageDisplayDatabase(loginMessageDisplay, Color.green);
            print("Login Successful");

            databaseScreen.SetActive(true);
            loginScreen.SetActive(false);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        m_Path = Application.dataPath;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
