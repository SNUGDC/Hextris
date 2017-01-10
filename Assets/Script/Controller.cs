using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private GameObject Base;
    private Vector3 MouseStartPos;
    private Vector3 MouseFinishPos;
    private Vector3 SwipeVector;

    private void Start()
    {
        Base = GameObject.Find("Base");
    }

    private void Update()
    {
    }

    private void OnMouseDown()
    {
        MouseStartPos = Input.mousePosition;
    }

    private enum Command
    {
        RIGHT,
        LEFT,
        DOWN,
        CREATE
    }

    private Command? swipeToCommand(Vector3 swipeVector)
    {
        if (Mathf.Abs(SwipeVector.x) < PlayerPrefs.GetFloat("Create Block Sensitivity") && Mathf.Abs(SwipeVector.y) < PlayerPrefs.GetFloat("Create Block Sensitivity"))
        {
            return Command.CREATE;
        }
        if (Mathf.Abs(SwipeVector.x) >= Mathf.Abs(SwipeVector.y))
        {
            if (SwipeVector.x > PlayerPrefs.GetFloat("Swipe Sensitivity"))
            {
                return Command.RIGHT;
            }
            if (SwipeVector.x < -PlayerPrefs.GetFloat("Swipe Sensitivity"))
            {
                return Command.LEFT;
            }
        }
        else
        {
            if (SwipeVector.y < -PlayerPrefs.GetFloat("Swipe Sensitivity"))
            {
                return Command.DOWN;
            }
        }
        return null;
    }

    private void OnMouseUp()
    {
        if (PlayerPrefs.GetString("In Game State") == "Play")
        {
            MouseFinishPos = Input.mousePosition;
            SwipeVector = MouseFinishPos - MouseStartPos;

            Command? command = swipeToCommand(SwipeVector);
            if (command.HasValue)
            {
                switch (command.Value)
                {
                    case Command.RIGHT:
                        Base.GetComponent<BaseController>().RotateToRight = true;
                        break;
                    case Command.LEFT:
                        Base.GetComponent<BaseController>().RotateToLeft = true;
                        break;
                    case Command.DOWN:
                        Base.GetComponent<BaseController>().MoveBlockDownward = true;
                        break;
                    case Command.CREATE:
                        Base.GetComponent<BaseController>().CreateBlock = true;
                        break;
                    default:
                        Debug.Log("Something is Worng at Command");
                        break;
                }
            }
        }
    }
}
