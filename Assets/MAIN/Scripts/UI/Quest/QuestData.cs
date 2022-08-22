using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string title;
    public string content;
    public List<Goal> goal;
    public Reward reward;
}

public class Goal
{
    public string type;
    public string content;
}

public class Reward
{
    public float exp;
    public float money;
}

