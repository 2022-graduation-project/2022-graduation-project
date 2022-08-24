using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
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
}
