using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckSSForStatus : MonoBehaviour
{
    public int ID;
    public bool haveInteracted;//이미 상호작용이 완료되었는지 여부.
    public Tilemap afterInteractTilemap;//상호작용 후 타일맵
    public StoryScriptable storyScriptable;


    // Update is called once per frame
    void Update()
    {
        switch (ID)
        {
            case 1:
                if (storyScriptable.stage1_roomDoor)
                {
                    afterInteractTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    haveInteracted = true;
                }
                else
                {
                    haveInteracted = false;
                }
                break;
            case 2:
                if (storyScriptable.stage1_Statue)
                {
                    afterInteractTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    haveInteracted = true;
                }
                else
                {
                    haveInteracted = false;
                }
                break;

            default:
                break;
        }
    }

    public void Interaction()//player가 상호작용을 했을 때 실행되는 함수.
    {
        //추후 소리 추가.
        switch (ID)
        {
            case 1:
                storyScriptable.stage1_roomDoor = true;
                break;
            case 2:
                storyScriptable.stage1_Statue = true;
                break;
            default:
                break;
        }
    }
}
