using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemBox : MonoBehaviour
{
    public int ID;
    public bool isOpened;
    public Item itemInBox;
    public Tilemap openedBoxTilemap;//���� ������ �ڽ� Ÿ�ϸ�
    public StoryScriptable storyScriptable;

    private void Update()
    {
        switch (ID)
        {
            case 1:
                if (storyScriptable.stage1_box1)
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;
            case 2:
                if (storyScriptable.stage1_box2)
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;
            case 3:
                if (storyScriptable.stage1_box3)
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;
            case 4:
                if (storyScriptable.stage1_bossbox1)
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;
            case 5:
                if (storyScriptable.stage1_bossbox2)
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;
            case 6:
                if(storyScriptable.stage1_Sword)
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;

             case 7:
                if(storyScriptable.stage2_CaveBox)
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;
            case 8:
                if(storyScriptable.hidden_box) //Blood Elixir �� �������.
                {
                    openedBoxTilemap.gameObject.SetActive(true);
                    gameObject.SetActive(false);
                    isOpened = true;
                }
                else
                {
                    isOpened = false;
                }
                break;
            case 9:
                if(storyScriptable.stage2_windearring)
                {
                    gameObject.SetActive(false);
                }
                if (storyScriptable.Stage2Extra3)//�ɱ��� �Ͱ��� ��Ȱ��ȭ
                {
                    gameObject.SetActive(false);
                }
                break;
            default:
                Debug.Log("Box ID not found");
                break;
        }
    }

    public void OpenBox()
    {

        switch (ID)
        {
            case 1:
                GetBoxItem();
                storyScriptable.stage1_box1 = true;
                break;
            case 2:
                GetBoxItem();
                storyScriptable.stage1_box2 = true;
                break;
            case 3:
                GetBoxItem();
                storyScriptable.stage1_box3 = true;
                break;
            case 4:
                GetBoxItem();
                storyScriptable.stage1_bossbox1 = true;
                break;
            case 5:
                GetBoxItem();
                storyScriptable.stage1_bossbox2 = true;
                break;
            case 6:
                GetBoxItem();
                storyScriptable.stage1_Sword = true;
                break;
            case 7:
                GetBoxItem();
                storyScriptable.stage2_CaveBox = true;
                break;
            case 8:
                GetBoxItem();
                storyScriptable.hidden_box = true;
                break;
            case 9:
                if(!storyScriptable.stage2_windearring && storyScriptable.Stage2Extra3)//���� ����Ʈ�� �Ϸ��߰� + ���� ȹ�� ���϶�  ȹ�氡��
                {
                    GetBoxItem();
                    storyScriptable.stage2_windearring = true;
                }

                break;
            default:
                Debug.Log("Box ID not found");
                break;
        }
    }

    private void GetBoxItem()//�ڽ��� ������ ȹ��
    {
        //���� ���
        GameManager.Instance.inventory.AddItem(itemInBox, 1, (int)itemInBox.itemType);
        //ȹ�� �ϸ� "(�������̸�) �� ȹ�� �Ͽ����ϴ�" ��� �˸� ���.
    }
}
