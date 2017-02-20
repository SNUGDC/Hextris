﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Sprite[] Number;
    public Image[] TimeImage;

    private GameObject Base;
    private int Score;
    private int[] PlayTimeNumber;

    private void Start()
    {
        Base = GameObject.Find ("Base");

        PlayTimeNumber = new int[5];
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("In Game State") == "Play")
        {
            Pause();
        }
    }

    public void Pause()
    {
        Score = Base.GetComponent<BaseController>().Score;

        if (Score < 100)
        {
            TimeImage[4].color = new Vector4(1, 1, 1, 0);
            TimeImage[3].color = new Vector4(1, 1, 1, 0);
            TimeImage[2].color = new Vector4(1, 1, 1, 0);
        }
        else if (Score < 1000)
        {
            TimeImage[4].color = new Vector4(1, 1, 1, 0);
            TimeImage[3].color = new Vector4(1, 1, 1, 0);
            TimeImage[2].color = new Vector4(1, 1, 1, 1);
        }
        else if (Score < 10000)
        {
            TimeImage[4].color = new Vector4(1, 1, 1, 0);
            TimeImage[3].color = new Vector4(1, 1, 1, 1);
            TimeImage[2].color = new Vector4(1, 1, 1, 1);
        }

        PlayTimeNumber[4] = Score / 10000;
        PlayTimeNumber[3] = Score % 10000;
        PlayTimeNumber[2] = PlayTimeNumber[3] % 1000;
        PlayTimeNumber[3] = PlayTimeNumber[3] / 1000;
        PlayTimeNumber[1] = PlayTimeNumber[2] % 100;
        PlayTimeNumber[2] = PlayTimeNumber[2] / 100;
        PlayTimeNumber[0] = PlayTimeNumber[1] % 10;
        PlayTimeNumber[1] = PlayTimeNumber[1] / 10;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (PlayTimeNumber[i] == j)
                {
                    TimeImage[i].sprite = Number[j];
                }
            }
        }
    }
}
