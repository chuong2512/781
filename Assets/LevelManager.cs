using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;

    public GameObject[] ActiveLater;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        if (PlayerPrefs.GetInt("Level")>Levels.Length)
        {
            PlayerPrefs.SetInt("Level", 0);
        }

        GameObject Level = Instantiate(Levels[PlayerPrefs.GetInt("Level")]);
        for (int i = 0; i < ActiveLater.Length; i++)
        {
            ActiveLater[i].SetActive(true);
        }
        print("Level "+PlayerPrefs.GetInt("Level"));
    }
}
