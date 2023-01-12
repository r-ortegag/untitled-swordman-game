using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameObject.tag == "ChangeLvl2")
        {
            SceneManager.LoadScene("Level2");
        }
        if (collision.gameObject.tag == "Player" && gameObject.tag == "ChangeLvl3")
        {
            SceneManager.LoadScene("Level3");
        }
    }
}
