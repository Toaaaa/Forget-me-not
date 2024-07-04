using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryProgress", menuName = "StoryProgress")]
public class StoryScriptable : ScriptableObject
{
    public List<bool> storyScriptable;

    public bool isScript;// if(!isScript)로 해서 다른 내용 테스트 중일때 해당맵용 스크립트가 실행 되지 않게 하기.
    //스크립트적용을 테스트 할 때는 true, 다른 기능을 테스트 할 때는 false.
    public bool firstTime;//제일 처음 맵에서 시작할 때. //처음 왔을때 false고 스크립트가 끝나면 true로 바뀜.
    public bool secondTime;//두번째 맵을 처음 올 때. //처음 왔을때 false고 스크립트가 끝나면 true로 바뀜.
    public bool second_map1;
    public bool second_map2;//주변 맵들 탐색완료 여부. //탐색이 전부 true고 두번째 맵으로 돌아왔을 때 다음 스크립트로 넘어감.
    public bool isTutorial;
    public bool isTutorialCompleted;
    public bool isOnStage1;//스테이지1에 들어왔을때.
    public bool Stage1Started;//견습 기사 합류시점
    public bool Stage1Encountered;//스테이지1 보스방 입장전 몬스터 조우
    public bool Stage1BossCompleted;//스테이지1 보스방 입장할때.true가됨
    public bool isStage1Completed;//스토리9000의 대사가 끝나면 true.
    public bool isOnStage2;
    public bool isStage2Completed;
    public bool isOnStage3;
    public bool isStage3Completed;
    public bool isOnStage4;
    public bool isStage4Completed;
    public bool isAllCompleted;


    public bool stage1_box1;//box1
    public bool stage1_box2;//box2
    public bool stage1_box3;//box3
    public bool stage1_bossbox1;//box4
    public bool stage1_bossbox2;//box5
    public bool stage1_Sword;//sword
    public bool stage1_Statue;//statue
    public bool stage1_roomDoor;//roomDoor

    public bool stage2_CaveBox;//caveBox

    public void restAll()
    {
        firstTime = false;
        secondTime = false;
        second_map1 = false;
        second_map2 = false;
        isTutorial = false;
        isTutorialCompleted = false;
        isOnStage1 = false;
        Stage1Started = false;
        Stage1BossCompleted = false;
        isStage1Completed = false;
        isOnStage2 = false;
        isStage2Completed = false;
        isOnStage3 = false;
        isStage3Completed = false;
        isOnStage4 = false;
        isStage4Completed = false;
        isAllCompleted = false;

        stage1_box1 = false;
        stage1_box2 = false;
        stage1_box3 = false;
        stage1_bossbox1 = false;
        stage1_bossbox2 = false;
        stage1_Sword = false;
        stage1_Statue = false;
        stage1_roomDoor = false;
        stage2_CaveBox = false;
    }




}
