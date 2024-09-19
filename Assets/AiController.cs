using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiController : MonoBehaviour
{
    public static AiController init;
    private Transform[] PosSpot;
    private NavMeshAgent Agent;
    private Animator anim;
    public GameObject FOV;

    bool checkOneTime;

    private void Awake()
    {
        init = this;
    }
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        Agent = GetComponent<NavMeshAgent>();
       
    }
    private void Update()
    {
        if (GameManager.init.SeekerStart)
        {
            if (Agent.velocity.magnitude > 0.15f)
            {
                anim.Play("Fast Run");
            }
            else
            {
                anim.Play("Idle");
                StartCoroutine(DelayForDestination());

            }
        }
        if (GameManager.init.SeekerStart)
        {
            if (!FOV.activeInHierarchy)
            {
                FOV.SetActive(true);
            }
        }

        if (GameManager.init.Iwon)
        {
            FOV.SetActive(false);
            anim.Play("Died");
            Agent.enabled = false;
            this.enabled = false;
        }
    }

    IEnumerator DelayForDestination()
    {
        yield return new WaitForSeconds(1f);
        checkOneTime = false;
        SetDestination();
        
    }
    private void SetDestination()
    {
        if (!checkOneTime&& !Agent.hasPath)
        {
            PosSpot = HidePos.init.HidePosition.ToArray();
            Transform distination = PosSpot[Random.Range(0, PosSpot.Length)];
            Agent.SetDestination(distination.position);
            checkOneTime = true;
        }
     
    }


    public void CatchPlayers()
    {

    }
}
