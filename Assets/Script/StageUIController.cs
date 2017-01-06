using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUIController : MonoBehaviour
{
    public GameObject StageInfo;
    public string[] StageName;

    public void NextStage()
    {
        StageInfo.GetComponent<RectTransform>().position = new Vector3(StageInfo.GetComponent<RectTransform>().position.x - 1080, StageInfo.GetComponent<RectTransform>().position.y, 0);
    }

    public void PriorStage()
    {
        StageInfo.GetComponent<RectTransform>().position = new Vector3(StageInfo.GetComponent<RectTransform>().position.x + 1080, StageInfo.GetComponent<RectTransform>().position.y, 0);
    }

    public void GoToGameScene()
    {

    }
}
