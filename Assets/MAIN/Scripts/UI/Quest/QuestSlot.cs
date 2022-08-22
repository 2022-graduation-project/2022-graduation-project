using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot : MonoBehaviour
{
    public Text title;
    public Text content;
    public Text money;
    public Text exp;

    public void Set(QuestData _questData)
    {
        title.text = _questData.title;
        content.text = _questData.content;
        money.text = _questData.reward.money.ToString();
        exp.text = _questData.reward.exp.ToString();
    }
}