using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject ClockwiseRotateArrow;
    public GameObject CounterClockwiseRotateArrow;

    private GameObject Base;
    private Vector3 MouseStartPos;
    private Vector3 MouseDragPos;
    private Vector3 MouseFinishPos;
    private Vector3 SwipeVector;

    private void Start()
    {
        Base = GameObject.Find("Base");
        ClockwiseRotateArrow.SetActive (false);
        CounterClockwiseRotateArrow.SetActive (false);
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
        if (Mathf.Abs(SwipeVector.x) < 50f && Mathf.Abs(SwipeVector.y) < 50f)
        {
            return Command.CREATE;
        }
        if (Mathf.Abs(SwipeVector.x) >= Mathf.Abs(SwipeVector.y))
        {
            if (SwipeVector.x > 200f)
            {
                return Command.RIGHT;
            }
            if (SwipeVector.x < -200f)
            {
                return Command.LEFT;
            }
        }
        else
        {
            if (SwipeVector.y < -200f)
            {
                return Command.DOWN;
            }
        }
        return null;
    }

    private void OnMouseDrag()
    {
        MouseDragPos = Input.mousePosition;
        SwipeVector = MouseDragPos - MouseStartPos;

        Command? command = swipeToCommand(SwipeVector);
        if (command.HasValue)
        {
            switch (command.Value)
            {
            case Command.RIGHT:
                CounterClockwiseRotateArrow.SetActive (true);
                ClockwiseRotateArrow.SetActive (false);
                break;
            case Command.LEFT:
                ClockwiseRotateArrow.SetActive (true);
                CounterClockwiseRotateArrow.SetActive (false);
                break;
            case Command.DOWN:
                ClockwiseRotateArrow.SetActive (false);
                CounterClockwiseRotateArrow.SetActive (false);
                break;
            case Command.CREATE:
                ClockwiseRotateArrow.SetActive (false);
                CounterClockwiseRotateArrow.SetActive (false);
                break;
            default:
                Debug.Log("Something is Worng at Command");
                break;
            }
        }
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
                        Base.GetComponent<BaseController> ().RotateToRight = true;
                        CounterClockwiseRotateArrow.SetActive (false);
                        break;
                    case Command.LEFT:
                        Base.GetComponent<BaseController>().RotateToLeft = true;
                        ClockwiseRotateArrow.SetActive (false);
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
