using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text nameTxt;
    public Text levelTxt;
    public Text classTxt;
    public Text moneyTxt;

    public Image hpBar;
    public Image mpBar;

    /*************************************************/
    /* 각 컴포넌트들 엔진 사용하지 않고 코드로 직접 접근 */
    /*************************************************/

    public void Set(PlayerData _playerData)
    {
        nameTxt.text = _playerData.name;
        levelTxt.text = "Lv. " + _playerData.level.ToString();
        classTxt.text = _playerData.cls;
        moneyTxt.text = _playerData.money.ToString();

        hpBar.fillAmount = 1;
        mpBar.fillAmount = 1;
    }
}