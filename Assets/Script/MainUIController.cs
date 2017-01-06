using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    public Sprite[] Block;
    public GameObject Base;

    public Image[] NextBlockImage;

    private int[] BlockOrder;

    void Start()
    {
        BlockOrder = new int[3];
    }

    void Update()
    {
        BlockOrder = Base.GetComponent<BaseController> ().BlockOrder;

        for (int i = 0; i <= 2; i++)
        {
            NextBlockImage [i].sprite = Block [BlockOrder [i]];
        }
    }
}
