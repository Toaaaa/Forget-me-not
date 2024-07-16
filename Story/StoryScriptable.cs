using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryProgress", menuName = "StoryProgress")]
public class StoryScriptable : ScriptableObject
{
    public List<bool> storyScriptable;

    public bool isScript;// if(!isScript)�� �ؼ� �ٸ� ���� �׽�Ʈ ���϶� �ش�ʿ� ��ũ��Ʈ�� ���� ���� �ʰ� �ϱ�.
    //��ũ��Ʈ������ �׽�Ʈ �� ���� true, �ٸ� ����� �׽�Ʈ �� ���� false.
    public bool firstTime;//���� ó�� �ʿ��� ������ ��. //ó�� ������ false�� ��ũ��Ʈ�� ������ true�� �ٲ�.
    public bool secondTime;//�ι�° ���� ó�� �� ��. //ó�� ������ false�� ��ũ��Ʈ�� ������ true�� �ٲ�.
    public bool second_map1;
    public bool second_map2;//�ֺ� �ʵ� Ž���Ϸ� ����. //Ž���� ���� true�� �ι�° ������ ���ƿ��� �� ���� ��ũ��Ʈ�� �Ѿ.
    public bool isTutorial;
    public bool isTutorialCompleted;
    public bool isOnStage1;//��������1�� ��������.
    public bool Stage1Started;//�߽� ��� �շ�����
    public bool Stage1beforEncounter;//������ ������ ���� ���������� ��ȭ
    public bool Stage1Encountered;//��������1 ������ ������ ���� ����
    public bool Stage1BossCompleted;//��������1 ������ �����Ҷ�.true����
    public bool isStage1Completed;//���丮9000�� ��簡 ������ true.
    ////////////////�������� 2
    public bool isOnStage2;
    public bool Stage2Check1;
    public bool Stage2Check2;
    public bool Stage2Check3;
    public bool Stage2Check4;
    public bool Stage2Check5;
    public bool Stage2Check6;
    public bool Stage2Check7;
    public bool Stage2Check7Dragon;
    public bool Stage2Check8;
    public bool isStage2Completed;
    public bool Stage2Extra0;
    public bool Stage2Extra1;
    public bool Stage2Extra2;
    public bool Stage2Extra3;
    public bool Stage2Extra4;
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
        Stage1beforEncounter = false;
        Stage1Encountered = false;
        Stage1BossCompleted = false;
        isStage1Completed = false;
        isOnStage2 = false;
        Stage2Check1 = false;
        Stage2Check2 = false;
        Stage2Check3 = false;
        Stage2Check4 = false;
        Stage2Check5 = false;
        Stage2Check6 = false;
        Stage2Check7 = false;
        Stage2Check7Dragon = false;
        Stage2Check8 = false;
        isStage2Completed = false;
        Stage2Extra0 = false;
        Stage2Extra1 = false;
        Stage2Extra2 = false;
        Stage2Extra3 = false;
        Stage2Extra4 = false;
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
