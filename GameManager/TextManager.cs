using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    
    void GenerateData()//�̰��� ��ȭ �����͵� �Է�
    {
        talkData.Add(1, new string[] { "�ֽŽ� ��ǻ�ʹ�." });
        talkData.Add(1000, new string[] { "�ȳ�:0", "�̰��� ó������?:1" });//extraNPC�� �ʻ�ȭ�� ������, �ӽ÷� �ʻ�ȭ�ֱ�.
        
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
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
}
