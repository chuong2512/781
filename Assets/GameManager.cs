using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using MoreMountains.NiceVibrations;
public class GameManager : MonoBehaviour
{
    public static GameManager init;
    public Joystick PlayerJoyStick;
    public GameObject PlayerHide;
    public GameObject PlayerSeek;
    [SerializeField]
    private GameObject HomeMenu;
    [SerializeField]
    private GameObject GamePlayUi;
    [SerializeField]
    private GameObject SeekerLoseScene;
    [SerializeField]
    private GameObject WinScene;
    [SerializeField]
    private GameObject HiderLoseScene;
    [SerializeField]
    private GameObject HiderLoseWindow;
    public bool GameStarted;

    [SerializeField]
    private float TimeToHide;
    bool DisableTimer;
    [SerializeField]
    private GameObject TimerHolder;
    [SerializeField]
    private TextMeshProUGUI Timer;
    private bool StartTheTimer;
    public bool SeekerStart;

    public CinemachineVirtualCamera Cam;
    [SerializeField]
    private TextMeshProUGUI CoinText;
    [SerializeField]
    private TextMeshProUGUI CollectedCoinsText;
    public int CoinsAmount;
    public bool AmHider, AmSeeker;
    public bool GameEnded;
    [HideInInspector]
    public bool Iwon;
    [HideInInspector]
    public bool ILost;

    public List<Texture> Chara_Textures = new List<Texture>();
    [Header("Cameras")]
    public CinemachineVirtualCamera PoppyCam;
    public CinemachineVirtualCamera GamePlayCam;
    [Header("Audio Manager")]
    public AudioClip SmallCoin;
    public AudioClip BigCoin;
    public AudioClip HiderWin;
    public AudioClip BabyWin;
    public AudioSource SourceAudio;
    private void Awake()
    {
        init = this;
      
    }

    private void Start()
    {
       
    }

    public void PlaySound(AudioClip clip)
    {
        SourceAudio.clip = clip;
        SourceAudio.Play();
    }

    public void Seek()
    {
        PoppyCam.gameObject.SetActive(true);
        AmSeeker = true;
        TimerHolder.SetActive(true);
        PlayerSeek.GetComponent<PlayerController>().enabled = true;
        Destroy(PlayerHide.GetComponent<PlayerController>());
        PlayerSeek.GetComponent<AiController>().enabled = false;
        PlayerJoyStick.gameObject.SetActive(true);
        HomeMenu.SetActive(false);
        Cam.Follow = PlayerSeek.transform;
        Cam.LookAt = PlayerSeek.transform;
        GameStarted = true;
        StartTheTimer = true;
    }

    public void Hide()
    {
        AmHider = true;
        PlayerSeek.GetComponent<PlayerController>().enabled = false;
        PlayerHide.GetComponent<PlayerController>().enabled = true;
        PlayerHide.GetComponent<AiHider>().enabled = false;
        Destroy(PlayerHide.GetComponent<NavMeshAgent>());
        PlayerJoyStick.gameObject.SetActive(true);
        HomeMenu.SetActive(false);
        Cam.Follow = PlayerHide.transform;
        Cam.LookAt = PlayerHide.transform;
        GameStarted = true;
        StartTheTimer = true;
    }

    private void Update()
    {
        if (StartTheTimer)
        {
            HidingTime();
        }

        CoinSetting();
    }
    private void HidingTime()
    {
        if (!DisableTimer)
        {
            TimeToHide -= Time.deltaTime;
            Timer.text = "Time To Hide " + TimeToHide.ToString("F1");
            if (TimeToHide <= 0)
            {
                SeekerStart = true;
                TimerHolder.SetActive(false);
                DisableTimer = true;
            }
        }
       
       
    }


    private void CoinSetting()
    {
        CoinText.text = CoinsAmount.ToString();
        CollectedCoinsText.text = CoinsAmount.ToString();
    }

    private bool RunOneTime;
    public void Win()
    {
        if (!Iwon&&!GameEnded)
        {
            GamePlayUi.SetActive(false);
            WinScene.SetActive(true);
            CollectedCoinsText.text = CoinsAmount.ToString();
            if (AmHider)
            {
                PlaySound(HiderWin);
            }
            if (AmSeeker)
            {
                PlaySound(BabyWin);
            }
            if (!Iwon)
            {
                Iwon = true;
                MMVibrationManager.Haptic(HapticTypes.Success);
            }
            AdManager.init.ShowInterstitial();
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CoinsAmount);
            print("Show Ad");
        }
  
    }

    public void Lose()
    {
        if (!GameEnded&&!Iwon)
        {
            GamePlayUi.SetActive(false);
            if (AmSeeker)
            {
                SeekerLoseScene.SetActive(true);
            }

            if (AmHider)
            {
                HiderLoseScene.SetActive(true);
                HiderLoseWindow.SetActive(true);
            }
            if (!GameEnded)
            {
                GameEnded = true;
                MMVibrationManager.Haptic(HapticTypes.Failure);
            }
            if (!ILost)
            {
                ILost = true;
            }
            AdManager.init.ShowInterstitial();

            print("Show Ad");
        }

    }

    public void LoadNextScenne()
    {
        if (!RunOneTime)
        {
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            print("Added Level "+ PlayerPrefs.GetInt("Level"));
            RunOneTime = true;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReloadScenne()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelRewardedButton()
    {
        AdManager.init.SceneReward = true;

    }

    public void MoneyRewardedButton()
    {
        AdManager.init.MoneyReward = true;
    }

    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
        if (completed == true)
        {
            LoadNextScenne();
        }
        else
        {
            //no reward
        }
    }

    private void MoneyRewardedButton(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
        if (completed == true)
        {
            CoinsAmount = CoinsAmount * 2;
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CoinsAmount);
        }
        else
        {
            //no reward
        }
    }

    public void AddMoney()
    {
        CoinsAmount = CoinsAmount * 2;
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + CoinsAmount);
    }
}
