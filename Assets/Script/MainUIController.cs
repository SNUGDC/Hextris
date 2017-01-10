using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public Sprite[] Block;
    public Image[] NextBlockImage;

    private int[] BlockOrder;

    public GameObject Base;
    public GameObject PauseUI;
    public GameObject SwipeSensitivityButton;
    public GameObject BlockCreateSensitivityButton;

    void Start()
    {
        BlockOrder = new int[3];
        PauseUI.SetActive (false);
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
        PauseUI.SetActive (true);
        PlayerPrefs.SetString("In Game State", "Pause");
        SwipeSensitivityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(((PlayerPrefs.GetFloat("Swipe Sensitivity") - 60) * 1.2f) - 290, SwipeSensitivityButton.GetComponent<RectTransform>().anchoredPosition.y);
        BlockCreateSensitivityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2((PlayerPrefs.GetFloat("Create Block Sensitivity") * 10f) - 290, BlockCreateSensitivityButton.GetComponent<RectTransform>().anchoredPosition.y);
    }

    public void PlayAgain()
    {
        PauseUI.SetActive(false);
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
            BlockCreateSensitivityButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(Input.mousePosition.x - 550, BlockCreateSensitivityButton.GetComponent<RectTransform>().anchoredPosition.y);
            PlayerPrefs.SetFloat("Create Block Sensitivity", (float)((Input.mousePosition.x - 260) / 10));
        }
    }
}
