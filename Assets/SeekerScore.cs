using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SeekerScore : MonoBehaviour
{
    private List<Image> ScoreImage = new List<Image>();
    public Color CatchColor;
    public Color UncatchColor;
    public int indexScore;
    public static SeekerScore init;

    private void Awake()
    {
        init = this;
    }
    void Start()
    {
        foreach (Transform item in transform)
        {
            ScoreImage.Add(item.GetComponent<Image>());
        }
    }

    private void Update()
    {
        if (indexScore==6&&GameManager.init.AmSeeker)
        {
            GameManager.init.Win();
        }
    }
    public void AddScore()
    {
        ScoreImage[indexScore].color = CatchColor;
        indexScore++;
    }
}
