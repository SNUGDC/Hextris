using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public Sprite[] Block;    

    private GameObject Base;
    private GameObject PausePanel;
    private GameObject SwipeSensitivityButton;
    private GameObject CreateBlockSensitivityButton;

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
        SwipeSensitivityButton = GameObject.Find("Swipe Sensitivity Button");
        CreateBlockSensitivityButton = GameObject.Find("Create Block Sensitivity Button");

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
        SwipeSensitivityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(((PlayerPrefs.GetFloat("Swipe Sensitivity") - 60) * 1.2f) - 290, SwipeSensitivityButton.GetComponent<RectTransform>().anchoredPosition.y);
        CreateBlockSensitivityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2((PlayerPrefs.GetFloat("Create Block Sensitivity") * 10f) - 290, CreateBlockSensitivityButton.GetComponent<RectTransform>().anchoredPosition.y);
    }

    public void PlayAgain()
    {
        PausePanel.SetActive(false);
        PlayerPrefs.SetString("In Game State", "Play");
    }

    public void SwipeSensitivityButtonControl()
    {
        if (Input.mousePosition.x >= 260 && Input.mousePosition.x <= 830)
        {
            SwipeSensitivityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x - 550, SwipeSensitivityButton.GetComponent<RectTransform>().anchoredPosition.y);
            PlayerPrefs.SetFloat("Swipe Sensitivity", (float)((Input.mousePosition.x - 260) / 1.2) + 60);
        }
    }

    public void BlockCreateSensitivityButtonControl()
    {
        if (Input.mousePosition.x >= 260 && Input.mousePosition.x <= 830)
        {
            CreateBlockSensitivityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x - 550, CreateBlockSensitivityButton.GetComponent<RectTransform>().anchoredPosition.y);
            PlayerPrefs.SetFloat("Create Block Sensitivity", (float)((Input.mousePosition.x - 260) / 10));
        }
    }
}
