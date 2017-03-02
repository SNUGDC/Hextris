using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    public Sprite[] Block;
    public GameObject PausePanel;

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
    }

    public void BombButtonIsClicked()
    {
        if(Base.GetComponent<BaseController>().IsBlockCreated == true
            && Base.GetComponent<BaseController>().SpecialBlock == 0)
            Base.GetComponent<BaseController>().SpecialBlockNumber = 2;
    }

    public void SinkerButtonIsClicked()
    {
        if (Base.GetComponent<BaseController>().IsBlockCreated == true
            && Base.GetComponent<BaseController>().SpecialBlock == 0)
            Base.GetComponent<BaseController>().SpecialBlockNumber = 1;
    }

    public void GameOver()
    {
        PlayerPrefs.SetString("In Game State", "Pause");
        GetComponent<BoxCollider2D>().enabled = false;
        PausePanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("Basic");
    }
}
