using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayTimeController : MonoBehaviour
{
    public Sprite[] Number;
    public Image[] TimeImage;

    private GameObject Base;
    private float GameTime_float;
    private int GameTime_int;
    private int[] PlayTimeNumber;

    private void Start()
    {
        Base = GameObject.Find ("Base");

        PlayTimeNumber = new int[3];
    }

    private void Update()
    {
        GameTime_float = Base.GetComponent<BaseController> ().GameTime;

        GameTime_int = (int)GameTime_float;
    }
}
