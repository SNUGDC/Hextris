using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlock : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 2.1f, transform.position.z);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Destroy(this);
        }
    }
}
