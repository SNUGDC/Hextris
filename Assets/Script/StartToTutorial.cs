using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartToTutorial : MonoBehaviour
{
    public Image YesButtonImage;
    public Image NoButtonImage;
    public Image NeverButtonImage;

    private void Start()
    {
        if (PlayerPrefs.GetString("Tutorial Panel") == "Off")
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {

    }

    public void PushYesButton()
    {
        YesButtonImage.color = new Color(1, 1, 1, 1);
    }

    public void PushNoButton()
    {
        NoButtonImage.color = new Color(1, 1, 1, 1);
        gameObject.SetActive(false);
    }

    public void PushNeverButton()
    {
        NeverButtonImage.color = new Color(1, 1, 1, 1);
        PlayerPrefs.SetString("Tutorial Panel", "Off");
    }
}
