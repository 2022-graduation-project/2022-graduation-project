using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class QuestData
{
    public string title;
    public string content;
    public List<Goal> goal = new List<Goal>();
    public Reward reward;
}

class Goal
    {
        public readonly bool type;
        public readonly string content;
    }

    class Reward
    {
        public readonly float exp;
        public readonly float money;
    }

