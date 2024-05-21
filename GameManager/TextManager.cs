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
        storyTalkData.Add(4, new string[]
        {
            "여기로는 갈 필요가 없을것 같아"
        });

        /////스토리 스크립트/////스테이지 0
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
            "요즘 바빠서 전혀 운동을 못해서 답답하네. 잠도 안오는데 잠시 산책이나 할까.:0:나:3"//고양이 fadeout
        });
        storyTalkData.Add(3000, new string[] 
        {
            "역시 다른 사람은 없구나... 차원의 경계로 넘어오는 경우는 흔치 않지.:4:고양이:2",//고양이 fadein
            "뭐야?!?!:0:나:4", //화면 흔들기
            "고양이가 말을 한다!!!:0:나:0",
            "...:4:고양이:0",
            "고양이 아니에요...:4:고양이:0",
            "고양이 아니에요...?:0:나:0",
            "저는 이곳과 다른 세상에서온 마법사 입니다.:4:고양이:0",
            "다른 차원에 미치는 영향을 최소화 하기 위해서 이런 모습으로 왔어요.:4:고양이:0",
            "당신이 사는 세상이 이렇게 변한 이유는, 아마도 저가 받은 저주의 영향이라고 생각해요...:4:고양이:0",
            "죽어도 죽지 못하는 존재가 되어, 수백년간 의 불사의 인과율이 쌓이고 :4:고양이 :0",
            "세계가 무리하게 확장을 하는 과정에서 해당 차원을 침범하게 되었습니다.:4:고양이:0",
            "이대로 방치하면 당신의 세계는 사라질거에요.:4:고양이:0",
            "엑..?? 그럼 이대로 나는 죽는거야?:0:나:0",
            "그렇게 되지 않도록 저를 도와주실 수 있을까요?:4:고양이:0",
            ".......:0:나:1",//암전 효과
            "그러니깐..마왕의 저주 때문에 너의 존재를 속박시키는 물건들이 있고.:0:나:0",
            "그것들을 하나씩 찾아서 부수면 해결된다는 거지?:0:나:0",
            "네, 위험한 여정이 될 수도 있을것 같은데, 동의해 주셔서 감사합니다:4:고양이:0",
            "아니야.. 어짜피 아무것도 안하면 이대로 사라져 버리는거 아니야:0:나:0",
            "그건 그렇네요.:4:고양이:0",
            "현재 저희 차원과 당신의 차원이 연결된 상태이기에 각각의 물건이 있는 위치는 빠르게 이동이 가능 할 거에요:4:고양이:0",
            "먼저 빠르게 첫번째 각인 부터 찾으러 가도록 하죠.:4:고양이:0",
            "아까 잠시 확인을 해 보니, 마을 윗쪽의 강변에 있었던거 같아요:4:고양이:3",//고양이 fadeout
        });
        storyTalkData.Add(4000, new string[]
        {
            "저기 있는 하수구 구멍으로 들어가라는 소리는 아니지?? :3:나:0",
            "맞아요! 저곳이 저희 차원과 연결된 통로에요!:5:고양이:0",
            "...... :3:나:0",
            "가까이 다가가면 포탈을 사용하실 수 있으실 거에요.:4:고양이:0"
        });
        /////스토리 스크립트/////스테이지 1
        storyTalkData.Add(5000, new string[]
        {
            "윽... 진짜 하수구 잖아.. :3:나:0",
            "이곳은 드워프의 마을이였던 곳이네요.:4:고양이:0",
            "저가 알기론 마왕과의 전쟁이 시작되고 마을 곳곳의 샘물이 저주를 받아 오염되면서, 마을을 버리고 떠난것으로 알고 있어요:4:고양이:0",
            "마왕과의 전쟁?:0:나:0",
            "네, 전쟁은 300년 전에 끝났지만 강력한 저주의 힘은 오랜 시간이 지난 지금도 여전히 지속되고 있는것 같네요.:4:고양이:0",
            "그럼 저 물만 조심하면 안전하다는 말이지?:0:나:0",
            "글쎄요.. 300년의 시간이 지나면서 다른 몬스터들이 들어와 정착 했을 수도 있고,:4:고양이:0",
            "정확히 어떤 몬스터가 있는지 모르니 조심하는게 좋을것 같아요:4:고양이:0",
            "(저 초록색 물에 닿으면 아프겟지..?):0:나:0",
            "일단 주변을 둘러 볼까요, 각인된 물건의 기운이 확실히 느껴지는게, 이곳 어딘가에 있는건 분명한것 같아요:4:고양이:0",
        });
        storyTalkData.Add(6000, new string[]
        {
            //다음 맵에 입장하며 카메라 무빙과 함께 새로운 플레이어 탱커 합류
            "와!!! 드디어 사람이다!!:8:???:6",
            "내가 잘못 보고 있는거 아니지??? :8:???:0",
            "뭐라고 말좀해봐, 내가 헛것을 보고 있는건가?:8:???:0",
            "아..안녕하세요? :0:나:7",
            "진짜 사람이다! 여신님 감사합니다... :8:???:0",
            "아..안녕하세요? :0:나:0",
            "옛 드워프의 마을이 있다고 해서 찾은곳이 길이 하나같이 똑같이 생겼지뭐야. :8:???:0",
            "덕분에 5일째 나가는곳을 못 찾고 이제 식량도 다 떨어져가고 있어서 :8:???:0",
            "꼼짝없이 이곳에 갇혀서 기사 수행만 하다 죽는줄 알았어. :0:나:0",
            "기사 수행 중이신 견습 기사분이시군요? 만나서 반갑습니다 :4:고양이:0",
            "말하는 고양이..? 역시 헛것을 보고 있는건가.. :8:견습 기사:0",
            "사정이 있어서 고양이의 모습으로 지내고 있어요. :4:고양이:0",
            "흠..? :8:견습 기사:0",
            "일단 이곳에서 나가고싶어,이곳에 오래 있었더니 몸에서 시궁장 냄새가 나는것 같아. :8:견습 기사:0",
            "아쉽게도 이쪽은 먼저 해야 할 일이 있어, 마왕의 저주가 걸린 어떤 물건을 찾으러 왔는데, 혹시 이곳에서 독특한 물건 같은거 못봤어? :0:나:0",
            "독특한 물건? :8:견습 기사:0",
            "독특한 물건이라면 이곳에 자리잡은 고블린 무리들이 다 들고 있을거야 :8:견습 기사:0",
            "녀석들은 탐욕으로 가득해서 조금만 독특해 보이는 물건이라면 욕심을 내더라고. :8:견습 기사:0",
            "고블린 무리? 살짝 무서워 지려 하는데.:0:나:0",
            "혹시 고블린들이 물건을 어디에 보관하고 있는지 알고계신가요? :4:고양이:0",
            "기본적으로 숨어 지내는 몬스터 들이지만, 저번에 굴 안쪽 깊은곳의 어떤 동굴 같은곳에 접근하려 하니, 매우 경계하던데. :8:견습 기사:0",
            "혹시 그곳에 물건을 보관한게 아닐까 싶어. :8:견습 기사:0",
            "그렇다면 저희는 그쪽으로 가야 할것 같은데, 기사님은 어떻게 하시겠어요? :4:고양이:0",
            "일이 다 끝나고 바로 나갈수 있다면 나야 기꺼이 돕지. :8:견습 기사:0",
            "애초에 기사 수행이 목적이라 이후의 계획도 크게 없고 정처없이 떠돌아 다닐 뿐이니깐. :8:견습 기사:0",
            "그럼 한동안 저희들과 함께 하실래요? 저희도 여러지역을 돌아 다닐것 같아요 :4:고양이:1",//암전 효과
        });
        storyTalkData.Add(7000, new string[]
        {
            "저곳이 그곳이야. 숨어서 지키는 고블린이 있는지 근처에 다가가면 금방 나오더라고.:8:견습 기사:0",
            "주변에 딱히 적은 안보이는데.:0:나:0",
            "싸움이 시작되면 어딘가에 숨어있던 고블린들이 줄줄이 나와서 긴장해야해.:8:견습 기사:0",
            "그렇다면 빠르게 전투를 끝내는게 제일 중요할 것 같네요:4:고양이:0",
        });
        storyTalkData.Add(8000, new string[]
        {
            "진짜 약속이라도 한것처럼 끊임없이 몰려왔네.:0:나:0",
            "그래도 주변에 있는 고블린들은 저게 다겠죠?:4:고양이:0",
            "으악! 엘더 고블린이다!:8:견습 기사:5", //카메라 무빙으로 고블린 엘더 보여줌 (카메라 무빙 + 카메라 무빙 해주는 4번이 아닌 스크립트 번호 제작.)
            "저녀석 무지막지하게 강하다고 들었어.:8:견습 기사:7", //다시 카메라 주인공으로 바로 리턴
            "고블린들 중에서 가장 강한 전투력을 가진 녀석이 족장 비슷한 것으로 추앙 받는다고 하던데.:8:견습 기사:0",
            "나 죽는거 아니겠지..?:1:나:0",
            "그워어어어어!!!:20:엘더 고블린:6", //흔들림+카메라 무빙으로 고블린 엘더 보여줌
            "온다! 조심해!:0:나:7" //다시 카메라 주인공으로 바로 리턴
        });
        storyTalkData.Add(9000, new string[]
        {
            "이게 그거야?:0:나:0", //동상 오브젝트 활성화
            "네, 용사 파티의 동상이네요, 아마 드워프들이 전쟁의 승리를 기원하기 위해 만든것 같아요. :4:고양이:0",
            "용사 는 마법사를 부르는 거야? 제일 앞에 있네.:0:나:0",
            "아니요.. 용사파티의 용사는...:4:고양이:0",
            "용사 파티라고 부르는 이유는 파티 전원을 용사라고 한꺼번에 부르는거 아니야?:8:견습 기사:0",
            ".....:4:고양이:0",
            "세명이서 마왕을 물리치고 세상을 구원한, 모든 기사들의 우상과 같은 존재라고 할 수 있지.:8:견습 기사:0",
            "아무튼 이 동상을 부수면 된다는 거지?:0:나:0",
            ".....:0:나:0", //칼로 동상을 베는 효과 + 동상이 베여서 쓰러지는 애니메이션 + 페이드 아웃으로 동상이 사라지며 비활성화
            "이게 끝이야?:0:나:0",
            "네. 확실히 저주가 약해진게 느껴지네요. :4:고양이:0",
            "..... :12:마법사:8",
            "어..? :0:나:0",
            "어..? :8:견습 기사:0",
            "저주가 약해져서 원래의 모습으로 돌아와도 부담이 덜하네요. :12:마법사:0",
            "뭐야 진짜 고양이가 아니였네 :8:견습 기사:0",
            "고양이도 괜찮았는데.. :0:나:0",
            "아쉽지만 저는 사람입니다 후훗. :12:마법사:0",
            "이곳에서 할 일이 다 끝났으면 일단 이곳에서 나가자.:10:견습 기사:0",
            "하수구 냄새때문에 속이 나빠지고있어...:10:견습 기사:101", //stage1complete 세팅하기.
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
