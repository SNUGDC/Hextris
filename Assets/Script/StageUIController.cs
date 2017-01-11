using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUIController : MonoBehaviour
{
    public string[] StageName;

    private Vector2 NowRectPosition;
    private bool GoToNextMap;
    private bool GoToPriorMap;

    private void Start()
    {
        GoToNextMap = false;
        GoToPriorMap = false;
    }

    private void Update()
    {

        if (GoToNextMap == true)
        {
            
        }
        else if (GoToPriorMap == true)
        {
            
        }
    }

    public void NextStage()
    {
        GoToNextMap = true;
    }

    public void PriorStage()
    {
        GoToPriorMap = true;
    }

    public void GoToGameScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
