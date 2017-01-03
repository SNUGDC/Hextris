using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject Base;

    private void OnMouseDown()
    {
        if (gameObject.name == "Right Arrow")
        {
            Base.GetComponent<BaseController>().RightArrowIsClicked = true;
        }

        if (gameObject.name == "Left Arrow")
        {
            Base.GetComponent<BaseController>().LeftArrowIsClicked = true;
        }

        if (gameObject.name == "Down Arrow")
        {
            Base.GetComponent<BaseController>().DownArrowIsClicked = true;
        }

        if (gameObject.name == "Create Block")
        {
            Base.GetComponent<BaseController>().CreateBlock = true;
        }
    }
}
