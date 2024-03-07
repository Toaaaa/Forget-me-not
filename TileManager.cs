using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour //돈 디스트로이를 하지 않은 특정 이벤트 발생용 타일 매니저
{
    [SerializeField] private Tilemap tilemapforevent; //특정 이벤트를 발생시키기 위한 타일맵
    [SerializeField] private List<TileData> tileDatas;

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


    public bool IsMonsterZone(Vector2 worldPosition)
    {
        Vector3Int gridPosition = tilemapforevent.WorldToCell(worldPosition);

        TileBase tile = tilemapforevent.GetTile(gridPosition);

        if(tile ==null) //타일이 없을때는 false 리턴.
            return false;

        bool ismonsterzone = dataFromTiles[tile].isMonsterZone;

        return ismonsterzone;
    }
}
