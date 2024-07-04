using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCombatInStage0 : MonoBehaviour
{
    public StoryScriptable story;
    private MapData mapData;

    private void Start()
    {
        mapData = GetComponent<MapData>();
        if (story.isTutorialCompleted)
        {
            mapData.encounterRate = 0;
            GameManager.Instance.Player.GetComponent<RandomEncounter>().encounterRate = 0;
        }
    }

    void Update()
    {
        if (story.isTutorialCompleted)
        {
            mapData.encounterRate = 0;
        }
    }
}
