using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUIController : MonoBehaviour
{
    public GameObject StageInfo;
    public string[] StageName;

    private Vector2 NowRectPosition;
    private float NowTime;
    private float ButtonClickedTime;
    private float FlowTime;
    private bool GoToNextMap;
    private bool GoToPriorMap;

    private void Start()
    {
        NowTime = 0;
        ButtonClickedTime = 0;
        FlowTime = 0;
        GoToNextMap = false;
        GoToPriorMap = false;
    }

    private void Update()
    {
        NowTime = NowTime + Time.deltaTime;
        FlowTime = NowTime - ButtonClickedTime;

        if (GoToNextMap == true)
        {
            StageInfo.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (StageInfo.GetComponent<RectTransform> ().anchoredPosition.x - 40 * FlowTime, StageInfo.GetComponent<RectTransform> ().anchoredPosition.y);
            if (FlowTime >= 1f)
            {
                GoToNextMap = false;
                StageInfo.GetComponent<RectTransform> ().anchoredPosition = NowRectPosition - new Vector2 (1080, 0);
            }
        }
        else if (GoToPriorMap == true)
        {
            StageInfo.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (StageInfo.GetComponent<RectTransform> ().anchoredPosition.x + 40 * FlowTime, StageInfo.GetComponent<RectTransform> ().anchoredPosition.y);
            if (FlowTime >= 1f)
            {
                GoToPriorMap = false;
                StageInfo.GetComponent<RectTransform> ().anchoredPosition = NowRectPosition + new Vector2 (1080, 0);
            }
        }
    }

    public void NextStage()
    {
        ButtonClickedTime = NowTime;
        GoToNextMap = true;
        NowRectPosition = StageInfo.GetComponent<RectTransform> ().anchoredPosition;
    }

    public void PriorStage()
    {
        ButtonClickedTime = NowTime;
        GoToPriorMap = true;
        NowRectPosition = StageInfo.GetComponent<RectTransform> ().anchoredPosition;
    }

    public void GoToGameScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
