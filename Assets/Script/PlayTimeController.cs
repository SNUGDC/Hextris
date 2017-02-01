using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimeController : MonoBehaviour
{
    public Sprite[] Number;
    public Image[] TimeImage;

    private GameObject Base;
    private float GameTime;
    private int[] PlayTimeNumber;

    private void Start()
    {
        Base = GameObject.Find ("Base");

        PlayTimeNumber = new int[3];
    }

    private void Update()
    {
        GameTime = Base.GetComponent<BaseController> ().GameTime;


    }
}
