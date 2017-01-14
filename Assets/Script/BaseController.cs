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
    public bool RotateToRight;
    public bool RotateToLeft;
    public bool MoveBlockDownward;
    public bool CreateBlock;
    public int[] BlockOrder;
    public int SpecialBlockNumber;

    private string ControlBlockName;
    private float GameTime;
    private float RotateStartTime;
    private float StartRotateAngle;
    private float RotatingSpeed;
    private bool IsRotateRight;
    private bool IsRotateLeft;
    private int BlockColor;
    private int BlockNumber;
    private int CreateBlockSwitcher;
    private int SpecialBlock;
    private int IsSection3Full = 1;
    private int IsSection4Full = 1;
    private int IsSection5Full = 1;
    private int IsSection6Full = 1;
    private int IsSection7Full = 1;

    private void Start()
    {
        GameTime = 0;
        RotatingSpeed = 0.5f;
        RotateStartTime = 0;
        SpecialBlockNumber = 0;
        IsRotateRight = false;
        IsRotateLeft = false;
        IsBlockCreated = false;
        CreateBlockSwitcher = 0;
        PlayerPrefs.SetString("In Game State", "Play");

        BlockOrder [0] = Random.Range (0, 3);
        switch (BlockOrder [0])
        {
        case 0:
            BlockOrder [1] = Random.Range (0, 2);
            if (BlockOrder [1] == 0)
            {
                BlockOrder [1] = 1;
                BlockOrder [2] = 2;
            }
            else
            {
                BlockOrder [1] = 2;
                BlockOrder [2] = 1;
            }
            break;
        case 1:
            BlockOrder [1] = Random.Range (0, 2);
            if (BlockOrder [1] == 0)
            {
                BlockOrder [1] = 0;
                BlockOrder [2] = 2;
            }
            else
            {
                BlockOrder [1] = 2;
                BlockOrder [2] = 0;
            }
            break;
        case 2:
            BlockOrder [1] = Random.Range (0, 2);
            if (BlockOrder [1] == 0)
            {
                BlockOrder [1] = 0;
                BlockOrder [2] = 1;
            }
            else
            {
                BlockOrder [1] = 1;
                BlockOrder [2] = 0;
            }
            break;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.GetString("In Game State") == "Play")
        {
            GameTime = GameTime + Time.deltaTime;

            RotateRight();
            RotateLeft();

            if (IsRotateLeft == false && IsRotateRight == false)
            {
                if (IsBlockCreated == true)
                {
                    if (CheckBeforeMove() == true)
                    {
                        if (Input.GetKeyDown(KeyCode.DownArrow) || MoveBlockDownward)
                        {
                            MoveBlocks();
                        }
                        else if (SpecialBlockNumber != 0)
                        {
                            CreateSpecialBlock();
                        }
                    }
                    else if (CheckBeforeMove() == false)
                    {
                        if (SpecialBlock == 0)
                        {
                            if (Input.GetKeyUp(KeyCode.C) || CreateBlock)
                            {
                                Coloring();
                                CreateBlocks();
                            }
                            else if (Input.GetKeyDown(KeyCode.DownArrow) || MoveBlockDownward)
                            {
                                Coloring();
                            }
                            else if (SpecialBlockNumber != 0)
                            {
                                CreateSpecialBlock();
                            }
                        }
                        else if (SpecialBlock != 0)
                        {
                            if (Input.GetKeyUp(KeyCode.C) || CreateBlock)
                            {
                                SpecialBlockEffect();
                                CreateBlocks();
                            }
                            else if (Input.GetKeyDown(KeyCode.DownArrow) || MoveBlockDownward)
                            {
                                SpecialBlockEffect();
                            }
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.C) || CreateBlock)
                    {
                        CreateBlocks();
                    }
                }
            }
        }
    }

    private void RotateRight()
    {
        if ((Input.GetKeyUp(KeyCode.RightArrow) || RotateToRight) && (IsRotateRight == false) && (IsRotateLeft == false))
        {
            RotateStartTime = GameTime;
            IsRotateRight = true;
            StartRotateAngle = transform.eulerAngles.z;
            RotateToRight = false;
        }

        if (IsRotateRight == true)
        {
            if ((GameTime - RotateStartTime) <= RotatingSpeed)
            {
                transform.eulerAngles = new Vector3(0, 0, StartRotateAngle + (60f * (GameTime - RotateStartTime) / RotatingSpeed));
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
        if ((Input.GetKeyUp(KeyCode.LeftArrow) || RotateToLeft) && (IsRotateLeft == false) && (IsRotateRight == false))
        {
            RotateStartTime = GameTime;
            IsRotateLeft = true;
            StartRotateAngle = transform.eulerAngles.z;
            RotateToLeft = false;
        }

        if (IsRotateLeft == true)
        {
            if ((GameTime - RotateStartTime) <= RotatingSpeed)
            {
                transform.eulerAngles = new Vector3(0, 0, StartRotateAngle - (60f * (GameTime - RotateStartTime) / RotatingSpeed));
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, StartRotateAngle - 60f);
                IsRotateLeft = false;
            }
        }
    }

    private void CreateBlocksOrder()
    {
        BlockOrder = new int[3] { BlockOrder [1], BlockOrder [2], 3 };

        switch (CreateBlockSwitcher)
        {
        case 0:
            BlockOrder [2] = Random.Range (0, 3);
            CreateBlockSwitcher = 1;
            break;
        case 1:
            switch (BlockOrder [1])
            {
            case 0:
                BlockOrder [2] = Random.Range (0, 2);
                if (BlockOrder [2] == 0)
                {
                    BlockOrder [2] = 1;
                    CreateBlockSwitcher = 4;
                }
                else
                {
                    BlockOrder [2] = 2;
                    CreateBlockSwitcher = 3;
                }
                break;
            case 1:
                BlockOrder [2] = Random.Range (0, 2);
                if (BlockOrder [2] == 0)
                {
                    BlockOrder [2] = 0;
                    CreateBlockSwitcher = 4;
                }
                else
                {
                    BlockOrder [2] = 2;
                    CreateBlockSwitcher = 2;
                }
                break;
            case 2:
                BlockOrder [2] = Random.Range (0, 2);
                if (BlockOrder [2] == 0)
                {
                    BlockOrder [2] = 0;
                    CreateBlockSwitcher = 3;
                }
                else
                {
                    BlockOrder [2] = 1;
                    CreateBlockSwitcher = 2;
                }
                break;
            }
            break;
        case 2:
            BlockOrder [2] = 0;
            CreateBlockSwitcher = 0;
            break;
        case 3:
            BlockOrder [2] = 1;
            CreateBlockSwitcher = 0;
            break;
        case 4:
            BlockOrder [2] = 2;
            CreateBlockSwitcher = 0;
            break;
        }
    }

    private void CreateBlocks()
    {
        SpecialBlock = 0;

        BlockNumber = BlockOrder[0];

        switch (BlockNumber)
        {
            case 0:
                BlockColor = 1;
                break;
            case 1:
                BlockColor = 3;
                break;
            case 2:
                BlockColor = 4;
                break;
            default:
                Debug.Log("Something Worng At Deciding Block Color");
                break;
        }

        BlockNumber = BlockNumber + 3 * ((int)transform.eulerAngles.z / 60);
        Instantiate(Blocks[BlockNumber], this.transform, false);

        ControlBlockName = Blocks[BlockNumber].name + "(Clone)";
        CreatedBlocks = GameObject.Find(ControlBlockName);

        for (int i = 0; i <= 2; i++)
        {
            ControlBlock[i] = CreatedBlocks.transform.GetChild(i).gameObject;
        }

        CreateBlocksOrder();
        IsBlockCreated = true;
        CreateBlock = false;
        MoveBlockDownward = false;
    }

    private void CreateSpecialBlock()
    {
        Destroy(CreatedBlocks);

        switch (SpecialBlockNumber)
        {
            case 1:
                Instantiate(Blocks[18], this.transform, false);
                ControlBlockName = "100t(Clone)";
                CreatedBlocks = GameObject.Find(ControlBlockName);
                SpecialBlock = 1;
                break;
            case 2:
                Instantiate(Blocks[19], this.transform, false);
                ControlBlockName = "Bomb(Clone)";
                CreatedBlocks = GameObject.Find(ControlBlockName);
                SpecialBlock = 2;
                break;
            default:
                break;
        }

        for (int i = 0; i <= 2; i++)
        {
            ControlBlock[i] = CreatedBlocks.transform.GetChild(i).gameObject;
        }

        SpecialBlockNumber = 0;
    }

    private void SpecialBlockEffect()
    {
        switch (SpecialBlock)
        {
            case 1:
                bool DeletedAllBlock = false;
                int j = 1;

                while (DeletedAllBlock == false)
                {
                    for (int i = 0; i < 169; i++)
                    {
                        if (Tile[i].transform.localPosition == ControlBlock[0].transform.localPosition + j * GravityVector())
                        {
                            Tile[i].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                            DeletedAllBlock = false;
                            j++;
                        }
                        else DeletedAllBlock = true;
                    }
                }
                break;
            case 2:
                for (int i = 0; i < 169; i++)
                {
                    if (Tile[i].transform.localPosition == ControlBlock[0].transform.localPosition + new Vector3(0, -2.1f, 0))
                    {
                        Tile[i].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                    }
                    if (Tile[i].transform.localPosition == ControlBlock[0].transform.localPosition + new Vector3(0, 2.1f, 0))
                    {
                        Tile[i].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                    }
                    if (Tile[i].transform.localPosition == ControlBlock[0].transform.localPosition + new Vector3(-1.83f, -1.05f, 0))
                    {
                        Tile[i].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                    }
                    if (Tile[i].transform.localPosition == ControlBlock[0].transform.localPosition + new Vector3(-1.83f, 1.05f, 0))
                    {
                        Tile[i].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                    }
                    if (Tile[i].transform.localPosition == ControlBlock[0].transform.localPosition + new Vector3(1.83f, -1.05f, 0))
                    {
                        Tile[i].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                    }
                    if (Tile[i].transform.localPosition == ControlBlock[0].transform.localPosition + new Vector3(1.83f, 1.05f, 0))
                    {
                        Tile[i].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                    }
                }
                break;
            default:
                Debug.Log("Something is Wrong at Special Block");
                break;
        }

        Destroy(CreatedBlocks);
        IsBlockCreated = false;
    }

    private void Coloring()
    {
        TileColoring();
        DestroyBlock();
        CheckToClearBlock();
        IsBlockCreated = false;
    }

    private void MoveBlocks()
    {
        if (IsBlockCreated == true && IsRotateRight == false && IsRotateLeft == false)
        {
            ControlBlock[0].transform.localPosition = ControlBlock[0].transform.localPosition + GravityVector ();
            ControlBlock[1].transform.localPosition = ControlBlock[1].transform.localPosition + GravityVector ();
            ControlBlock[2].transform.localPosition = ControlBlock[2].transform.localPosition + GravityVector ();
        }

        CreateBlock = false;
        MoveBlockDownward = false;
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

    private void TileColoring()
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
    }

    private void DestroyBlock()
    {
        Destroy (CreatedBlocks);
    }

    private void CheckToClearBlock()
    {
        for (int i = 0; i < 42; i++)
        {
            if (Section7[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection7Full = IsSection7Full * 1;
            }
            else
            {
                IsSection7Full = IsSection7Full * 0;
            }
        }

        for (int i = 0; i < 36; i++)
        {
            if (Section6[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection6Full = IsSection6Full * 1;
            }
            else
            {
                IsSection6Full = IsSection6Full * 0;
            }
        }

        for (int i = 0; i < 30; i++)
        {
            if (Section5[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection5Full = IsSection5Full * 1;
            }
            else
            {
                IsSection5Full = IsSection5Full * 0;
            }
        }

        for (int i = 0; i < 24; i++)
        {
            if (Section4[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection4Full = IsSection4Full * 1;
            }
            else
            {
                IsSection4Full = IsSection4Full * 0;
            }
        }

        for (int i = 0; i < 18; i++)
        {
            if (Section3[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection3Full = IsSection3Full * 1;
            }
            else
            {
                IsSection3Full = IsSection3Full * 0;
            }
        }
    }
}