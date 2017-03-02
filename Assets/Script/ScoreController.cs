using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Sprite[] Number;
    public Image[] TimeImage;
    public string WhoAreYou;

    private GameObject Base;
    private int Score;
    private int[] PlayTimeNumber;

    private void Start()
    {
        if (!(PlayerPrefs.HasKey("Best Score")))
        {
            PlayerPrefs.SetInt("Best Score", 0);
        }

        Base = GameObject.Find ("Base");

        PlayTimeNumber = new int[5];
    }

    private void Update()
    {
        Score = Base.GetComponent<BaseController>().Score;

        if (PlayerPrefs.GetString("In Game State") == "Play")
        {
            if (WhoAreYou == "Score")
            {
                DisplayScore(Score);
            }
        }
        if (PlayerPrefs.GetString("In Game State") == "Pause")
        {
            if (WhoAreYou == "Best Score")
            {
                DisplayScore(BestScore());
            }
            else if (WhoAreYou == "Score")
            {
                DisplayScore(Score);
            }
            else if (WhoAreYou == "Coin")
            {
                CoinController(Score / 100);
            }
            else if (WhoAreYou == "Shop Coin")
            {
                CoinController(PlayerPrefs.GetInt("Coin"));
            }
        }
    }

    public void GamePause()
    {
        PlayerPrefs.SetString("In Game State", "Pause");
    }

    private void DisplayScore(int Score)
    {
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

    private int BestScore()
    {
        int BestScore = PlayerPrefs.GetInt("Best Score");

        if (Score > BestScore)
        {
            PlayerPrefs.SetInt("Best Score", Score);
            return Score;
        }
        else
        {
            return BestScore;
        }
    }

    private void CoinController(int Coin)
    {
        if (Coin < 10)
        {
            TimeImage[2].color = new Vector4(1, 1, 1, 0);
            TimeImage[1].color = new Vector4(1, 1, 1, 0);
            TimeImage[0].color = new Vector4(1, 1, 1, 1);
        }
        else if (Coin < 100)
        {
            TimeImage[2].color = new Vector4(1, 1, 1, 0);
            TimeImage[1].color = new Vector4(1, 1, 1, 1);
            TimeImage[0].color = new Vector4(1, 1, 1, 1);
        }
        else if (Coin < 1000)
        {
            TimeImage[2].color = new Vector4(1, 1, 1, 1);
            TimeImage[1].color = new Vector4(1, 1, 1, 1);
            TimeImage[0].color = new Vector4(1, 1, 1, 1);
        }

        for (int i = 0; i < 10; i++)
        {
            if (Coin / 100 == i)
            {
                TimeImage[2].sprite = Number[i];
            }
        }
        for (int i = 0; i < 10; i++)
        {
            Coin = Coin % 100;

            if (Coin / 10 == i)
            {
                TimeImage[1].sprite = Number[i];
            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (Coin % 10 == i)
            {
                TimeImage[0].sprite = Number[i];
            }
        }
    }
}
