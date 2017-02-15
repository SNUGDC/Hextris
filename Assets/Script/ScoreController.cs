using System.Collections;
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

    }

    public void Pause()
    {
        Score = Base.GetComponent<BaseController>().Score;

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
