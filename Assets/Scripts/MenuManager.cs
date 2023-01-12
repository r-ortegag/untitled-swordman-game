using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject help;
    public GameObject credits;

    void Start()
    {
        mainMenu.SetActive(true);
        help.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Settings()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Help()
    {
        help.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void Credits()
    {
        credits.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //Back Buttons
    public void BackSettingsMain()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void BackHelpMain()
    {
        mainMenu.SetActive(true);
        help.SetActive(false);
    }

    public void BackCreditsMain()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
    }

}
