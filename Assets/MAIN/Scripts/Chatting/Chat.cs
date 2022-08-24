using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public static Chat instance;
    void Awake() => instance = this;

    public TMP_InputField sendInput;
    public RectTransform chatContent;
    public TMP_Text chatText;
    public ScrollRect chatScrollRect;

    public void ShowMessage(string data)
    {
        chatText.text += chatText.text == "" ? data : "\n" + data;

        Fit(chatText.GetComponent<RectTransform>());
        Fit(chatContent);
        Invoke("ScrollDelay", 0.03f);
    }

    void Fit(RectTransform rect) => LayoutRebuilder.ForceRebuildLayoutImmediate(rect);

    void ScrollDelay() => chatScrollRect.verticalScrollbar.value = 0;
    
}
