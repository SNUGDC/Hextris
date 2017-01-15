using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUIController : MonoBehaviour
{
    public Sprite[] Block;

    private GameObject Base;
    private GameObject PausePanel;

    private Image[] NextBlockImage;
    private int[] BlockOrder;

    void Start()
    {
        NextBlockImage = new Image[3];
        NextBlockImage[0] = GameObject.Find("Next Block 1").GetComponent<Image>();
        NextBlockImage[1] = GameObject.Find("Next Block 2").GetComponent<Image>();
        NextBlockImage[2] = GameObject.Find("Next Block 3").GetComponent<Image>();
        Base = GameObject.Find("Base");
        PausePanel = GameObject.Find("Pause Panel");

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
        SceneManager.LoadScene("Stage Select");
    }

    public void BombButtonIsClicked()
    {
        if(Base.GetComponent<BaseController>().IsBlockCreated == true)
            Base.GetComponent<BaseController>().SpecialBlockNumber = 2;
    }

    public void SinkerButtonIsClicked()
    {
        if (Base.GetComponent<BaseController>().IsBlockCreated == true)
            Base.GetComponent<BaseController>().SpecialBlockNumber = 1;
    }
}
