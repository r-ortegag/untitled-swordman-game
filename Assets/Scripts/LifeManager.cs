using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager lifeManager;

    public Text lifePoints;

    public int life = 10;

    void Start()
    {
        if (lifeManager == null)
        {
            lifeManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (lifePoints == null)
        {
            lifePoints = GameObject.Find("LifePoints").GetComponent<Text>();
            lifePoints.text = life + "";
        }

        if(life <= 0)
        {
            life = 10;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void RaiseLife(int coinLife)
    {
        life += coinLife;
        lifePoints.text = life + "";
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        lifePoints.text = life + "";
    }
}
