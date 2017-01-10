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
    }
}
