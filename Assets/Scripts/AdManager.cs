using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    private string gameId = "3492176";
    private bool testMode = true;

    void Start()
    {
#if UNITY_ANDROID
            Advertisement.Initialize(gameId, testMode);
            Advertisement.Show();
#endif
    }
}