using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Lose : MonoBehaviour
{
    public GameObject Cadge;
    private Animator anim;
    public bool Lost;
    bool AllreadyAdded;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void GameOver()
    {
        Cadge.SetActive(true);
        anim.Play("Died");
        if (AllreadyAdded == false)
        {
            SeekerScore.init.AddScore();
            AllreadyAdded = true;
        }
        Lost = true;
        if (GetComponent<NavMeshAgent>()!=null)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }
        if (GetComponent<PlayerController>() != null)
        {
            GameManager.init.Lose();
        }
        GetComponent<AiHider>().AmDead = true;
        print("Dead");
        Destroy(GetComponent<Lose>());
       
    }

}
