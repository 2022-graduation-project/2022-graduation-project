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
    public readonly string type;
    public readonly string content;
}

public class Reward
{
    public readonly float exp;
    public readonly float money;
}

