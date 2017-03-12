using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    public Sprite[] Block;
    public Sprite[] GameState;
    public GameObject PausePanel;
    public GameObject PlayAgainButton;
    public GameObject VideoAdvertise;
    public Image GameStateWord;

    private GameObject Base;
    
    private Image[] NextBlockImage;
    private int[] BlockOrder;

    void Start()
    {
        NextBlockImage = new Image[3];
        NextBlockImage[0] = GameObject.Find("Next Block 1").GetComponent<Image>();
        NextBlockImage[1] = GameObject.Find("Next Block 2").GetComponent<Image>();
        NextBlockImage[2] = GameObject.Find("Next Block 3").GetComponent<Image>();
        Base = GameObject.Find("Base");
        VideoAdvertise.SetActive(false);

        GameStateWord.sprite = GameState[0];
        BlockOrder = new int[3];
        PausePanel.SetActive(false);
    }

    void Update()
    {
        BlockOrder = Base.GetComponent<BaseController> ().BlockOrder;

        for (int i = 0; i <= 2; i++)
        {
            NextBlockImage [i].sprite = Block [BlockOrder [i]];
        }
    }

    public void Pause()
    {
        PausePanel.SetActive (true);
        GameStateWord.sprite = GameState[0];
        PlayerPrefs.SetString("In Game State", "Pause");
    }

    public void PlayAgain()
    {
        PausePanel.SetActive(false);
        PlayerPrefs.SetString("In Game State", "Play");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Start");
        int Coin = Base.GetComponent<BaseController>().Score / 100;

        if (!(PlayerPrefs.HasKey("Coin")))
        {
            PlayerPrefs.SetInt("Coin", Coin);
        }
        else
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + Coin);
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetString("In Game State", "Pause");
        GetComponent<BoxCollider2D>().enabled = false;
        GameStateWord.sprite = GameState[1];
        PlayAgainButton.SetActive(false);
        VideoAdvertise.SetActive(true);
        PausePanel.SetActive(true);
    }
}
