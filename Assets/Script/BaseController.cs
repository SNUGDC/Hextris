using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public GameObject[] Section0;
    public GameObject[] Section1;
    public GameObject[] Section2;
    public GameObject[] Section3;
    public GameObject[] Section4;
    public GameObject[] Section5;
    public GameObject[] Section6;
    public GameObject[] Section7;

    private float GameTime;
    private float RotateStartTime;
    private float StartRotateAngle;
    private bool IsRotateRight;
    private bool IsRotateLeft;

    private void Start()
    {
        GameTime = 0;
        RotateStartTime = 0;
        IsRotateRight = false;
        IsRotateLeft = false;
    }

    private void Update()
    {
        GameTime = GameTime + Time.deltaTime;

        RotateRight();
        RotateLeft();
    }

    private void RotateRight()
    {
        if (Input.GetKeyUp(KeyCode.RightArrow) && (IsRotateRight == false) && (IsRotateLeft == false))
        {
            RotateStartTime = GameTime;
            IsRotateRight = true;
            StartRotateAngle = transform.eulerAngles.z;
        }

        if (IsRotateRight == true)
        {
            if ((GameTime - RotateStartTime) <= 1f)
            {
                transform.eulerAngles = new Vector3(0, 0, StartRotateAngle + (60f * (GameTime - RotateStartTime)));
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, StartRotateAngle + 60f);
                IsRotateRight = false;
            }
        }
    }

    private void RotateLeft()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow) && (IsRotateLeft == false) && (IsRotateRight == false))
        {
            RotateStartTime = GameTime;
            IsRotateLeft = true;
            StartRotateAngle = transform.eulerAngles.z;
        }

        if (IsRotateLeft == true)
        {
            if ((GameTime - RotateStartTime) <= 1f)
            {
                transform.eulerAngles = new Vector3(0, 0, StartRotateAngle - (60f * (GameTime - RotateStartTime)));
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, StartRotateAngle - 60f);
                IsRotateLeft = false;
            }
        }
    }
}
