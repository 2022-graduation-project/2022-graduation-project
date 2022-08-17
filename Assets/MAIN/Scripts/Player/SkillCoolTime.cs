using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolTime : MonoBehaviour
{
    /* UI Objects */
    public Image img_UI;   // 남은 시간 시각적으로 표시
    public Text txt_UI;   // 남은 시간 표시

    /* Local Variables */
    private float coolTime;  // 스킬 쿨타임
    private float curTime;  // 현재 시간
    private float startTime;
    private bool isEnded;


    void Awake()
    {
        coolTime = 5.0f;
        isEnded = true;
    }

    void Start()
    {
        /* 360도 시계 반대 방향으로 회전하게 설정 (초기 세팅) */
        img_UI.type = Image.Type.Filled;
        img_UI.fillMethod = Image.FillMethod.Radial360;
        img_UI.fillOrigin = (int)Image.Origin360.Top;
        img_UI.fillClockwise = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Trigger_Skill();
        }
        if (isEnded)
            return;

        CheckCoolTime();
    }





    private void CheckCoolTime()   // 스킬 재사용까지 남은 시간을 검사 및 표시 (Update, for, while, 등 반복문에 삽입) 
    {
        curTime = Time.time - startTime;

        if (curTime < coolTime)
        {
            Set_FillAmount(coolTime - curTime);
        }
        else if (!isEnded)
        {
            End_CoolTime();
        }
    }

    private void End_CoolTime() // 쿨타임이 끝나서 스킬 재사용이 가능해진 시점
    {
        Set_FillAmount(0);
        isEnded = true;
        txt_UI.gameObject.SetActive(false);
        Debug.Log("Skill Available !");
    }

    private void Trigger_Skill()    // 스킬 발동
    {
        if (!isEnded)
        {
            Debug.LogError("Hold On");
            return;
        }

        Reset_CoolTime();
    }

    private void Reset_CoolTime()   // 쿨타임 리셋
    {
        txt_UI.gameObject.SetActive(true);
        curTime = coolTime;
        startTime = Time.time;
        Set_FillAmount(coolTime);
        isEnded = false;
    }

    private void Set_FillAmount(float _value)   // 스킬 재사용 시간 시각화
    {
        img_UI.fillAmount = _value / coolTime;

        string txt = _value.ToString("0");
        txt_UI.text = txt;
    }
}
