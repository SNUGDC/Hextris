using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinController : MonoBehaviour
{
    public Image SkinName;
    public Image BlueTile;
    public Image RedTile;
    public Image YellowTile;
    public GameObject CheckMark;

    public Sprite[] SkinNameWord;
    public Sprite[] SkinBlue;
    public Sprite[] SkinRed;
    public Sprite[] SkinYellow;

    private int SkinNumber;
    private int WhatSkinShow;

    private void Start()
    {
        SkinNumber = PlayerPrefs.GetInt("Skin Number");
        WhatSkinShow = 0;
        CheckMark.SetActive(false);
    }

    private void Update()
    {
        SkinName.sprite = SkinNameWord[WhatSkinShow];
        BlueTile.sprite = SkinBlue[WhatSkinShow];
        RedTile.sprite = SkinRed[WhatSkinShow];
        YellowTile.sprite = SkinYellow[WhatSkinShow];

        if (WhatSkinShow == SkinNumber)
        {
            CheckMark.SetActive(true);
        }
        else
        {
            CheckMark.SetActive(false);
        }
    }

    public void NextSkin()
    {
        if (WhatSkinShow + 1 < SkinNameWord.Length)
        {
            WhatSkinShow = WhatSkinShow + 1;
        }
    }

    public void PriorSkin()
    {
        if (WhatSkinShow - 1 >= 0)
        {
            WhatSkinShow = WhatSkinShow - 1;
        }
    }

    public void SelectSkin()
    {
        SkinNumber = WhatSkinShow;
        PlayerPrefs.SetInt("Skin Number", SkinNumber);
    }
}
