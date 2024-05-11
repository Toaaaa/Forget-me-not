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
    
    void GenerateData()//이곳에 대화 데이터들 입력
    {
        talkData.Add(1, new string[] { "최신식 컴퓨터다." });
        talkData.Add(2, new string[] { "안녕", "이곳은 처음이지?" });
        
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
