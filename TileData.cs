using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;

    public bool isMonsterZone;
    public bool isStoryTile;//이게 false 일 경우 data값을 세팅해주는 타일임.
    public int storyNum;

}
