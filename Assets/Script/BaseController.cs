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
    public float GameTime;

    private string ControlBlockName;
    private float RotateStartTime;
    private float StartRotateAngle;
    private float RotatingSpeed;
    private bool IsRotateRight;
    private bool IsRotateLeft;
    private bool SpreadFinish = false;
    private int BlockColor;
    private int BlockNumber;
    private int CreateBlockSwitcher;
    private int SpecialBlock;
    private int IsSection1Full;
    private int IsSection2Full;
    private int IsSection3Full;
    private int IsSection4Full;

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

        if (PlayerPrefs.GetString ("In Game State") == "Pause")
        {
            if (IsSection4Full == 0)
            {
                for (int i = 0; i < 24; i++)
                {
                    if (SpreadFinish == false
                        && Mathf.Abs (Tile [i + 37].transform.position.x) > 30f
                        && Mathf.Abs (Tile [i + 37].transform.position.y) > 30f)
                    {
                        SpreadFinish = true;
                    }
                    else if (SpreadFinish == true)
                    {
                        Tile[i + 37].GetComponent<Rigidbody2D>().Sleep();
                        Tile[i + 37].GetComponent<SpriteRenderer>().sprite = ColorTile[0];
                    }
                }
                GroundworkBeforeExpand(37, 19);
                GroundworkBeforeExpand(38, 20);
                GroundworkBeforeExpand(41, 21);
                GroundworkBeforeExpand(42, 22);
                GroundworkBeforeExpand(44, 23);
                GroundworkBeforeExpand(45, 24);
                GroundworkBeforeExpand(46, 25);
                GroundworkBeforeExpand(48, 26);
                GroundworkBeforeExpand(49, 27);
                GroundworkBeforeExpand(50, 28);
                GroundworkBeforeExpand(51, 29);
                GroundworkBeforeExpand(52, 30);
                GroundworkBeforeExpand(53, 31);
                GroundworkBeforeExpand(54, 32);
                GroundworkBeforeExpand(56, 33);
                GroundworkBeforeExpand(57, 34);
                GroundworkBeforeExpand(58, 35);
                GroundworkBeforeExpand(60, 36);
            }
        }
    }

    private void GroundworkBeforeExpand(int MovingTileNumber, int MovedTileNumber)
    {
        Tile[MovingTileNumber].transform.localPosition = Tile[MovedTileNumber].transform.localPosition;
        Tile[MovingTileNumber].GetComponent<SpriteRenderer>().sprite = Tile[MovedTileNumber].GetComponent<SpriteRenderer>().sprite;
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
        //SpreadBlocks ();
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

            PlayerPrefs.SetString ("In Game State", "Pause");
        }

        if (IsSection3Full == 0)
        {
            for (int i = 0; i < 18; i++)
            {
                Tile [i + 37].AddComponent<Rigidbody2D> ();
                TileBody = Tile [i + 37].GetComponent<Rigidbody2D> ();

                XVelocity = Random.Range (-10.0f, 10.0f);
                YVelocity = Random.Range (-10.0f, 10.0f);
                TileBody.gravityScale = 3;

                TileBody.velocity = new Vector2 (XVelocity, YVelocity);
            }
        }

        if (IsSection2Full == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                Tile [i + 37].AddComponent<Rigidbody2D> ();
                TileBody = Tile [i + 37].GetComponent<Rigidbody2D> ();

                XVelocity = Random.Range (-10.0f, 10.0f);
                YVelocity = Random.Range (-10.0f, 10.0f);
                TileBody.gravityScale = 3;

                TileBody.velocity = new Vector2 (XVelocity, YVelocity);
            }
        }

        if (IsSection1Full == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                Tile [i + 37].AddComponent<Rigidbody2D> ();
                TileBody = Tile [i + 37].GetComponent<Rigidbody2D> ();

                XVelocity = Random.Range (-10.0f, 10.0f);
                YVelocity = Random.Range (-10.0f, 10.0f);
                TileBody.gravityScale = 3;

                TileBody.velocity = new Vector2 (XVelocity, YVelocity);
            }
        }
    }

    private void SpreadBlocks()
    {
        if (IsSection4Full == 0)
        {
            for (int i = 0; i < 24; i++)
            {
                Section4 [i].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
            }

            MoveBlockAtoB (19, 37);
            MoveBlockAtoB (20, 38);
            MoveBlockAtoB (21, 40);
            MoveBlockAtoB (22, 41);
            MoveBlockAtoB (23, 42);
            MoveBlockAtoB (24, 44);
            MoveBlockAtoB (25, 45);
            MoveBlockAtoB (26, 46);
            MoveBlockAtoB (27, 48);
            MoveBlockAtoB (28, 49);
            MoveBlockAtoB (29, 50);
            MoveBlockAtoB (30, 52);
            MoveBlockAtoB (31, 53);
            MoveBlockAtoB (32, 54);
            MoveBlockAtoB (33, 56);
            MoveBlockAtoB (34, 57);
            MoveBlockAtoB (35, 58);
            MoveBlockAtoB (36, 60);

            MoveBlockAtoB (7, 19);
            MoveBlockAtoB (8, 20);
            MoveBlockAtoB (8, 21);
            MoveBlockAtoB (9, 22);
            MoveBlockAtoB (10, 23);
            MoveBlockAtoB (10, 24);
            MoveBlockAtoB (11, 25);
            MoveBlockAtoB (12, 26);
            MoveBlockAtoB (12, 27);
            MoveBlockAtoB (13, 28);
            MoveBlockAtoB (14, 29);
            MoveBlockAtoB (14, 30);
            MoveBlockAtoB (15, 31);
            MoveBlockAtoB (16, 32);
            MoveBlockAtoB (16, 33);
            MoveBlockAtoB (17, 34);
            MoveBlockAtoB (18, 35);
            MoveBlockAtoB (18, 36);

            MoveBlockAtoB (1, 7);
            MoveBlockAtoB (2, 9);
            MoveBlockAtoB (3, 11);
            MoveBlockAtoB (4, 13);
            MoveBlockAtoB (5, 15);
            MoveBlockAtoB (6, 17);

            MoveBlockAtoB (0, 1);
            MoveBlockAtoB (0, 2);
            MoveBlockAtoB (0, 3);
            MoveBlockAtoB (0, 4);
            MoveBlockAtoB (0, 5);
            MoveBlockAtoB (0, 6);
        }

        if (IsSection3Full == 0)
        {
            for (int i = 0; i < 18; i++)
            {
                Section3 [i].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
            }

            MoveBlockAtoB (7, 19);
            MoveBlockAtoB (8, 20);
            MoveBlockAtoB (8, 21);
            MoveBlockAtoB (9, 22);
            MoveBlockAtoB (10, 23);
            MoveBlockAtoB (10, 24);
            MoveBlockAtoB (11, 25);
            MoveBlockAtoB (12, 26);
            MoveBlockAtoB (12, 27);
            MoveBlockAtoB (13, 28);
            MoveBlockAtoB (14, 29);
            MoveBlockAtoB (14, 30);
            MoveBlockAtoB (15, 31);
            MoveBlockAtoB (16, 32);
            MoveBlockAtoB (16, 33);
            MoveBlockAtoB (17, 34);
            MoveBlockAtoB (18, 35);
            MoveBlockAtoB (18, 36);

            MoveBlockAtoB (1, 7);
            MoveBlockAtoB (2, 9);
            MoveBlockAtoB (3, 11);
            MoveBlockAtoB (4, 13);
            MoveBlockAtoB (5, 15);
            MoveBlockAtoB (6, 17);

            MoveBlockAtoB (0, 1);
            MoveBlockAtoB (0, 2);
            MoveBlockAtoB (0, 3);
            MoveBlockAtoB (0, 4);
            MoveBlockAtoB (0, 5);
            MoveBlockAtoB (0, 6);
        }

        if (IsSection2Full == 0)
        {
            for (int i = 0; i < 12; i++)
            {
                Section2 [i].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
            }

            MoveBlockAtoB (1, 7);
            MoveBlockAtoB (2, 9);
            MoveBlockAtoB (3, 11);
            MoveBlockAtoB (4, 13);
            MoveBlockAtoB (5, 15);
            MoveBlockAtoB (6, 17);

            MoveBlockAtoB (0, 1);
            MoveBlockAtoB (0, 2);
            MoveBlockAtoB (0, 3);
            MoveBlockAtoB (0, 4);
            MoveBlockAtoB (0, 5);
            MoveBlockAtoB (0, 6);
        }

        if (IsSection1Full == 0)
        {
            for (int i = 0; i < 6; i++)
            {
                Section1 [i].GetComponent<SpriteRenderer> ().sprite = ColorTile [0];
            }

            MoveBlockAtoB (0, 1);
            MoveBlockAtoB (0, 2);
            MoveBlockAtoB (0, 3);
            MoveBlockAtoB (0, 4);
            MoveBlockAtoB (0, 5);
            MoveBlockAtoB (0, 6);
        }
    }

    private void MoveBlockAtoB(int a, int b)
    {
        Tile [b].GetComponent<SpriteRenderer> ().sprite = Tile [a].GetComponent<SpriteRenderer> ().sprite;
    }
}