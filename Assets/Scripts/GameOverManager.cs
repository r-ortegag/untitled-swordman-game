using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        Destroy(GameObject.Find("GameManager"));
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
