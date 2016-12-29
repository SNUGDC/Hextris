using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public GameObject Tile;
    public Transform Base;

    private Vector3 XVector;
    private Vector3 YVector;
    private Vector3 ZVector;

    void Awake()
    {
        XVector = new Vector3 (0, 2.1f, 0);
        YVector = new Vector3 (1.83f, 1.05f, 0);
        ZVector = new Vector3 (1.83f, -1.05f, 0);
    }

    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int x = 0; x <= i; x++)
            {
                for (int y = 0; y <= i - x; y++)
                {
                    Vector3 Position = x * XVector + y * YVector + (i - x - y) * ZVector;
                    Instantiate (Tile, Position, transform.rotation, Base);
                }
            }
        }
    }
}
