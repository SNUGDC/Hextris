using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    public GameObject HowToPlayPanel;
    public GameObject HowToPlay1;
    public GameObject HowToPlay2;

    private void Start()
    {
        if (!(PlayerPrefs.HasKey("Skin Number")))
        {
            PlayerPrefs.SetInt("Skin Number", 0);
        }

        HowToPlay1.SetActive(true);
        HowToPlay2.SetActive(false);
        HowToPlayPanel.SetActive(false);
    }

    private void Update()
    { }

    public void Next()
    {
        HowToPlay1.SetActive(false);
        HowToPlay2.SetActive(true);
    }

    public void Previous()
    {
        HowToPlay1.SetActive(true);
        HowToPlay2.SetActive(false);
    }
}
