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
    public int SpecialBlock;
    public int Score;
    public float GameTime;

    private string ControlBlockName;
    private float RotateStartTime;
    private float StartRotateAngle;
    private float RotatingSpeed;
    private float ExplodeWaitingTime;
    private float ExplodeStartTime;
    private float ExplodeNowTime;
    private float AnimationTime;
    private bool IsExplodeStart = false;
    private bool GroundWorkFinish = false;
    private bool IsExplodeFinish;
    private bool IsRotateRight;
    private bool IsRotateLeft;
    private int BlockColor;
    private int BlockNumber;
    private int CreateBlockSwitcher;
    private int IsSection1Full;
    private int IsSection2Full;
    private int IsSection3Full;
    private int IsSection4Full;

    private void Start()
    {
        GameTime = 0;
        Score = 0;
        RotatingSpeed = 0.5f;
        RotateStartTime = 0;
        SpecialBlockNumber = 0;
        ExplodeWaitingTime = 1.5f;
        IsRotateRight = false;
        IsRotateLeft = false;
        IsBlockCreated = false;
        CreateBlockSwitcher = 0;
        AnimationTime = 0;

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

        if (PlayerPrefs.GetString ("In Game State") == "Pause")
        {

            if (IsSection4Full == 0)
            {
                if (IsExplodeStart == true && IsExplodeFinish == false)
                {
                    ExplodeNowTime = ExplodeNowTime + Time.deltaTime;

                    if (ExplodeNowTime - ExplodeStartTime >= ExplodeWaitingTime)
                    {
                        IsExplodeFinish = true;
                        IsExplodeStart = false;
                    }
                }
                else if (IsExplodeStart == false && IsExplodeFinish == true && GroundWorkFinish == false)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        Destroy (Tile [i + 37].GetComponent<Rigidbody2D> ());
                        Tile [i + 37].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
                    }

                    Section4SpriteAssignBeforeSpread ();
                    Section3SpriteAssignBeforeSpread ();
                    Section2SpriteAssignBeforeSpread ();
                    Section1SpriteAssignBeforeSpread ();

                    AnimationTime = 0;
                    GroundWorkFinish = true;
                }
                else if(IsExplodeStart == false && IsExplodeFinish == true && GroundWorkFinish == true)
                {
                    GetComponent<Animation> ().Play ("Base Spread From Section4");

                    if (AnimationTime < 4f)
                    {
                        AnimationTime = AnimationTime + Time.deltaTime;
                    }
                    else
                    {
                        PlayerPrefs.SetString ("In Game State", "Play");
                        IsExplodeStart = false;
                        AnimationTime = 0;
                        GroundWorkFinish = false;
                    }
                }
            }

            else if (IsSection3Full == 0)
            {
                if (IsExplodeStart == false && IsExplodeFinish == false)
                {
                    ExplodeStartTime = GameTime;
                    ExplodeNowTime = GameTime;
                    IsExplodeStart = true;
                    IsExplodeFinish = false;
                }
                else if (IsExplodeStart == true && IsExplodeFinish == false)
                {
                    ExplodeNowTime = ExplodeNowTime + Time.deltaTime;
                    if (ExplodeNowTime - ExplodeStartTime >= ExplodeWaitingTime)
                    {
                        IsExplodeFinish = true;
                        IsExplodeStart = false;
                    }
                }
                else if (IsExplodeFinish == true && IsExplodeStart == false && GroundWorkFinish == false)
                {
                    for (int i = 0; i < 18; i++)
                    {
                        Destroy (Tile [i + 19].GetComponent<Rigidbody2D> ());
                        Tile [i + 19].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
                    }

                    Section3SpriteAssignBeforeSpread ();
                    Section2SpriteAssignBeforeSpread ();
                    Section1SpriteAssignBeforeSpread ();

                    AnimationTime = 0;
                    GroundWorkFinish = true;
                }
                else if(IsExplodeStart == false && IsExplodeFinish == true && GroundWorkFinish == true)
                {
                    GetComponent<Animation> ().Play ("Base Spread From Section3");

                    if (AnimationTime < 3f)
                    {
                        AnimationTime = AnimationTime + Time.deltaTime;
                    }
                    else
                    {
                        PlayerPrefs.SetString ("In Game State", "Play");
                        IsExplodeStart = false;
                        AnimationTime = 0;
                        GroundWorkFinish = false;
                    }
                }
            }

            else if (IsSection2Full == 0)
            {
                if (IsExplodeStart == false && IsExplodeFinish == false)
                {
                    ExplodeStartTime = GameTime;
                    ExplodeNowTime = GameTime;
                    IsExplodeStart = true;
                    IsExplodeFinish = false;
                }
                else if (IsExplodeStart == true && IsExplodeFinish == false)
                {
                    ExplodeNowTime = ExplodeNowTime + Time.deltaTime;
                    if (ExplodeNowTime - ExplodeStartTime >= ExplodeWaitingTime)
                    {
                        IsExplodeFinish = true;
                        IsExplodeStart = false;
                    }
                }
                else if (IsExplodeFinish == true && IsExplodeStart == false && GroundWorkFinish == false)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Destroy (Tile [i + 7].GetComponent<Rigidbody2D> ());
                        Tile [i + 7].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
                    }

                    Section2SpriteAssignBeforeSpread ();
                    Section1SpriteAssignBeforeSpread ();

                    AnimationTime = 0;
                    GroundWorkFinish = true;
                }
                else if(IsExplodeStart == false && IsExplodeFinish == true && GroundWorkFinish == true)
                {
                    GetComponent<Animation> ().Play ("Base Spread From Section2");

                    if (AnimationTime < 2f)
                    {
                        AnimationTime = AnimationTime + Time.deltaTime;
                    }
                    else
                    {
                        PlayerPrefs.SetString ("In Game State", "Play");
                        IsExplodeStart = false;
                        AnimationTime = 0;
                        GroundWorkFinish = false;
                    }
                }
            }

            else if (IsSection1Full == 0)
            {
                if (IsExplodeStart == false && IsExplodeFinish == false)
                {
                    ExplodeStartTime = GameTime;
                    ExplodeNowTime = GameTime;
                    IsExplodeStart = true;
                    IsExplodeFinish = false;
                }
                else if (IsExplodeStart == true && IsExplodeFinish == false)
                {
                    ExplodeNowTime = ExplodeNowTime + Time.deltaTime;
                    if (ExplodeNowTime - ExplodeStartTime >= ExplodeWaitingTime)
                    {
                        IsExplodeFinish = true;
                        IsExplodeStart = false;
                    }
                }
                else if (IsExplodeFinish == true && IsExplodeStart == false && GroundWorkFinish == false)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        Destroy (Tile [i + 1].GetComponent<Rigidbody2D> ());
                        Tile [i + 1].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
                    }

                    Section1SpriteAssignBeforeSpread ();

                    AnimationTime = 0;
                    GroundWorkFinish = true;
                }
                else if(IsExplodeStart == false && IsExplodeFinish == true && GroundWorkFinish == true)
                {
                    GetComponent<Animation> ().Play ("Base Spread From Section1");

                    if (AnimationTime < 1f)
                    {
                        AnimationTime = AnimationTime + Time.deltaTime;
                    }
                    else
                    {
                        PlayerPrefs.SetString ("In Game State", "Play");
                        IsExplodeStart = false;
                        AnimationTime = 0;
                        GroundWorkFinish = false;
                    }
                }
            }
        }
    }

    private void GroundworkBeforeSpread(int MovingTileNumber, int MovedTileNumber)
    {
        Tile[MovingTileNumber].GetComponent<SpriteRenderer>().sprite = Tile[MovedTileNumber].GetComponent<SpriteRenderer>().sprite;
    }

    private void Section4SpriteAssignBeforeSpread()
    {
        GroundworkBeforeSpread(37, 19);
        GroundworkBeforeSpread(38, 20);
        GroundworkBeforeSpread(40, 21);
        GroundworkBeforeSpread(41, 22);
        GroundworkBeforeSpread(42, 23);
        GroundworkBeforeSpread(44, 24);
        GroundworkBeforeSpread(45, 25);
        GroundworkBeforeSpread(46, 26);
        GroundworkBeforeSpread(48, 27);
        GroundworkBeforeSpread(49, 28);
        GroundworkBeforeSpread(50, 29);
        GroundworkBeforeSpread(52, 30);
        GroundworkBeforeSpread(53, 31);
        GroundworkBeforeSpread(54, 32);
        GroundworkBeforeSpread(56, 33);
        GroundworkBeforeSpread(57, 34);
        GroundworkBeforeSpread(58, 35);
        GroundworkBeforeSpread(60, 36);
    }

    private void Section3SpriteAssignBeforeSpread()
    {
        GroundworkBeforeSpread (19, 7);
        GroundworkBeforeSpread (20, 8);
        GroundworkBeforeSpread (21, 8);
        GroundworkBeforeSpread (22, 9);
        GroundworkBeforeSpread (23, 10);
        GroundworkBeforeSpread (24, 10);
        GroundworkBeforeSpread (25, 11);
        GroundworkBeforeSpread (26, 12);
        GroundworkBeforeSpread (27, 12);
        GroundworkBeforeSpread (28, 13);
        GroundworkBeforeSpread (29, 14);
        GroundworkBeforeSpread (30, 14);
        GroundworkBeforeSpread (31, 15);
        GroundworkBeforeSpread (32, 16);
        GroundworkBeforeSpread (33, 16);
        GroundworkBeforeSpread (34, 17);
        GroundworkBeforeSpread (35, 18);
        GroundworkBeforeSpread (36, 18);
    }

    private void Section2SpriteAssignBeforeSpread()
    {
        GroundworkBeforeSpread (7, 1);
        GroundworkBeforeSpread (9, 2);
        GroundworkBeforeSpread (11, 3);
        GroundworkBeforeSpread (13, 4);
        GroundworkBeforeSpread (15, 5);
        GroundworkBeforeSpread (17, 6);
    }

    private void Section1SpriteAssignBeforeSpread()
    {
        GroundworkBeforeSpread (1, 0);
        GroundworkBeforeSpread (2, 0);
        GroundworkBeforeSpread (3, 0);
        GroundworkBeforeSpread (4, 0);
        GroundworkBeforeSpread (5, 0);
        GroundworkBeforeSpread (6, 0);
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
                BlockColor = (3 * PlayerPrefs.GetInt("Skin Number")) + 1;
                break;
            case 1:
                BlockColor = (3 * PlayerPrefs.GetInt("Skin Number")) + 2;
                break;
            case 2:
                BlockColor = (3 * PlayerPrefs.GetInt("Skin Number")) + 3;
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
            ControlBlock[i].GetComponent<SpriteRenderer>().sprite = ColorTile[BlockColor];  
        }

        CreateBlocksOrder();
        IsBlockCreated = true;
        CreateBlock = false;
        MoveBlockDownward = false;
    }

    private void CheckToCanCreateBlock()
    {
        int BaseAngle = (int)transform.eulerAngles.z / 60;

        switch (BaseAngle)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
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
                    for (int i = 0; i < Tile.Length; i++)
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
                for (int i = 0; i < Tile.Length; i++)
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
        DestroyCreatedBlock();
        CheckToClearBlock();
        DestroyAndMoveBlocks();
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

        for (int i = 0; i < Tile.Length; i++)
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
            for (int i = 0; i < Tile.Length; i++)
            {
                if (Tile [i].transform.localPosition == ControlBlock [j].transform.localPosition)
                {
                    Tile [i].GetComponent<SpriteRenderer> ().sprite = ColorTile[BlockColor];
                }
            }
        }
    }

    private void DestroyCreatedBlock()
    {
        Destroy (CreatedBlocks);
    }

    private void CheckToClearBlock()
    {
        IsSection1Full = 6;
        IsSection2Full = 12;
        IsSection3Full = 18;
        IsSection4Full = 24;

        for (int i = 0; i < 24; i++)
        {
            if (Section4[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection4Full = IsSection4Full - 1;
            }
        }

        for (int i = 0; i < 18; i++)
        {
            if (Section3[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection3Full = IsSection3Full - 1;
            }
        }

        for (int i = 0; i < 12; i++)
        {
            if (Section2[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection2Full = IsSection2Full - 1;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (Section1[i].GetComponent<SpriteRenderer>().sprite.name != "Hex_Gray")
            {
                IsSection1Full = IsSection1Full - 1;
            }
        }
    }

    private void DestroyAndMoveBlocks()
    {
        ExplodeBlocks ();
    }

    private void ExplodeBlocks()
    {
        float XVelocity;
        float YVelocity;
        Rigidbody2D TileBody;

        if (IsSection4Full == 0)
        {
            for (int i = 0; i < 24; i++)
            {
                Tile [i + 37].AddComponent<Rigidbody2D> ();
                TileBody = Tile [i + 37].GetComponent<Rigidbody2D> ();

                XVelocity = Random.Range (-10.0f, 10.0f);
                YVelocity = Random.Range (-10.0f, 10.0f);
                TileBody.gravityScale = 3;

                TileBody.velocity = new Vector2 (XVelocity, YVelocity);
            }

            ExplodeStartTime = GameTime;
            ExplodeNowTime = GameTime;
            IsExplodeStart = true;
            IsExplodeFinish = false;

            PlayerPrefs.SetString ("In Game State", "Pause");
        }

        if (IsSection3Full == 0)
        {
            for (int i = 0; i < 18; i++)
            {
                Tile [i + 19].AddComponent<Rigidbody2D> ();
                TileBody = Tile [i + 19].GetComponent<Rigidbody2D> ();

                XVelocity = Random.Range (-10.0f, 10.0f);
                YVelocity = Random.Range (-10.0f, 10.0f);
                TileBody.gravityScale = 3;

                TileBody.velocity = new Vector2 (XVelocity, YVelocity);
            }

            ExplodeStartTime = GameTime;
            ExplodeNowTime = GameTime;
            IsExplodeStart = true;
            IsExplodeFinish = false;

            PlayerPrefs.SetString("In Game State", "Pause");
        }

        if (IsSection2Full == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                Tile [i + 7].AddComponent<Rigidbody2D> ();
                TileBody = Tile [i + 7].GetComponent<Rigidbody2D> ();

                XVelocity = Random.Range (-10.0f, 10.0f);
                YVelocity = Random.Range (-10.0f, 10.0f);
                TileBody.gravityScale = 3;

                TileBody.velocity = new Vector2 (XVelocity, YVelocity);
            }

            ExplodeStartTime = GameTime;
            ExplodeNowTime = GameTime;
            IsExplodeStart = true;
            IsExplodeFinish = false;

            PlayerPrefs.SetString("In Game State", "Pause");
        }

        if (IsSection1Full == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                Tile [i + 1].AddComponent<Rigidbody2D> ();
                TileBody = Tile [i + 1].GetComponent<Rigidbody2D> ();

                XVelocity = Random.Range (-10.0f, 10.0f);
                YVelocity = Random.Range (-10.0f, 10.0f);
                TileBody.gravityScale = 3;

                TileBody.velocity = new Vector2 (XVelocity, YVelocity);
            }

            ExplodeStartTime = GameTime;
            ExplodeNowTime = GameTime;
            IsExplodeStart = true;
            IsExplodeFinish = false;

            PlayerPrefs.SetString("In Game State", "Pause");
        }
    }
}