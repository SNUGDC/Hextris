using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBase : MonoBehaviour 
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z + 60);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3 (0, 0, transform.eulerAngles.z - 60);
        }
    }
}
