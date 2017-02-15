using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUIController : MonoBehaviour
{
    public string[] StageName;
    public Sprite[] MiniMapImage;
    public Sprite[] MapNameImage;
    public Image MiniMap;
    public Image MapName;

    private int MapNumber;

    private void Start()
    {
        MapNumber = 0;
    }

    private void Update()
    {
        MiniMap.sprite = MiniMapImage [MapNumber];
        MapName.sprite = MapNameImage [MapNumber];
    }

    public void NextStage()
    {
        if(MapNumber + 1 < StageName.Length)
            MapNumber = MapNumber + 1;
    }

    public void PriorStage()
    {
        if(MapNumber - 1 >= 0)
            MapNumber = MapNumber - 1;
    }

    public void GoToGameScene()
    {
        switch (MapNumber)
        {
            case 0:
                SceneManager.LoadScene("Basic");
                break;
            case 1:
                SceneManager.LoadScene("Dot");
                break;
            default:
                break;
        }
    }
}