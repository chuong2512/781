using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using MoreMountains.NiceVibrations;
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float Speed=3f;
    public float Smooth=0.04f;
    float TurnSmoothv;
    public Joystick joyStick;
    public Animator Anim;
    public static PlayerController init;
    public GameObject FOV;
    Vector3 moveVector;
    public bool AmSeeker;
    private void Awake()
    {
        init = this;
    }
    private void Start()
    {
        this.gameObject.AddComponent<CharacterController>();
        controller = GetComponent<CharacterController>();
        controller.center = new Vector3(0, 1.04f, 0);
        joyStick = GameManager.init.PlayerJoyStick;
        Anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
       
        moveVector = Vector3.zero;
        float Horizontal = joyStick.Horizontal;
        float Vertical = joyStick.Vertical;

        Vector3 Direction = new Vector3(Horizontal, 0, Vertical).normalized;
        if (AmSeeker && GameManager.init.SeekerStart&& !GameManager.init.GameEnded&& !GameManager.init.Iwon)
        {
            if (Direction.magnitude >= 0.1f)
            {
                float TargetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, TargetAngle, ref TurnSmoothv, Smooth);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                if (joyStick.Vertical >= 0.8f || joyStick.Vertical <= -0.8f || joyStick.Horizontal <= -0.8f || joyStick.Horizontal >= 0.8f)
                {
                    Anim.Play("Fast Run");
                }
                else
                {
                    Anim.Play("Fast Run");
                }

                controller.SimpleMove(Direction * Speed);

            }
            else
            {
                Anim.Play("Idle");
            }
        }
        else
        {
            if (AmSeeker == false && GameManager.init.GameStarted&& !GameManager.init.GameEnded&& !GameManager.init.Iwon)
            {
                if (Direction.magnitude >= 0.1f)
                {
                    float TargetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg;
                    float angle = Mathf.SmoothDampAngle(transform.localEulerAngles.y, TargetAngle, ref TurnSmoothv, Smooth);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);

                    if (joyStick.Vertical >= 0.8f || joyStick.Vertical <= -0.8f || joyStick.Horizontal <= -0.8f || joyStick.Horizontal >= 0.8f)
                    {
                        Anim.Play("Fast Run");
                    }
                    else
                    {
                        Anim.Play("Fast Run");
                    }

                    controller.SimpleMove(Direction * Speed);

                }
                else
                {
                    Anim.Play("Idle");
                }
                if (!GetComponent<AiHider>().Indicator.activeInHierarchy)
                {
                    GetComponent<AiHider>().Indicator.SetActive(true);
                }
               
            }
        
        }


        if (GameManager.init.SeekerStart&&AmSeeker)
        {
            if (!FOV.activeInHierarchy)
            {
                GameManager.init.PoppyCam.gameObject.SetActive(false);
                FOV.SetActive(true);
            }
        }
        if (GameManager.init.Iwon)
        {
            Anim.Play("Win");
        }
        if (GameManager.init.GameEnded)
        {
           
            Anim.Play("Died");
            this.enabled = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag=="SmallCoin")
        {
            GameManager.init.PlaySound(GameManager.init.SmallCoin);
            print("Small Coin");
            other.gameObject.GetComponent<Collider>().enabled = false;
            GameManager.init.CoinsAmount++;
            other.gameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
            other.gameObject.transform.GetChild(3).gameObject.SetActive(false);
            MMVibrationManager.Haptic(HapticTypes.LightImpact);
            Destroy(other.gameObject, 1);
        }

        if (other.transform.tag == "BigCoin")
        {
            GameManager.init.PlaySound(GameManager.init.BigCoin);
            print("Big Coin");
            other.gameObject.GetComponent<Collider>().enabled = false;
            GameManager.init.CoinsAmount+=4;
            other.gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
            other.gameObject.transform.GetChild(3).gameObject.SetActive(false);
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
            Destroy(other.gameObject, 1);
        }
    }

}
