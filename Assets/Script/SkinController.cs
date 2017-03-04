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
    public GameObject SkinShadowPanel;

    public Sprite[] SkinNameWord;
    public Sprite[] SkinBlue;
    public Sprite[] SkinRed;
    public Sprite[] SkinYellow;
    public Sprite[] NumberImage;
    public int[] SkinPrice;
    public Image[] PriceImage;
    public Image[] CoinImage;

    private bool[] BoughtSkin;
    private int SkinNumber;
    private int WhatSkinShow;
    private int NowSkinPrice;
    private int Coin;

    private void Start()
    {
        BoughtSkin = new bool[5] {true, false, false, false, false};

        if (!(PlayerPrefs.HasKey("Coin")))
        {
            PlayerPrefs.SetInt("Coin", 0);
        }

        Coin = PlayerPrefs.GetInt("Coin");

        if (!(PlayerPrefs.HasKey("Skin Number")))
        {
            PlayerPrefs.SetInt("Skin Number", 0);
        }
        SkinNumber = PlayerPrefs.GetInt("Skin Number");
        SkinShadowPanel.SetActive(false);
        WhatSkinShow = 0;
        CheckMark.SetActive(false);
    }

    private void Update()
    {
        SkinName.sprite = SkinNameWord[WhatSkinShow];
        BlueTile.sprite = SkinBlue[WhatSkinShow];
        RedTile.sprite = SkinRed[WhatSkinShow];
        YellowTile.sprite = SkinYellow[WhatSkinShow];
        NowSkinPrice = SkinPrice[WhatSkinShow];

        if (WhatSkinShow == SkinNumber)
        {
            CheckMark.SetActive(true);
        }
        else
        {
            CheckMark.SetActive(false);
        }

        ShowCoin();
        ShowSkinPrice();
        SkinShadowPanelController();
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

    private void ShowSkinPrice()
    {
        switch (WhatSkinShow)
        {
            case 0:
                PriceImage[2].color = new Vector4(1, 1, 1, 0);
                PriceImage[1].color = new Vector4(1, 1, 1, 0);
                PriceImage[0].color = new Vector4(1, 1, 1, 1);

                PriceImage[0].sprite = NumberImage[0];
                PriceImage[1].sprite = NumberImage[0];
                PriceImage[2].sprite = NumberImage[0];
                break;
            case 1:
                PriceImage[2].color = new Vector4(1, 1, 1, 1);
                PriceImage[1].color = new Vector4(1, 1, 1, 1);
                PriceImage[0].color = new Vector4(1, 1, 1, 1);

                PriceImage[0].sprite = NumberImage[0];
                PriceImage[1].sprite = NumberImage[0];
                PriceImage[2].sprite = NumberImage[3];
                break;
            case 2:
                PriceImage[2].color = new Vector4(1, 1, 1, 0);
                PriceImage[1].color = new Vector4(1, 1, 1, 1);
                PriceImage[0].color = new Vector4(1, 1, 1, 1);

                PriceImage[0].sprite = NumberImage[0];
                PriceImage[1].sprite = NumberImage[5];
                PriceImage[2].sprite = NumberImage[0];
                break;
            case 3:
                PriceImage[2].color = new Vector4(1, 1, 1, 1);
                PriceImage[1].color = new Vector4(1, 1, 1, 1);
                PriceImage[0].color = new Vector4(1, 1, 1, 1);

                PriceImage[0].sprite = NumberImage[0];
                PriceImage[1].sprite = NumberImage[0];
                PriceImage[2].sprite = NumberImage[1];
                break;
            case 4:
                PriceImage[2].color = new Vector4(1, 1, 1, 1);
                PriceImage[1].color = new Vector4(1, 1, 1, 1);
                PriceImage[0].color = new Vector4(1, 1, 1, 1);

                PriceImage[0].sprite = NumberImage[0];
                PriceImage[1].sprite = NumberImage[0];
                PriceImage[2].sprite = NumberImage[5];
                break;
            default:
                Debug.Log("Something is Wrong");
                break;
        }
    }

    public void BuySkin()
    {
        if (Coin >= NowSkinPrice)
        {
            BoughtSkin[WhatSkinShow] = true;
        }
        else
        {
            Debug.Log("Coin이 없엉...");
        }
    }

    private void SkinShadowPanelController()
    {
        if (BoughtSkin[WhatSkinShow] == true)
        {
            SkinShadowPanel.SetActive(false);
        }
        else
        {
            SkinShadowPanel.SetActive(true);
        }
    }

    private void ShowCoin()
    {
        if (Coin < 10)
        {
            CoinImage[2].color = new Vector4(1, 1, 1, 0);
            CoinImage[1].color = new Vector4(1, 1, 1, 0);
            CoinImage[0].color = new Vector4(1, 1, 1, 1);
        }
        else if (Coin < 100)
        {
            CoinImage[2].color = new Vector4(1, 1, 1, 0);
            CoinImage[1].color = new Vector4(1, 1, 1, 1);
            CoinImage[0].color = new Vector4(1, 1, 1, 1);
        }
        else if (Coin < 1000)
        {
            CoinImage[2].color = new Vector4(1, 1, 1, 1);
            CoinImage[1].color = new Vector4(1, 1, 1, 1);
            CoinImage[0].color = new Vector4(1, 1, 1, 1);
        }

        for (int i = 0; i < 10; i++)
        {
            if (Coin / 100 == i)
            {
                CoinImage[2].sprite = NumberImage[i];
            }
        }
        for (int i = 0; i < 10; i++)
        {
            Coin = Coin % 100;

            if (Coin / 10 == i)
            {
                CoinImage[1].sprite = NumberImage[i];
            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (Coin % 10 == i)
            {
                CoinImage[0].sprite = NumberImage[i];
            }
        }
    }
}
