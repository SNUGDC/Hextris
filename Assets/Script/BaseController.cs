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
    public Sprite[] ColorTile;
    public GameObject[] Blocks;
    public GameObject[] ControlBlock;

    public GameObject CreatedBlocks;
    public GameObject[] Tile;
    public bool IsBlockCreated;

    private float GameTime;
    private float RotateStartTime;
    private float StartRotateAngle;
    private bool IsRotateRight;
    private bool IsRotateLeft;
    private int BlockColor;
    private int BlockNumber;

    private void Start()
    {
        GameTime = 0;
        RotateStartTime = 0;
        IsRotateRight = false;
        IsRotateLeft = false;
        IsBlockCreated = false;
    }

    private void Update()
    {
        GameTime = GameTime + Time.deltaTime;

        RotateRight();
        RotateLeft();
        CreateBlocks();

        if (Input.GetKeyDown (KeyCode.DownArrow) && IsRotateLeft == false && IsRotateRight == false)
        {
            if (CheckBeforeMove () == true)
                MoveBlocks ();
            else
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int i = 0; i < 169; i++)
                    {
                        if (Tile [i].transform.localPosition == ControlBlock [j].transform.localPosition)
                        {
                            Tile [i].GetComponent<SpriteRenderer> ().sprite = ColorTile[BlockColor];
                        }
                    }
                }
                IsBlockCreated = false;
                Destroy (CreatedBlocks);
            }
        }
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

    private void CreateBlocks()
    {
        if (Input.GetKeyUp(KeyCode.C) && IsRotateRight == false && IsRotateLeft == false && IsBlockCreated == false)
        {
            BlockColor = Random.Range(1, 4);
            BlockNumber = Random.Range(0, 8);
            
            Instantiate(Blocks[BlockNumber], this.transform);

            string ControlBlockName = Blocks[BlockNumber].name + "(Clone)";
            CreatedBlocks = GameObject.Find(ControlBlockName);

            for (int i = 0; i <= 2; i++)
            {
                ControlBlock[i] = CreatedBlocks.transform.GetChild(i).gameObject;
                ControlBlock[i].GetComponent<SpriteRenderer>().sprite = ColorTile[BlockColor];
                Debug.Log(BlockColor);
            }

            IsBlockCreated = true;
        }
    }

    private void MoveBlocks()
    {
        if (IsBlockCreated == true && IsRotateRight == false && IsRotateLeft == false)
        {
            ControlBlock[0].transform.localPosition = ControlBlock[0].transform.localPosition + GravityVector ();
            ControlBlock[1].transform.localPosition = ControlBlock[1].transform.localPosition + GravityVector ();
            ControlBlock[2].transform.localPosition = ControlBlock[2].transform.localPosition + GravityVector ();
        }
    }

    private Vector3 GravityVector()
    {
        int BaseAngle = (int)transform.eulerAngles.z;
        switch (BaseAngle)
        {
        case 0:
            return new Vector3 (0, -2.1f, 0);
        case 60:
            return new Vector3 (-1.83f, -1.05f, 0);
        case 120:
            return new Vector3 (-1.83f, 1.05f, 0);
        case 180:
            return new Vector3 (0, 2.1f, 0);
        case 240:
            return new Vector3 (1.83f, 1.05f, 0);
        case 300:
            return new Vector3 (1.83f, -1.05f, 0);
        default:
            Debug.Log ("Wrong Angle");
            return new Vector3 (0, 0, 0);
        }
    }

    private bool CheckBeforeMove()
    {
        bool FirstBlockCanMove = false;
        bool SecondBlockCanMove = false;
        bool ThirdBlockCanMove = false;

        for (int i = 0; i < 169; i++)
        {
            if (Tile [i].transform.localPosition == ControlBlock [0].transform.localPosition + GravityVector ())
            {
                if (Tile [i].GetComponent<SpriteRenderer> ().sprite.name == "Hex_Gray")
                {
                    FirstBlockCanMove = true;
                }
            }
            if (Tile [i].transform.localPosition == ControlBlock [1].transform.localPosition + GravityVector ())
            {
                if (Tile [i].GetComponent<SpriteRenderer> ().sprite.name == "Hex_Gray")
                {
                    SecondBlockCanMove = true;
                }
            }
            if (Tile [i].transform.localPosition == ControlBlock [2].transform.localPosition + GravityVector () && Tile [i].GetComponent<SpriteRenderer> ().sprite.name == "Hex_Gray")
            {
                if (Tile [i].GetComponent<SpriteRenderer> ().sprite.name == "Hex_Gray")
                {
                    ThirdBlockCanMove = true;
                }
            }
        }

        if (FirstBlockCanMove && SecondBlockCanMove && ThirdBlockCanMove)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}