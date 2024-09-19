using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class AiHider : MonoBehaviour
{
    private NavMeshAgent agent;

    public Animator anim;
    public Transform Spot;
    public bool StartMoving;
    bool runonce;
    public bool AmDead;
    [SerializeField] private GameObject dustCloud;
    public GameObject Indicator;
    public SkinnedMeshRenderer Texture;
    public int Index;
    private void Start()
    {
        //anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (GameManager.init.GameStarted)
        {
            StartCoroutine(HidingDelay());
            if (agent.velocity.magnitude > 0.15f)
            {
                anim.Play("Fast Run");
                dustCloud.SetActive(true);
            }
            else
            {
                anim.Play("Idle");
                StartCoroutine(SetRandomDis());
                dustCloud.SetActive(false);
            }
          
        }

        if (AmDead)
        {
            anim.gameObject.SetActive(true);
        }
    }

    IEnumerator HidingDelay()
    {
        yield return new WaitForSeconds(1f);
        if (!AmDead&&GameManager.init.AmSeeker)
        {
            anim.gameObject.SetActive(false);
            dustCloud.SetActive(true);
        }
     
    }
    IEnumerator SetRandomDis()
    {
        runonce = true;
        yield return new WaitForSeconds(0.1f);
        if (runonce&& StartMoving&& AmDead==false)
        {
            agent.SetDestination(Spot.position);
            StartMoving = false;
            runonce = false;
        }
  
    }
}
