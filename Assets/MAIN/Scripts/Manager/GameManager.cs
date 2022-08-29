using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool moveable = true;
    public bool mouseavail = true;
    public bool keyboardavail = true;

    public Transform spawn_pos;

    private Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
    private Transform player_tr;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public WaitForSeconds GetWaitForSeconds(float _time)
    {
        if (!waitForSeconds.ContainsKey(_time))
        {
            waitForSeconds.Add(_time, new WaitForSeconds(_time));
        }
            
        return waitForSeconds[_time];
    }

    public void GameOver(Transform _player_tr)
    {
        player_tr = _player_tr;

        moveable = mouseavail = keyboardavail = false;
    }

    public void GameSet()
    {
        player_tr.position = spawn_pos.position;

        // 
    }
}