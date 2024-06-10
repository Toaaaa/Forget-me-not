using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckSSForStatus : MonoBehaviour
{
    public int ID;
    public bool haveInteracted;//�̹� ��ȣ�ۿ��� �Ϸ�Ǿ����� ����.
    public Tilemap afterInteractTilemap;//��ȣ�ۿ� �� Ÿ�ϸ�
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

    public void Interaction()//player�� ��ȣ�ۿ��� ���� �� ����Ǵ� �Լ�.
    {
        //���� �Ҹ� �߰�.
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
