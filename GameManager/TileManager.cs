using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour //돈 디스트로이를 하지 않은 특정 이벤트 발생용 타일 매니저 >> 각 씬마다 배치해줘서 맵마다 다를 맵 기믹또한 연출가능하게 할것.
{
    [SerializeField] private Tilemap tilemapforevent; //특정 이벤트를 발생시키기 위한 타일맵
    [SerializeField] private Tilemap tilemapforStory; //스토리 진행을 위한 타일맵 //storyscriptalbe의 값을 기반으로, 스크립트 재생이벤트 실행을 위한 타일.
    [SerializeField] private List<TileData> tileDatas; //각종 변수값들을 들고있는 타일 데이터 스크립터블 오브젝트.


    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        //using this to create a dictionary of tiles and their data to use the scriptable object for the tilemap
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach(var tiledata in tileDatas)
        {
            foreach(var tile in tiledata.tiles)
            {
                dataFromTiles.Add(tile, tiledata);
            }
        }
    }


    public bool IsMonsterZone(Vector2 worldPosition) //플레이어의 위치를 넣어서 해당위치의 타일이 스크립트 타일일 경우 해당 타일의 데이터를 가저와 몬스터존인지를 확인.
    {
        Vector3Int gridPosition = tilemapforevent.WorldToCell(worldPosition);

        TileBase tile = tilemapforevent.GetTile(gridPosition);//해당 타일맵의 그리드 포지션에서 타일을 가져오기.

        if(tile ==null) //타일이 없을때는 false 리턴. (ismonsterzone이 없는 타일) 이라는 false가 됨.
            //(정확히는 위에서 가져온 tilemapforevent에 등록된 타일맵의 타일이 아닐경우.
            return false;

        bool ismonsterzone = dataFromTiles[tile].isMonsterZone; //datafromtiles에서 tile을 키로 가지는 (tiledata스크립트의)데이터의 ismonsterzone을 가져옴.

        return ismonsterzone;
    }


    public void IsStoryTile(Vector2 worldPosition) //플레이어의 위치를 넣어서 해당위치의 타일이 스크립트 타일일 경우 해당 타일의 데이터를 가저와 스토리타일인지를 확인.
    {
        Vector3Int gridPosition = tilemapforStory.WorldToCell(worldPosition);

        TileBase tile = tilemapforStory.GetTile(gridPosition);//해당 타일맵의 그리드 포지션에서 타일을 가져오기.

        if (tile == null) //타일이 없을때는 false 리턴. (ismonsterzone이 없는 타일) 이라는 false가 됨.
            return;
        int storyNum = dataFromTiles[tile].storyNum;
        if (!dataFromTiles[tile].isStoryTile) //스토리타일이 아닌 데이터 세팅(ssobj 변수 변경) 타일일 경우.or 알림타일.
        {
            DataTile(storyNum);
        }
        else //스토리 타일일 경우 스토리 스크립트 재생
        {
            GameManager.Instance.textManager.storyScriptPlay(storyNum);
        }
        return;
    }


    void DataTile(int storynum) //update로 계속 출력되고 있음.
    {
         switch (storynum)//스토리 넘버 타일에 따른, 자동 ssobj 변수값 변경 OR 이벤트 발생. (ex. 알람 출력)
         {
            case 0:
                //오른쪽으로 가려할때(istage1completed가 아닐때는), (이벤트 스크립트textbox를 띄우며 "여기로는 갈 필요가 없을것 같아" 라며 )
                if (!GameManager.Instance.storyScriptable.isStage1Completed&&GameManager.Instance.Player.h>0)
                {
                    GameManager.Instance.Player.h = 0;//수평인풋 0
                    GameManager.Instance.Player.alarmOn = true;
                }
                if (GameManager.Instance.Player.alarmOn)
                {
                    GameManager.Instance.Player.ShowAlarm(storynum,2);//알람 출력.(타이핑 애니메이션)
                }
                break;
            case 1:
                if(!GameManager.Instance.storyScriptable.second_map1)
                    GameManager.Instance.storyScriptable.second_map1 = true;
                break;
            case 2:
                if(!GameManager.Instance.storyScriptable.second_map2)
                    GameManager.Instance.storyScriptable.second_map2 = true;
                break;
            case 4:
                if(!GameManager.Instance.storyScriptable.isTutorial&& GameManager.Instance.Player.v > 0)
                {
                    GameManager.Instance.Player.v = 0;//수직인풋 0
                    GameManager.Instance.Player.alarmOn = true;
                }
                if(GameManager.Instance.Player.alarmOn)
                {
                    GameManager.Instance.Player.ShowAlarm(storynum,1);
                }
                break;
            case 5://스테이지 2에서 전사가 합류 전일때 마을을 벗어나려 할 경우.
                if (!GameManager.Instance.playableManager.inParty.inPartySlots[2].inSlot && GameManager.Instance.Player.v < 0)
                {
                    GameManager.Instance.Player.v = 0;//수직인풋 0
                    GameManager.Instance.Player.alarmOn = true;
                }
                if (GameManager.Instance.Player.alarmOn)
                {
                    GameManager.Instance.Player.ShowAlarm(storynum, 0);//위로 강제이동(마을로)
                }
                break;
            case 6://유저가 장로방에 들어가려고 할때 못들어가게 강제 이동
                if (!GameManager.Instance.playableManager.inParty.inPartySlots[2].inSlot && GameManager.Instance.Player.h > 0)
                {
                    GameManager.Instance.Player.h = 0;//수평인풋 0
                    GameManager.Instance.Player.alarmOn = true;
                }
                if (GameManager.Instance.Player.alarmOn)
                {
                    GameManager.Instance.Player.ShowAlarm(storynum, 2);//왼쪽으로 강제이동
                }
                break;
            
            case 7100:
                if (!GameManager.Instance.storyScriptable.Stage1Encountered)//스테이지 1보스 입장전 몬스터 조우
                {
                    GameManager.Instance.storyScriptable.Stage1Encountered = true;
                    Player.Instance.placeBeforeEnteringCombat = Player.Instance.transform.position;
                    GameManager.Instance.combatManager.OnCombatStart();
                }
                break;
            /*case 23000://바람정령 나무 밑에 설치(꽃을 심는 과정)
                if (!GameManager.Instance.storyScriptable.Stage2Extra3&& GameManager.Instance.storyScriptable.Stage2Extra2)
                {
                    GameManager.Instance.storyScriptable.Stage2Extra3 = true;
                    GameManager.Instance.Player.alarmOn = true;
                }
                if (GameManager.Instance.Player.alarmOn)
                {
                    GameManager.Instance.Player.Talk_Tile(storynum);
                }
                break;*///그냥 다른 방법 사용//
            default:
                break;
         }           
    }

    private void Update() //알람 끄기 이기에 해당 코드가 다른 코드와 충돌해서 버그가 생길수 있으니 항상 예외처리 잘하기.
    {
        if (!GameManager.Instance.Player.isStory&GameManager.Instance.Player.alarmOn&&Input.GetKeyDown(KeyCode.Space)) //켜진 알람 스페이스로 끄기.
        {
            GameManager.Instance.Player.AlarmOff();
        }
    }
}
