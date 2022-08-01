using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginData
{
    public string username;
    public string password;
    public int counts;
    public Dictionary<string, Characters> characters;
}

public class Characters
{
    public string charname;
    public string job;
    public int level;
}