using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timerc : MonoBehaviour
{
    [SerializeField] private Image countdownCircleTimer;
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float RandomTime = 30.0f;
    private float currentTime;
    private bool updateTime;
    [SerializeField] private float[] Times;
    private void Start()
    {
       
        int randomIndex = Random.Range(0, Times.Length);
        RandomTime = Times[randomIndex];
        currentTime = RandomTime;
        countdownCircleTimer.fillAmount = 1.0f;
        // Easy way to represent only the seconds and skip the
        // float     
        countdownText.text = (int)currentTime + "s";
        // update the countdown on the update
        updateTime = true;
    }
    private void Update()
    {
        if (updateTime&&GameManager.init.SeekerStart)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0.0f)
            {
                // Stop the countdown timer
                // 
                if (GameManager.init.AmSeeker&&SeekerScore.init.indexScore!=6)
                {
                    GameManager.init.Lose();
                }
                else if (GameManager.init.AmHider)
                {
                    GameManager.init.Win();
                }
                updateTime = false;
                currentTime = 0.0f;
            }
            countdownText.text = (int)currentTime + "s";
            float normalizedValue = Mathf.Clamp(
                     currentTime / RandomTime, 0.0f, 1.0f);
            countdownCircleTimer.fillAmount = normalizedValue;
        }
    }
}
