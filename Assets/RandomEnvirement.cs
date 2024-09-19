using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnvirement : MonoBehaviour
{
    public GameObject[] Envirement;

    private void Start()
    {
        int randomIndex = Random.Range(0, Envirement.Length);
        for (int i = 0; i < Envirement.Length; i++)
        {
            if (randomIndex==i)
            {
                Envirement[i].SetActive(true);
            }
            else
            {
                Envirement[i].SetActive(false);
            }
           
        }
    }
}
