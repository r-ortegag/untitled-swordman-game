using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager scoreManager;

    public Text scoreText;

    int score = 0;

    void Start()
    {
        if(scoreManager == null)
        {
            scoreManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Update()
    {
        if(scoreText == null) 
        {
            scoreText = GameObject.Find("ScorePoints").GetComponent<Text>();
            scoreText.text = score + "";
        }
    }

    public void RaiseScore(int s)
    {
        score += s;
        scoreText.text = score + "";
    }
}
