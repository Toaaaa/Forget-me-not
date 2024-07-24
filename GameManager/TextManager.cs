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
        talkData.Add(1, new string[] { "오랫동안 방치된 해골이 있다. " });

        talkData.Add(1000, new string[] { "안녕123123123:0:임시NPC", "이곳은 처음이지?:1:임시NPC" });//extraNPC는 초상화가 없지만, 임시초상화 사용중.
        talkData.Add(2000, new string[] 
        {
            "연금술의 힘을 장비에 접목하는 것은 매우 위험한 시도다.:0:낡은 기록",
            "우리의 전통적인 장비 제작 기술은 수 세대에 걸쳐 증명된 안정성과 신뢰성을 자랑한다.:1:낡은 기록",
            "연금술은 예측할 수 없는 변화를 가져올 수 있으며, 이는 우리의 장비와 무기에 있어서 치명적인 결함을 초래할 수 있다.:0:낡은 기록",
            "전통적인 담금질을 통한 강도와 예리함을 만드는 기존의 법칙을 거스르는 방식으로 강화 되어서는 안된다.. :0:낡은 기록",
        });//1스테이지 책 1번
        talkData.Add(3000, new string[] 
        {
            "우리가 사용하는 약초와 연금술 비법은 단순한 치료제 그 이상이다.:0:낡은 기록",
            "과거 용사와 그 동료들이 우리 마을을 방문했을 때, 그들은 우리의 지혜와 약초의 힘을 경험했다.:1:낡은 기록",
            "특히, 용사는 우리의 약초에 깊은 관심을 보였고, :0:낡은 기록",
            "블러드루트로 만든 강화 포션을 전투에서 자주 사용하였다.:0:낡은 기록",
            "또한, 각종 희귀한 재료로 만들어 농축시킨 블러드 엘릭서의 경우 제조방식은 까다롭지만 완성만 한다면:0:낡은 기록",
            "복용시 영구적으로 신체능력을 향상시키는 효과를 가지고 있다.:0:낡은 기록"
        });//1스테이지 책 2번
        talkData.Add(4000, new string[]
        {
            "에버글로우 강철: 이 특별한 강철은 연금술적인 과정을 통해 빛을 내는 성질을 지니게 되었다.0:낡은 기록",
            "어둠 속에서 빛을 발하여, 광부들이 작업할 때나 전사들이 야간 전투 시 유용하다.:1:낡은 기록",
            "문실버: 달빛에 의해 강화된 은으로, 강력한 방어력을 제공하면서도 가볍다. :0:낡은 기록",
            "연금술로 처리하여 마법 저항력을 부여할 수 있다.:0:낡은 기록",
        });//1스테이지 책 3번
        talkData.Add(5000, new string[] 
        {
            "나는 이 검을 마왕 과의 전투를 끝내고 돌아온 용사 파티의 전사에게 주려고 한것 같았다,:0:낡은 기록",
            "이곳은 하지만 용사 파티는 전사가 없다고 하는데..?:0:낡은 기록",
            "나는 누구에게 이 검을 주려고 했던 거지...?:1:낡은 기록"
        });//1스테이지 책 4번 (용사의 검)
        talkData.Add(6000, new string[]//isnpc =false >>오직 텍스트 박스만 출력
        {
            "평생을 한사람만 바라본 엘리엇. 여기에 묻히다.:0",
        });//2스테이지 절벽 무덤 1
        talkData.Add(7000, new string[]//isnpc =false
        {
            "바람꽃 같은 그녀 여기서 쉬어간다.:0",
            "-안나- :0",
        });//2스테이지 절벽 무덤 2
        talkData.Add(7000, new string[]//isnpc =false
        {
            "파란빛이 나오는것 같은 푸른 꽃이다. :0",
        });//바람나무 앞 바람꽃.
        talkData.Add(8000, new string[]//NPC(관리인)
        {
            "이놈의 눈은 치워도 치워도 없어지지 않아... :0:오두막 관리인",
            "벌써 10년째 마을로 가는 길목의 눈을 치우고 있는 일을 하는데:0:오두막 관리인",
            "바람정령님이 없어진 이후로는 눈이 더 많이 내리는것 같아. :0:오두막 관리인",
        });//2-5에 있는 오두막 관리인
        talkData.Add(9000, new string[]//isnpc=false
        {
            "임시 텍스트 :0",
            "임시 텍스트 :0",
            "임시 텍스트 :0",
        });//2스테이지 도서관의 책 1
        talkData.Add(10000, new string[]//isnpc=false
        {
            "임시 텍스트 :0",
            "임시 텍스트 :0",
            "임시 텍스트 :0",
            "임시 텍스트 :0",
        });//2스테이지 도서관의 책 2
        talkData.Add(11000, new string[]//isnpc=false
        {
            "임시 텍스트 :0",
            "임시 텍스트 :0",
            "임시 텍스트 :0",
        });//2스테이지 도서관의 책 3
        talkData.Add(12000, new string[]//isnpc=false
        {
            "임시 텍스트 :0",
            "임시 텍스트 :0",
            "임시 텍스트 :0",
            "임시 텍스트 :0",
        });//2스테이지 도서관의 책 4



        for (int i = 0; i < portraitArr.Length; i++)
        {
            portraitData.Add(1000 + i, portraitArr[i]);
            portraitData.Add(2000 + i, portraitArr[i]);
            portraitData.Add(3000 + i, portraitArr[i]);
            portraitData.Add(4000 + i, portraitArr[i]);
            portraitData.Add(5000 + i, portraitArr[i]);
            portraitData.Add(6000 + i, portraitArr[i]);
            portraitData.Add(7000 + i, portraitArr[i]);
            portraitData.Add(8000 + i, portraitArr[i]);
            portraitData.Add(9000 + i, portraitArr[i]);
            portraitData.Add(10000 + i, portraitArr[i]);
            portraitData.Add(11000 + i, portraitArr[i]);
            portraitData.Add(12000 + i, portraitArr[i]);
        }
    }
    // 설명문 하나뿐인 엑스트라npc, 물건의 경우 1~999
    // 대화가 여러개인 npc는 1000,2000,3000....으로 구분.

    void GenerateStoryData()
    {
        //이곳에 스토리 데이터들 입력 0~3:플레이어,4~7:고양이, 8~11:견습기사, 12~15:마법사, 16~19:힐러, 20:엘더고블린
        //21:장로, 22:노인 (설원 맵에서의 서브퀘스트 노인), 

        /////알람 스크립트/////
        storyTalkData.Add(0, new string[]
        {
            "여기로는 갈 필요가 없을것 같아"
        });
        storyTalkData.Add(4, new string[]
        {
            "여기로는 갈 필요가 없을것 같아"
        });
        storyTalkData.Add(100, new string[]
        {
            " 획득."
        }); //아이템 획득시 출력 알람.

        /////스토리 스크립트/////
        /////기본 형식 >>  "대사:초상화번호:대상의이름:기타효과"
        //초상화 번호 0: 기본 1: 웃음 2: 놀람 3: 특수
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
            "차원에 미치는 영향을 최소화 하기 위해서 이런 모습으로 왔어요.:4:고양이:0",
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
            "현재 저희 차원과 당신의 차원이 연결된 상태이기에 각각의 물건이 있는 위치는 빠르게 이동이 가능 할 거에요.:4:고양이:0",
            "먼저 빠르게 첫번째 각인 부터 찾으러 가도록 하죠.:4:고양이:0",
            "아까 잠시 확인을 해 보니, 마을 윗쪽의 강변에 있었던거 같아요.:4:고양이:3",//고양이 fadeout
        });
        storyTalkData.Add(4000, new string[]
        {
            "저기 있는 하수구 구멍으로 들어가라는 소리는 아니지?? :3:나:0",
            "맞아요! 저곳이 저희 차원과 연결된 통로에요!:5:고양이:0",
            "...... :3:나:0",
            "가까이 다가가면 포탈을 사용하실 수 있으실 거에요.:4:고양이:0"
        });

        /////스토리 스크립트/////스테이지 1
        storyTalkData.Add(5000, new string[] //스테이지 1입장시 출력 (autostorywhenstart 에서 트리거 되면 자동으로 ss =true해주고 있음)
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
        storyTalkData.Add(6000, new string[] //1-2에서 타일 트리거
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
            "꼼짝없이 이곳에 갇혀서 기사 수행만 하다 죽는줄 알았어. :8:???:0",
            "흠흠... 나,드라니우스 백작가의 둘째. 깊은 던전까지 도우러 온 행동에 감사를 표하지. :8:???:0",
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
        storyTalkData.Add(7000, new string[] //1-5에서 타일 트리거
        {
            "저곳이 그곳이야. 숨어서 지키는 고블린이 있는지 근처에 다가가면 금방 나오더라고.:8:견습 기사:0",
            "주변에 딱히 적은 안보이는데.:0:나:0",
            "싸움이 시작되면 어딘가에 숨어있던 고블린들이 줄줄이 나와서 긴장해야해.:8:견습 기사:0",
            "그렇다면 빠르게 전투를 끝내는게 제일 중요할 것 같네요:4:고양이:0",
        });
        storyTalkData.Add(8000, new string[] //1-6입장시 출력
        {
            "진짜 약속이라도 한것처럼 끊임없이 몰려왔네.:0:나:0",
            "그래도 주변에 있는 고블린들은 저게 다겠죠?:4:고양이:0",
            "으악! 엘더 고블린이다!:10:견습 기사:5", //카메라 무빙으로 고블린 엘더 보여줌 (카메라 무빙 + 카메라 무빙 해주는 4번이 아닌 스크립트 번호 제작.)
            "저녀석 무지막지하게 강하다고 들었어.:8:견습 기사:7", //다시 카메라 주인공으로 바로 리턴
            "고블린들 중에서 가장 강한 전투력을 가진 녀석이 족장 비슷한 것으로 추앙 받는다고 하던데.:8:견습 기사:0",
            "나 죽는거 아니겠지..?:1:나:0",
            "그워어어어어!!!:20:엘더 고블린:6", //흔들림+카메라 무빙으로 고블린 엘더 보여줌
            "온다! 조심해!:0:나:7", //다시 카메라 주인공으로 바로 리턴
            ":0:?:9"//보스 전투 시작
        });
        storyTalkData.Add(9000, new string[] //1-6에서 타일 트리거
        {
            "이게 그거야?:0:나:0", //동상주변에서 대화를 나눔
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
            "..... :12:마법사:8",//고양이에서 마법사로 스프라이트가 바뀌는 효과
            "어..? :0:나:0",
            "어..? :8:견습 기사:0",
            "저주가 약해져서 원래의 모습으로 돌아와도 부담이 덜하네요. :12:마법사:0",
            "뭐야 진짜 고양이가 아니였네? :8:견습 기사:0",
            "고양이도 괜찮았는데.. :0:나:0",
            "아쉽지만 저는 사람입니다 후훗. :12:마법사:0",
            "이곳에서 할 일이 다 끝났으면 일단 이곳에서 나가자.:10:견습 기사:0",
            "하수구 냄새때문에 속이 나빠지고있어...:10:견습 기사:101", //stage1complete 세팅하기.
        });

        /////스토리 스크립트/////스테이지 1
        storyTalkData.Add(10000, new string[]//스테이지 2 입장시(입구의 타일을 밟으면) 출력 + isOnStage2
        {
            "으앗! 하수구 밖에 이런곳이 있었나??:10:견습 기사:0",
            "저희의 다음 목적지 까지 바로 이동하는 마법을 사용하였어요.:12:마법사:0",
            "그런 마법도 있다고?  세상은 정말 넓구나..:8:견습 기사:0",
            "그래서 이곳은 왜 온거야?:8:견습 기사:0",
            "제 기억이 맞다면 이곳에는 산 지킴이 종족이 살고 있을거에요, 먼저 그분들에게 가보죠.:12:마법사:0",
            "산 지킴이 종족은 뭐야? 엘프 같은건가?:0:나:0",
            "엘..프 는 처음 들어보네요, 산 지킴이는 대륙 북부의 산림지역에 거주지역이 넓게 분포된 종족이에요.:12:마법사:0",
            "평균 수명이 300살 가량으로 오랜시간 산에 거주하며 설산의 영혼을 섬기는 종족이다.:8:견습 기사:0",
            "수명이 길고 산에서 오랜시간 순수한 마나와 조화를 이루면서 높은 마나감응 능력이 있어서:8:견습 기사:0",
            "마나를 이용한 고차원의 마법을 구사할수 있기로 알려져있지.:8:견습 기사:0",
            "(엘프랑 비슷한 느낌이네..):0:나:0",
            "그런데 마법사 너는 무슨 종족이지?:8:견습 기사:0",
            "저는 그냥 사람이에요.  고양이의 모습으로 있었던 이유는 저주때문에 불안정한 마력을 안정화 하기 위한:12:마법사:0",
            "영향 범위가 작은 개체로 있어야 했기때문이고, 지금은 어느정도 안정화 되어서 원래의 모습으로 있어도 괜찮아 졌어요:12:마법사:0",
            "고양이 말고도 더 작은 동물도 있지 않아?:0:나:0",
            "그냥 제가 고양이를 좋아한다고 해둘게요.:12:마법사:0",
            "먼저 마을로 가보자, 나 멀쩡한 음식을 먹은지 너무 오래되서 배고프군.:8:견습기사:0",
        });
        storyTalkData.Add(11000, new string[]//마을에 도착(타일) 하면 출력 + checkpoint1
        {
            "나는 먼저 먹을것좀 찾으러 갈게, 볼일이 다 끝나면 불러줘 :8:견습 기사:0",
            "(견습 기사가 파티에서 이탈 하였습니다) :8:견습 기사:0",
            "그럼 저희는 마을의 장로를 찾으러 가볼까요:12:마법사:0",
        });
        storyTalkData.Add(12000, new string[]//(checkpoint2만 되어있을때) 장로에게 말을 걸면 출력 + checkpoint2
        {
            "안녕하세요, 혹시 이 마을의 장로님 되시는분 맞으신가요?:0:나:0",
            "어서 오게 , 여행자들이여. 험한 날씨를 뚫고 오느라 고생많았소.:21:장로:0",
            "반갑습니다, 장로님 :12:마법사:0",
            "그쪽은... 마나의 흐름이 익숙한데 혹시 예전에 만난적이 있는건가..:21:장로:0",
            "네, 아마 맞을거에요. 오래전에 이곳을 방문했던 마법사입니다. 정말 오랫만에 봽는것 같네요 에랄드 장로님.:12:마법사:0",
            "이럴수가! 이렇게 다시 만나게 될 줄이야.. :21:에랄드:0",
            "당신의 도움을 받아 마을이 위기를 벗어난 기억이 아직도 선명합니다 마법사님..:21:에랄드:0",
            "혹시 그때의 봉인에 문제가 생긴사실을 알고 오신건지? :21:에랄드:0",
            "봉인이라면, 드래곤의 봉인 말씀이신가요? :12:마법사:0",
            "그렇습니다.. 마법사님의 힘으로 완성한 봉인석의 힘이 최근  불안정해지며 봉인이 언제 깨질지 모르는 두려움에:21:에랄드:0",
            "하루하루를 지내고 있습니다. :21:에랄드:0",
            "최근 바람의 정령의 축복또한 잃어버려 마을 외부로도 도움을 구할수 없어 고민이 많았는데:21:에랄드:0",
            "이렇게 와주시니 정말 감사합니다... :21:에랄드:0",
            "아니에요, 예전에 많은 도움을 받았는데 제가 도와드릴수 있는 일이라면 뭐든지 돕겠습니다.:12:마법사:0",
            "항상 저희가 더 많은것을 받았기에 감사한 마음이 부족할 정도 입니다. 혹시 일행은 두분이 전부 인지..?:21:에랄드:0",
            "마을의 주점에서 저희 파티의 기사한명이 식사중입니다, 총 세명이서 여행중입니다 장로님.:12:마법사:0",
            "아, 그렇다면! 마침 도움을 드릴수 있을것 같습니다. :21:에랄드:0",
            "이쪽은 저희 마을의 신관인 리시아입니다, 마법사님의 파티에게 조금이나마 도움이 될수 있지 않을까 합니다.:21:에랄드:10",//리시아 페이드인
            "안녕하세요 마법사님, 리시아 라고 합니다 :16:리시아:0",
            "어렷을 적부터 마법사님의 이야기를 자주 들으며 자랐습니다,  만나봽게 되어 영광입니다.:16:리시아:0",
            "이 아이가 마을의 지리와 저희 마을에 계시면서의 신분을 보증해 드릴겁니다.:21:에랄드:0",
            "부디 또 한번 저희 마을을 지켜주십쇼..:21:에랄드:0",
        });
        storyTalkData.Add(13000, new string[]//장로 집에서 나오면 출력checkpoint3
        {
            "그... 마법사님? 혹시.. 올해 연세가 어떻게 되시는..지?:3:나:0",
            "숙녀의 나이를 묻는건 그쪽 세상에서도 예의가 아니라고 알고있어요.:13:마법사:0",
            "네... :0:나:0",
            "먼저 기사님를 대려오도록 하죠, 봉인이 언제 풀릴지 모르기에 조금은 서두를 필요가 있을것 같아요.:12:마법사:0",
            "기사님이라면 아까 처음보는 외지분이 마을 왼쪽의 주점으로 가는것을 봤어요.:16:리시아:0",
            "그런 복장이라면 눈에 띌수 밖에 없겠구나.:0:나:0",

        });
        storyTalkData.Add(14000, new string[]//주점에서 기사에게 말을걸면 + checkpoint4 (해당 체크포인트가 되어있을때 주점에서 대화용 기사 off하기)
        {
            "얼마만의 따듯한 음식이냐, 감동스러워서 눈물이 나올것 같아. :9:견습 기사:0",
            "기사님 이제 이동할 시간이에요. :12:마법사:0",
            "버..벌써?? 나 그냥 여기서 살고 싶어, 음식도 맛있고 사람들도 친절하다고. :8:견습 기사:0",
            "기사 수행은 포기하시는건가요? :12:마법사:0",
            "그래서 우리 이제 무슨일 하면되는건데? :8:견습 기사:0",
            "(기사라는게 이렇게 위엄이 없는 직업인건가..) :0:나:0",
            "드래곤 봉인을 해야해. :0:나:0",
            "드래곤 봉인?? :10:견습 기사:0",
            "드래곤이라면 그 날개 달리고 불을 뿜는 내가 아는 그 드래곤 맞지? :10:견습 기사:0",
            "맞습니다 기사님, 아마 봉인을 강화 하는것 만으로도 충분 할것 같지만 봉인이 풀려버릴 가능성도 존재하니:12:마법사:0",
            "전투를 하게될 수도 있을것 같아요.:12:마법사:0",
            "으으.. 그것보다 이쪽의 여성분은 누구신지..?:8:견습 기사:0",
            "안녕하세요, 장로님의 부탁을 받고 용사님들을 돕게된 리시아 이라고 합니다, 신성마법과 치유마법을 사용할 수 있습니다.:17:리시아:0",
            "잘!! 부탁!! 드립니다!!:9:견습 기사:4",//텍스트 박스 진동
            "잘 부탁 드려요 기사님:17:리시아:0",
            "자 그럼 마을 우측 외곽에 있는 드래곤의 봉인 구역으로 가시죠.:16:리시아:0"
        });
        storyTalkData.Add(15000, new string[]//마을에서 오른쪽 출구로 나오며 + checkpoint5
        {
            "오른쪽 길을 따라가면 드래곤을 봉인한 곳이 나와요, 가는길이 쌓인 눈도 많고 날씨가 험하니 조심하세요.:16:리시아:0",
            "이곳은 평소도 이런 날씨인거야? 바람이 너무 강한것 같은데.:8:견습 기사:0",
            "원래는 바람의 정령님의 축복이 있어서 이정도로 바람이 강하지는 않아요.:16:리시아:0",
            "바람 정령님의 축복을 잃어 버리며 급격히 날씨가 나빠지고, 외부로 부터의 발길이 끊기며:16:리시아:0",
            "축복을 다시 되찾을 지원도 구하기 힘들어지며 이러지도 저러지도 못하는 상황이네요.:16:리시아:0",
            "바람도 차갑고 눈도 많이 쌓여있어서 움직이기 쉽지 않을것 같네.:8:견습 기사:0",
            "너무 춥다... 마법사는 괜찮아?:0:나:0",
            "제가 몸을 따듯하게 하는 마법을 써 드릴게요.:12:마법사:0",
        });
        storyTalkData.Add(16000, new string[]//봉인석(objectID 스크립트를 통해,npc,interactable)과 상호작용 + checkpoint6
        {
            "이것이 드래곤을 억제하고 있는 봉인석 입니다.:16:리시아:0",
            "와... 생긴거 진짜 무서운데?.:0:나:0",
            "이건.. 상태가 많이 안좋아 보이는걸요.. 봉인을 강화하려고 해도 봉인석이 부숴질게 분명해요.:12:마법사:0",
            "어떻게 방법이 없을까요? 드래곤이 돌아오면 숲이 망가져버릴거에요.:19:리시아:0",//울음
            "200년 이상 봉인이 된 상태기 때문에 예전에 비해서 많이 약해진 상태 일거라 생각합니다, 만약 봉인의 연장에 실패한다면:12:마법사:0",
            "직접 전투를 통해 쓰러트리는 방법밖에 없을것 같아요.:12:마법사:0",
            "이것과 싸운다고? 정예기사 10명은 있어야 쓰러트릴수 있는 상대 아닌가?:10:견습 기사:0",
            "약해진 상태라고 해도 우리들이서 이길수 있을지 걱정되는군..:8:견습 기사:0",
            "기사님께 전투를 강요할 수는 없어요, 지금이라도 돌아가셔도 괜찮습니다.:12:마법사:0",
            "(리시아를 흘끗 바라본다.):8:견습 기사:0",
            "흠흠.. 아니 그렇다고 해도 기사가 사람들의 어려움을 보고도 못 본척 할 수는 없는거 아니겠소.:8:견습 기사:0",
            "그럼 다들 동의한 것으로 판단하고 봉인의 강화를 시도해 보겠습니다. 봉인석이 부숴지면 바로 전투가 시작될 거에요.:12:마법사:0",
            "전투를 준비하겠습니다:16:리시아:0",
            "(봉인석이 빛이 난다...):0:나:11",//봉인석이 파괴되는 효과와 함께 전투 시작
        });
        storyTalkData.Add(17000, new string[]//드래곤을 처치후(checkpoint7Dragon) 해당 룬스톤 위치에 돌아왔을때 그위치의 타일과 반응하여 출력 + checkpoint7
        {
            "드래곤을... 잡았어..! 우리가.!!:8:견습 기사:0",
            "쉽지 않은 전투였네요.. 다들 고생하셨습니다.:16:리시아:0",
            "생각보다 힘을 보존한 상태의 드래곤이여서 고생했네요, 리시아님이 없었더라면 쉽지 않았을 전투였어요.:12:마법사:0",
            "마법사님과 함께 싸울수 있어서 영광이였습니다.:16:리시아:0",
            "그럼 이제 마을로 돌아가자, 장로님이 걱정하고 계실거야.:0:나:0",
            "네, 그러죠.:12:마법사:0",
        });
        storyTalkData.Add(18000, new string[]//마을로 돌아와 장로와 대화 + Stage2Check8
        {
            "돌아오셨군요 마법사님.:21:에랄드:1",//블랙아웃 + 원상태
            "결국 봉인은 깨져버렸군요.. 아무리 마법사님이 계셔도 드래곤은 쉽지 않은 상대였을텐데.:21:에랄드:0",
            "이 은혜는 잊지 않겠습니다.  마을 모두와 정령님들을 대표해서 감사인사를 드립니다..:21:에랄드:0",
            "용사님들은 혹시 이후 계획이 있으십니까?.:21:에랄드:0",
            "저희는 한동안 세계 각지를 돌아다닐것 같아요, 마왕이 저에게 건 저주를 풀기위해 여행중이에요.:12:마법사:0",
            "아직까지 마왕의 잔재가 남아있었군요, 예전에도 그러하셨지만 항상 큰 짐을 들고 계시는것 같습니다.:21:에랄드:0",
            "그래도 항상 제 곁에서 도움을 주는 동료들이 있기에 많이 무겁지는 않네요.:13:마법사:0",
            "혹시 그렇다면 여러분들의 파티에 리시아도 같이 따라가는건 어떻겠습니까? :21:에랄드:0",
            "저 아이는 항상 마을의 밖으로 나가고 싶어하였지만 기회가 마땅치 않았는데,마침 용사님들의 파티에 자리가 비는것 같아서 그렇습니다.:21:에랄드:0",
            "이렇게 훌륭한 힐러가 와준다면 저희야 좋습니다 장로님!.:8:견습 기사:0",
            "그렇게 해 주신다면 저희가 더 감사하죠, 저희와 함께 가실래요 리시아님?:12:마법사:0",
            "여러분들이 괜찮으시다면 기꺼히 같이 가도록 할게요.:16:리시아:0",
            "그렇다면 다음 이동 전 마을을 떠날때까지 편하계 계시다 가십시요 용사님들. 만약 여유가 되신다면:21:에랄드:0",
            "저희 마을을 지나가는 모험가들의 다양한 이야기들과 정보를 정리해서 보관해둔 도서관도 있으니:21:에랄드:0",
            "한번쯤 들러보시는것도 좋을것 같습니다. 혹시 도움이 될만한 정보가 있을지도 모르지요.:21:에랄드:0",
            "다시한번 정말 감사드립니다 용사님들.:21:에랄드:0",
        });
        storyTalkData.Add(19000, new string[]//설원을 떠나 스테이지 0으로 돌아온뒤. +isStage2Completed
        {
            "따로 파괴한 물건은 없었던것 같은데 혹시 봉인석이 이번의 목표였던거야?:0:나:0",
            "네, 어렴풋이 예상은 했지만 역시 맞았네요, 그래도 봉인이 불안정한 상태여서 다행이였던것 같아요.:12:마법사:0",
            "아무래도 멀쩡히 지키고 있는 봉인석을 파괴하자고 할 수는 없으니깐요..:12:마법사:0",
            "그럼 다음 각인도 찾으로 가볼까?:0:나:0",
            "그럴까요?  뭔가 옛날 생각도 나고 즐겁네요.:13:마법사:0",
            "용사 파티에서 모험했던 시절말이야?:0:나:0",
            "네, 다들 밝고 긍정적인 분들이여서 매 순간이 즐거웠어요. 아무리 강한 적이여도 이길수 있을것 같은 그런 기분도 들었고 :12:마법사:0",
            "지금은 더이상 만날수 없지만 제 기억속에 오랫동안 간직하고 있어요.:12:마법사:0",
            "그럼 이 여정이 끝났을때 나도 기억해줘:0:나:0",
            "후훗...앞으로도 계속 기억할거에요.:13:마법사:0",//이전까지도 기억하고 있었다는 떡밥 (주인공 == 용사의 환생)
            "그럼 다음 지역으로 가 볼까요? :12:마법사:0",
            "제 기억이 맞다면, 당신이 계시던 집 바로 옆에 포탈이 있었어요.:12:마법사:0",
        });
        storyTalkData.Add(20000, new string[]//리시아의 합류후(checkpoint4) ~ isStage2Completed 전까지 진행 가능 + stage2_extra0
        {
            "쿨럭 쿨럭....:22:노인:0",
            "최근 몇달간 날씨가 나빠서 이렇게 외지인들을 보는것도 오랬만이구먼..:22:노인:0",
            "원래는 이렇지 않았나?  워낙 눈이 많은곳이라 항상 이렇게 바람이 강하고 추운곳인줄 알았는데.:8:견습 기사:0",
            "허허... 눈이 항상 쌓이는 곳이긴 했지만 해와 바람의 정령님이 축복을 내려주어 어느정도 따듯하게 지내고 있었다네.:22:노인:0",
            "그랬던 정령님들이 어디라도 가신건가, 이곳의 날씨가 말이 아닌데.:8:견습 기사:0",
            "바람의 정령님이 좋아하시는 꽃이 시들어버린게야..:22:노인:0",
            "몇 백년 동안 아무런 관리없이 잘 자라던 바람꽃이 어느날 천천히 마르기 시작하더니 흔적도 없이 사라졌어..:22:노인:0",
            "봉인의 힘이 약해진 것과 관련이 있는것 같네요.:12:마법사:0",
            "바람꽃이 제 자리를 다시 찾아준다면 정령님도 돌아오실텐데...:22:노인:0",
            "......:8:견습 기사:0",
            "......:22:노인:0",
            "나이가 들어서 내가 산을 오를수도 없고..:22:노인:0",
            "저희가 도와드릴게요 어르신.:0:나:0",
            "정말인가..? 그렇게 해준다면 더할나위 없이 고맙지.:22:노인:0",
            "바람꽃은 어디서 구하면 되는건가요?:0:나:0",
            "산속 깊은곳 인적이 드문 절벽근처에서 자생한다고 어릴적 들었네.:22:노인:0",
            "그중 몇송이만 가져와서 바람정령님의 나무 아래에 심어두면 이후부터는 우리가 더 조심히 관리 할 걸세:22:노인:0",
            "혹시 기존에 있던 바람꽃 말고 다른 바람꽃을 보신적이 있나요?:0:나:0",
            "한번도 본적이 없다네.:22:노인:0",
            "(기사가 의심하는 눈으로 바라본다):8:견습 기사:0",
            "바람꽃을 저희들이 찾을 수 있을지 모르겠지만 찾게 된다면 그렇게 할게요.:12:마법사:0",
            "고맙네 젊은이들:22:노인:0",
        });
        storyTalkData.Add(21000, new string[]//(퀘스트를 받고)2-6지역에 도착했을때 출력 + stage2_extra1
        {
            "그런데 정령이라는거 직접 본적 있는거야?:8:견습 기사:0",
            "정령님들은 마나감응력만 좋은분들이면 누구든지 볼 기회가 있습니다.:16:리시아:0",
            "저같은 경우 치유능력을 정령님을 통해서 빌려오고 있기에 항상 주변에서 보이고 있습니다.:16:리시아:0",
            "어!! 정말? 나도 보고싶은데 가능할까?:8:견습 기사:0",
            "기사님은 마나의 흐름이 전혀 느껴지지 않네요, 아마 정령님을 보시는건 어려울것 같습니다.:16:리시아:0",
            "그렇겠지..? 어렴풋이 느낌은 왔지만 직접 들으니 조금 슬퍼지는구만.:8:견습 기사:0",
            "나도 어렸을때는 마법사가 되고싶었는데 우리가문이 뿌리깊은 왕국 기사 가문이라고 하더라고.:8:견습 기사:0",
            "그 말을 듣고 마법사가 되는 꿈은 빠르게 포기하고 기사의 길을 걷고있지.:0:나:0",
            "육신에서 영혼이 벗어나게 되면 정령님이 기사님의 영혼을 인도해 주실거에요. 그때라면 가능할 것 같습니다.:16:리시아:0",
            "죽으면 볼 수 있다는 뜻이지...?:10:견습 기사:0",
        });
        storyTalkData.Add(22000, new string[]//꽃을 발견하여 채집(상호작용 하면) + stage2_extra2
        {
            "이런곳에 꽃이있으니 영감님이 우리에게 부탁할 수밖에 없지.:8:견습 기사:0",
            "아무래도 늙은 몸으로 이곳까지 오기에는 너무 위험하니깐요.:12:마법사:0",
            "그럼 우린 이제 이걸 가져가서 바람 정령의 나무 아래에 심으면 된다는거지?:0:나:0",
            "마을의 왼쪽으로 가게되면 바람 정령님의 나무가 있어요.:16:리시아:0",
            "영감님도 기다릴텐데 빨리가서 심어보자.:8:견습 기사:0",
        });
        storyTalkData.Add(23000, new string[]//extra2 상태에서 나무 아래로 가게되면 출력 + stage2_extra3 //(텍스트만 출력되는 알람)
        {
            "(흙을파서 조심히 나무아래에 꽃을 심는다):0: :0",
            "(꽃에서 약한 파란 빛이 나오다 약해지며 사라졌다):0: :0",
            "(나무 아래에 무언가 반짝이고 있다):0: :0",
            "(리시아가 고개를 살짝 들며 미소를 짓는다):0: :0",
        });
        storyTalkData.Add(24000, new string[]//다 끝내고 노인에게 말을 걸면 출력 + stage2_extra4
        {
            "자네들 해냈군..! 바람의 정령님이 돌아오셨어.:22:노인:0",
            "어르신도 정령이 보이시는 건가요?:0:나:0",
            "나는 이제 늙어서 보이지는 않지만 느껴진다네... 따듯한 바람에서 정령님이 축복이 느껴져..:22:노인:0",
            "고맙다네 젊은이들...:22:노인:1",//맵전체의 안개가 눈에띄게 약해짐.(마을에는 원래 안개 X)
        });





        for (int i = 0; i < 22; i++)
        {
            storyPortraitData.Add(1000 + i, storyArr[i]);
            storyPortraitData.Add(2000 + i, storyArr[i]);
            storyPortraitData.Add(3000 + i, storyArr[i]);
            storyPortraitData.Add(4000 + i, storyArr[i]);
            storyPortraitData.Add(5000 + i, storyArr[i]);
            storyPortraitData.Add(6000 + i, storyArr[i]);
            storyPortraitData.Add(7000 + i, storyArr[i]);
            storyPortraitData.Add(8000 + i, storyArr[i]);
            storyPortraitData.Add(9000 + i, storyArr[i]);
            storyPortraitData.Add(10000 + i, storyArr[i]);
            storyPortraitData.Add(11000 + i, storyArr[i]);
            storyPortraitData.Add(12000 + i, storyArr[i]);
            storyPortraitData.Add(13000 + i, storyArr[i]);
            storyPortraitData.Add(14000 + i, storyArr[i]);
            storyPortraitData.Add(15000 + i, storyArr[i]);
            storyPortraitData.Add(16000 + i, storyArr[i]);
            storyPortraitData.Add(17000 + i, storyArr[i]);
            storyPortraitData.Add(18000 + i, storyArr[i]);
            storyPortraitData.Add(19000 + i, storyArr[i]);
            storyPortraitData.Add(20000 + i, storyArr[i]);
            storyPortraitData.Add(21000 + i, storyArr[i]);
            storyPortraitData.Add(22000 + i, storyArr[i]);
            storyPortraitData.Add(23000 + i, storyArr[i]);
            storyPortraitData.Add(24000 + i, storyArr[i]);
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
