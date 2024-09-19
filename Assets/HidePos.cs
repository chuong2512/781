using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePos : MonoBehaviour
{

    public List<Transform> HidePosition=new List<Transform>();
    public static HidePos init;
    public Transform[] CurrentHidePosition ;
    private void Awake()
    {
        init = this;
        foreach (Transform Pos in transform)
        {
            HidePosition.Add(Pos);
        }
        CurrentHidePosition = GetComponentsInChildren<Transform>();

    }

    private void Update()
    {
        CurrentHidePosition = GetComponentsInChildren<Transform>();
    }
}
