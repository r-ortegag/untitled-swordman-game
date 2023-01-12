using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMobile : MonoBehaviour
{
    public GameObject controlsMobile;

    bool isMobile;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
            controlsMobile.SetActive(true);
            isMobile = true;
#else 
            controlsMobile.SetActive(false);
            isMobile = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_ANDROID
        if(controlsMobile == null)
        {
            controlsMobile = GameObject.Find("ControlsMobile");
            if (isMobile)
            {
                controlsMobile.SetActive(true);
            }
            else
            {
                controlsMobile.SetActive(false);
            }
        }
#endif
    }
}
