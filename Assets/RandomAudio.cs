using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudio : MonoBehaviour
{
    public AudioSource ClipPlayer;
    public AudioClip[] Clips;

    private void Start()
    {
        int RandomClip = Random.Range(0, Clips.Length);
        ClipPlayer.clip = Clips[RandomClip];
        ClipPlayer.Play();
    }
}
