using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour
{
    public GameObject Base;
	public GameObject RightTriangle;
	public GameObject UpCurve;
	public GameObject StraightLine;
	public GameObject LeftTriangle;

	public bool IsBlockCreated;

    private GameObject MovingBlock;

	void Start ()
	{
		IsBlockCreated = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			if (IsBlockCreated == false)
			{
                if (Random.Range (1, 4) == 1)
                {
                    MovingBlock = Instantiate (RightTriangle, new Vector3 (0, -2.1f, 0), new Quaternion (0, 0, 0, 0));
                    MovingBlock.transform.parent = GameObject.Find ("Base").transform;
                    IsBlockCreated = true;
                }
                else if (Random.Range (1, 4) == 2)
                {
                    MovingBlock = Instantiate (UpCurve, new Vector3 (0, -2.1f, 0), new Quaternion (0, 0, 0, 0));
                    MovingBlock.transform.parent = GameObject.Find ("Base").transform;
                    IsBlockCreated = true;
                }
                else if (Random.Range (1, 4) == 3)
                {
                    MovingBlock = Instantiate (StraightLine, new Vector3 (0, -2.1f, 0), new Quaternion (0, 0, 0, 0));
                    MovingBlock.transform.parent = GameObject.Find ("Base").transform;
                    IsBlockCreated = true;
                }
                else if (Random.Range (1, 4) == 4)
                {
                    MovingBlock = Instantiate (LeftTriangle, new Vector3 (0, -2.1f, 0), new Quaternion (0, 0, 0, 0));
                    MovingBlock.transform.parent = GameObject.Find ("Base").transform;
                    IsBlockCreated = true;
                }
			}
		}
        else if(Input.GetKeyDown (KeyCode.UpArrow))
        {
            IsBlockCreated = false;
        }
	}
}
