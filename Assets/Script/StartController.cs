using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartController : MonoBehaviour
{
    public void MoveScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
