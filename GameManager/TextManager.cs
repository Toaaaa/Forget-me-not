using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, string[]> storyTalkData;//�ڵ���� ��ũ��Ʈ�� ��ȭ ������.
    Dictionary<int, Sprite> portraitData;
    Dictionary<int, Sprite> storyPortraitData;

    public Sprite[] portraitArr;
    public Sprite[] storyArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        storyTalkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        storyPortraitData = new Dictionary<int, Sprite>();
        GenerateData();
        GenerateStoryData();
    }
    
    void GenerateData()//�̰��� ��ȭ �����͵� �Է�
    {
        talkData.Add(1, new string[] { "�ֽŽ� ��ǻ�ʹ�." });
        talkData.Add(1000, new string[] { "�ȳ�:0", "�̰��� ó������?:1" });//extraNPC�� �ʻ�ȭ�� ������, �ӽ÷� �ʻ�ȭ�ֱ�.
        
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
    }
    // ���� �ϳ����� ����Ʈ��npc, ������ ��� 1~999
    // ��ȭ�� �������� npc�� 1000,2000,3000....���� ����.

    void GenerateStoryData()//�̰��� ���丮 �����͵� �Է�
    {
        /////�˶� ��ũ��Ʈ/////
        storyTalkData.Add(0, new string[]
        {
            "����δ� �� �ʿ䰡 ������ ����"
        });

        /////���丮 ��ũ��Ʈ/////
        storyTalkData.Add(1000, new string[]
        {
            "�Ͼ����...:7:???:0"
        });
        storyTalkData.Add(2000, new string[]
        {
            //���⼭ ����� ������Ʈ fadein.
            "�߿� :4:�����:2", //���� ���� 2�� splie[3]���� ����� fadein�� ���� �ε���.
            "�߿˾� �ȳ�:0:��:0",
            "�߿� (����̴� ����� �ٶ󺸰� �ִ�) :4:�����:0",
            "(���̶� �ʹ� �����ѵ�..?):0:��:0",
            //���⿡ ����Ʈ���� ���� ����� ���ϰ�.
            "���� �ٺ��� ���� ��� ���ؼ� ����ϳ�. �ᵵ �ȿ��µ� ��� ��å�̳� �ұ�.:0:��:0"
        });
        storyTalkData.Add(3000, new string[] 
        {
            "���� �ٸ� ����� ������... �̷��� ������ ���� �Ѿ���� ���� ��ġ ����.:4:�����:0",
            "����?!?!:0:��:0", //ȭ�� ����
            "����̰� ���� �Ѵ�!!!:0:��:0",
            "...:4:�����:0",
            "����� �ƴϿ���...:4:�����:0",
            "����� �ƴϿ���...?:0:��:0",
            "���� �̰��� �ٸ� ���󿡼��� ������ �Դϴ�.:4:�����:0",
            "�ٸ� ������ ��ġ�� ������ �ּ�ȭ �ϱ� ���ؼ� �̷� ������� �Ծ��.:4:�����:0",
            "����� ��� ������ �̷��� ���� ������, �Ƹ��� ���� ���� ������ �����̶�� �����ؿ�...:4:�����:0",
            "�׾ ���� ���ϴ� ���簡 �Ǿ�, ����Ⱓ �� �һ��� �ΰ����� ���̰� :4:����� :0",
            "���谡 �����ϰ� Ȯ���� �ϴ� �������� �ش� ������ ħ���Ͽ���, �̴�� ��ġ�ϸ� ����� ����� ������ſ���.:4:�����:0",
            "��..?? �׷� �̴�� ���� �״°ž�?:0:��:0",
            "�׷��� ���� �ʵ��� ���� �����ֽ� �� �������?:4:�����:0",
            ".......:0:��:1",//0�� ������ ����ȿ���ϰ������..
            "�׷��ٸ�, ���� ���簡 ���ε� ������ �ϳ��� ã�Ƽ� �μ��� �ذ� �ȴٴ� ����?:0:��:0",
            "��, ������ ������ �� ���� ������ ������, ������ �ּż� �����մϴ�:4:�����:0",
            "�ƴϾ�.. ��¥�� �ƹ��͵� ���ϸ� �̴�� ����� �����°� �ƴϾ�:0:��:0",
            "�װ� �׷��׿�.:4:�����:0",
            "���� ���� ������ ����� ������ ����� �����̱⿡ ������ ������ �ִ� ��ġ�� ������ �̵��� ���� �� �ſ���:4:�����:0",
            "���� ������ ù��° ���� ���� ã���� ������ ����.:4:�����:0",
            "�Ʊ� ��� Ȯ���� �� ����, ���� ������ ������ �־����� ���ƿ�:4:�����:0",
        });
        storyTalkData.Add(4000, new string[]
        {
            "���� �ִ� �ϼ��� �������� ����� �Ҹ��� �ƴ���?? :3:��:0",
            "�¾ƿ�! ������ ���� ������ ����� ��ο���!:5:�����:0",
            "...... :3:��:0",
            "������ �ٰ����� ��Ż�� ����Ͻ� �� ������ �ſ���.:4:�����:0"
        });


        for (int i = 0; i < 8; i++)
        {
            storyPortraitData.Add(1000 + i, storyArr[i]);
            storyPortraitData.Add(2000 + i, storyArr[i]);
            storyPortraitData.Add(3000 + i, storyArr[i]);
        }//0�� �������� �ʻ�ȭ.
    }

    public string GetTalk(int id, int talkIndex)
    { 
         if (talkIndex == talkData[id].Length)
         {
             return null;
         }
         else
         {
             return talkData[id][talkIndex];
         }
    }

    public Sprite GetPortrait(int id, int portraitIndex) //id�� ����� id��ȣ
    {
        return portraitData[id + portraitIndex];
    }


    public string GetStoryTalk(int id, int talkIndex)
    {
        if (talkIndex == storyTalkData[id].Length)
        {
            return null;
        }
        else
        {
            return storyTalkData[id][talkIndex];
        }
    }
    public Sprite GetStoryPortrait(int id, int storyIndex)
    {
        return storyPortraitData[id + storyIndex];
    }

    public void storyScriptPlay(int storyNum)
    {
        if (GameManager.Instance.storyScriptable.isScript)
        {
            if(GameManager.Instance.Player.storyTalking)
            {
                if (Input.GetKeyDown(KeyCode.Space)&&!SceneChangeManager.Instance.duringBlackout)
                    GameManager.Instance.Player.StoryAction(storyNum);
            }
            else
            {
                GameManager.Instance.Player.StoryAction(storyNum);
            }
        }
            //GameManager.Instance.Player.StoryAction(storyNum);
    }

}
