using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public Sprite[] Block;

    private GameObject Base;
    private GameObject PausePanel;
    private Image[] SwipeSensitivityButton;
    private Image[] CreateBlockSensitivityButton;

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

        SwipeSensitivityButton = new Image[7];
        CreateBlockSensitivityButton = new Image[7];
        SwipeSensitivityButton[0] = GameObject.Find("Swipe Sensitivity = 1").GetComponent<Image>();
        SwipeSensitivityButton[1] = GameObject.Find("Swipe Sensitivity = 2").GetComponent<Image>();
        SwipeSensitivityButton[2] = GameObject.Find("Swipe Sensitivity = 3").GetComponent<Image>();
        SwipeSensitivityButton[3] = GameObject.Find("Swipe Sensitivity = 4").GetComponent<Image>();
        SwipeSensitivityButton[4] = GameObject.Find("Swipe Sensitivity = 5").GetComponent<Image>();
        SwipeSensitivityButton[5] = GameObject.Find("Swipe Sensitivity = 6").GetComponent<Image>();
        SwipeSensitivityButton[6] = GameObject.Find("Swipe Sensitivity = 7").GetComponent<Image>();
        CreateBlockSensitivityButton[0] = GameObject.Find("Create Block Sensitivity = 1").GetComponent<Image>();
        CreateBlockSensitivityButton[1] = GameObject.Find("Create Block Sensitivity = 2").GetComponent<Image>();
        CreateBlockSensitivityButton[2] = GameObject.Find("Create Block Sensitivity = 3").GetComponent<Image>();
        CreateBlockSensitivityButton[3] = GameObject.Find("Create Block Sensitivity = 4").GetComponent<Image>();
        CreateBlockSensitivityButton[4] = GameObject.Find("Create Block Sensitivity = 5").GetComponent<Image>();
        CreateBlockSensitivityButton[5] = GameObject.Find("Create Block Sensitivity = 6").GetComponent<Image>();
        CreateBlockSensitivityButton[6] = GameObject.Find("Create Block Sensitivity = 7").GetComponent<Image>();


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

        for (int i = 0; i < 7; i++)
        {
            SwipeSensitivityButton[i].color = new Color(1, 1, 1, 0);
        }
        SwipeSensitivityButton[(int)(PlayerPrefs.GetFloat("Swipe Sensitivity") / 70) - 1].color = new Color(1, 1, 1, 1);

        for (int i = 0; i < 7; i++)
        {
            CreateBlockSensitivityButton[i].color = new Color(1, 1, 1, 0);
        }
        CreateBlockSensitivityButton[(int)(PlayerPrefs.GetFloat("Create Block Sensitivity") / 10)].color = new Color(1, 1, 1, 1);
    }

    public void PlayAgain()
    {
        PausePanel.SetActive(false);
        PlayerPrefs.SetString("In Game State", "Play");
    }

    public void SwipeSensitivityButtonControl(int Sensitivity)
    {
        for (int i = 0; i < 7; i++)
        {
            SwipeSensitivityButton[i].color = new Color(1, 1, 1, 0);
        }

        SwipeSensitivityButton[Sensitivity].color = new Color(1, 1, 1, 1);
        PlayerPrefs.SetFloat("Swipe Sensitivity", Sensitivity * 70 + 70);
    }

    public void BlockCreateSensitivityButtonControl(int Sensitivity)
    {
        for (int i = 0; i < 7; i++)
        {
            CreateBlockSensitivityButton[i].color = new Color(1, 1, 1, 0);
        }

        CreateBlockSensitivityButton[Sensitivity].color = new Color(1, 1, 1, 1);
        PlayerPrefs.SetFloat("Create Block Sensitivity", Sensitivity * 10);
    }

    public void BombButtonIsClicked()
    {
        Base.GetComponent<BaseController>().SpecialBlockNumber = 2;
        Debug.Log("숫자를 바꾸긴 함");
    }

    public void SinkerButtonIsClicked()
    {
        Base.GetComponent<BaseController>().SpecialBlockNumber = 1;
        Debug.Log("숫자를 바꾸긴 함");
    }
}
