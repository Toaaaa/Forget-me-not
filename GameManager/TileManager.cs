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
        GameManager.Instance.textManager.storyScriptPlay(storyNum);
        return;
    }
}
