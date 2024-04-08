using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour //�� ��Ʈ���̸� ���� ���� Ư�� �̺�Ʈ �߻��� Ÿ�� �Ŵ��� >> �� ������ ��ġ���༭ �ʸ��� �ٸ� �� ��Ͷ��� ���Ⱑ���ϰ� �Ұ�.
{
    [SerializeField] private Tilemap tilemapforevent; //Ư�� �̺�Ʈ�� �߻���Ű�� ���� Ÿ�ϸ�
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


    public bool IsMonsterZone(Vector2 worldPosition) //�÷��̾��� ��ġ�� �־ �ش���ġ�� Ÿ���� ��ũ��Ʈ Ÿ���� ��� �ش� Ÿ���� �����͸� ������ ������������ Ȯ��.
    {
        Vector3Int gridPosition = tilemapforevent.WorldToCell(worldPosition);

        TileBase tile = tilemapforevent.GetTile(gridPosition);

        if(tile ==null) //Ÿ���� �������� false ����. (ismonsterzone�� ���� Ÿ��) �̶�� false�� ��.
            return false;

        bool ismonsterzone = dataFromTiles[tile].isMonsterZone; //datafromtiles���� tile�� Ű�� ������ (tiledata��ũ��Ʈ��)�������� ismonsterzone�� ������.

        return ismonsterzone;
    }
}
