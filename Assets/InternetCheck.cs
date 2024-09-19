using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetCheck : MonoBehaviour
{
    public GameObject CheckTheInternetWindow;
    void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            CheckTheInternetWindow.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            CheckTheInternetWindow.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
