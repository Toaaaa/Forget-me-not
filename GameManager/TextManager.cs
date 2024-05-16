using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, string[]> storyTalkData;//자동재생 스크립트용 대화 데이터.
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
    
    void GenerateData()//이곳에 대화 데이터들 입력
    {
        talkData.Add(1, new string[] { "최신식 컴퓨터다." });
        talkData.Add(1000, new string[] { "안녕:0", "이곳은 처음이지?:1" });//extraNPC는 초상화가 없지만, 임시로 초상화넣기.
        
        portraitData.Add(1000 + 0, portraitArr[0]);
        portraitData.Add(1000 + 1, portraitArr[1]);
    }
    // 설명문 하나뿐인 엑스트라npc, 물건의 경우 1~999
    // 대화가 여러개인 npc는 1000,2000,3000....으로 구분.

    void GenerateStoryData()//이곳에 스토리 데이터들 입력
    {
        /////알람 스크립트/////
        storyTalkData.Add(0, new string[]
        {
            "여기로는 갈 필요가 없을것 같아"
        });

        /////스토리 스크립트/////
        storyTalkData.Add(1000, new string[]
        {
            "일어나세요...:7:???:0"
        });
        storyTalkData.Add(2000, new string[]
        {
            //여기서 고양이 오브젝트 fadein.
            "야옹 :4:고양이:2", //여기 끝의 2가 splie[3]에서 고양이 fadein을 위한 인덱스.
            "야옹아 안녕:0:나:0",
            "야옹 (고양이는 당신을 바라보고 있다) :4:고양이:0",
            "(밤이라도 너무 조용한데..?):0:나:0",
            //여기에 스마트폰을 보는 모션을 취하고.
            "요즘 바빠서 전혀 운동을 못해서 답답하네. 잠도 안오는데 잠시 산책이나 할까.:0:나:0"
        });
        storyTalkData.Add(3000, new string[] 
        {
            "역시 다른 사람은 없구나... 이렇게 차원의 경계로 넘어오는 경우는 흔치 않지.:4:고양이:0",
            "뭐야?!?!:0:나:0", //화면 흔들기
            "고양이가 말을 한다!!!:0:나:0",
            "...:4:고양이:0",
            "고양이 아니에요...:4:고양이:0",
            "고양이 아니에요...?:0:나:0",
            "저는 이곳과 다른 세상에서온 마법사 입니다.:4:고양이:0",
            "다른 차원에 미치는 영향을 최소화 하기 위해서 이런 모습으로 왔어요.:4:고양이:0",
            "당신이 사는 세상이 이렇게 변한 이유는, 아마도 저가 받은 저주의 영향이라고 생각해요...:4:고양이:0",
            "죽어도 죽지 못하는 존재가 되어, 수백년간 의 불사의 인과율이 쌓이고 :4:고양이 :0",
            "세계가 무리하게 확장을 하는 과정에서 해당 차원을 침범하였고, 이대로 방치하면 당신의 세계는 사라질거에요.:4:고양이:0",
            "엑..?? 그럼 이대로 나는 죽는거야?:0:나:0",
            "그렇게 되지 않도록 저를 도와주실 수 있을까요?:4:고양이:0",
            ".......:0:나:1",//0을 붙혀서 암전효과하고싶은데..
            "그렇다면, 너의 존재가 각인된 물건을 하나씩 찾아서 부수면 해결 된다는 거지?:0:나:0",
            "네, 위험한 여정이 될 수도 있을것 같은데, 동의해 주셔서 감사합니다:4:고양이:0",
            "아니야.. 어짜피 아무것도 안하면 이대로 사라져 버리는거 아니야:0:나:0",
            "그건 그렇네요.:4:고양이:0",
            "현재 저희 차원과 당신의 차원이 연결된 상태이기에 각각의 물건이 있는 위치는 빠르게 이동이 가능 할 거에요:4:고양이:0",
            "먼저 빠르게 첫번째 각인 부터 찾으러 가도록 하죠.:4:고양이:0",
            "아까 잠시 확인을 해 보니, 마을 윗쪽의 강변에 있었던거 같아요:4:고양이:0",
        });
        storyTalkData.Add(4000, new string[]
        {
            "저기 있는 하수구 구멍으로 들어가라는 소리는 아니지?? :3:나:0",
            "맞아요! 저곳이 저희 차원과 연결된 통로에요!:5:고양이:0",
            "...... :3:나:0",
            "가까이 다가가면 포탈을 사용하실 수 있으실 거에요.:4:고양이:0"
        });


        for (int i = 0; i < 8; i++)
        {
            storyPortraitData.Add(1000 + i, storyArr[i]);
            storyPortraitData.Add(2000 + i, storyArr[i]);
            storyPortraitData.Add(3000 + i, storyArr[i]);
        }//0번 스테이지 초상화.
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
