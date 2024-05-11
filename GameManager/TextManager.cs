using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    
    void GenerateData()//�̰��� ��ȭ �����͵� �Է�
    {
        talkData.Add(1, new string[] { "�ֽŽ� ��ǻ�ʹ�." });
        talkData.Add(2, new string[] { "�ȳ�", "�̰��� ó������?" });
        
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

}
