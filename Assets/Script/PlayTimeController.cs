using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimeController : MonoBehaviour
{
    public Sprite[] Number;
    public Image[] TimeImage;

    private GameObject Base;
    private float GameTime_float;
    private int GameTime_int;
    private int[] PlayTimeNumber;

    private void Start()
    {
        Base = GameObject.Find ("Base");

        PlayTimeNumber = new int[3];
    }

    private void Update()
    {

    }

    public void Pause()
    {
        GameTime_float = Base.GetComponent<BaseController>().GameTime;

        GameTime_int = (int)GameTime_float;
        PlayTimeNumber[0] = GameTime_int / 60;

        GameTime_int = GameTime_int - PlayTimeNumber[0] * 60;
        PlayTimeNumber[1] = GameTime_int / 10;
        PlayTimeNumber[2] = GameTime_int % 10;

        for (int i = 0; i < 3; i++)
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
