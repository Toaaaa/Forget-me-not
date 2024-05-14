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
    Dictionary<int, Sprite> storyData;

    public Sprite[] portraitArr;
    public Sprite[] storyArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        storyTalkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
        //GenerateStoryData();
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
        storyTalkData.Add(0, new string[] { "���丮���:0", "���丮���:1" });

        storyData.Add(0 + 0, storyArr[0]);
        storyData.Add(0 + 1, storyArr[1]);
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
        return storyData[id + storyIndex];
    }

    public void storyScriptPlay(int storyNum)
    {
        GameManager.Instance.Player.StoryAction(storyNum);
    }
}
