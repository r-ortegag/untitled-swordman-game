using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager timerManager;

    public Text timerText;

    private float time = 0;

    void Start()
    {
        if(timerManager == null)
        {
            timerManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    { 
        timerText = GameObject.Find("TimeScore").GetComponent<Text>();
        time += Time.deltaTime;
        timerText.text = time.ToString("f0");
    }
}
