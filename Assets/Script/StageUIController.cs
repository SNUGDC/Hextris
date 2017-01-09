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
        StageInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(StageInfo.GetComponent<RectTransform>().anchoredPosition.x - 1080, StageInfo.GetComponent<RectTransform>().anchoredPosition.y);
    }

    public void PriorStage()
    {
        StageInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(StageInfo.GetComponent<RectTransform>().anchoredPosition.x + 1080, StageInfo.GetComponent<RectTransform>().anchoredPosition.y);
    }

    public void GoToGameScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
