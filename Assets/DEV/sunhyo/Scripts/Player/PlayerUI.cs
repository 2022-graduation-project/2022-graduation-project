using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Image hp;
    public Image mp;

    public void Set(PlayerData _playerData)
    {
        hp.fillAmount = _playerData.curHp / _playerData.maxHp;
        mp.fillAmount = _playerData.curMp / _playerData.maxMp;
    }

    public void UpdateHpBar(float _maxHp, float _curHp)
    {
        print("체력 변경" + (_curHp / _maxHp));
        hp.fillAmount = _curHp / _maxHp;
    }

    public void UpdateMpBar(float _maxMp, float _curMp)
    {
        mp.fillAmount = _curMp / _maxMp;
    }
}