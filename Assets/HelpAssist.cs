using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpAssist : MonoBehaviour
{
    void LateUpdate()
    {
        var target = GameManager.init.GamePlayCam.transform.position;// Camera.main.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
    }
}
