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
    
    void GenerateData()//이곳에 대화 데이터들 입력
    {
        talkData.Add(1, new string[] { "최신식 컴퓨터다." });
        talkData.Add(1000, new string[] { "안녕:0", "이곳은 처음이지?:1" });//extraNPC는 초상화가 없지만, 임시로 초상화넣기.
        
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

    public Sprite GetPortrait(int id, int portraitIndex) //id는 상대의 id번호
    {
        return portraitData[id + portraitIndex];
    }
}
