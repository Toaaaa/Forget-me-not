using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour //�� ��Ʈ���̸� ���� ���� Ư�� �̺�Ʈ �߻��� Ÿ�� �Ŵ��� >> �� ������ ��ġ���༭ �ʸ��� �ٸ� �� ��Ͷ��� ���Ⱑ���ϰ� �Ұ�.
{
    [SerializeField] private Tilemap tilemapforevent; //Ư�� �̺�Ʈ�� �߻���Ű�� ���� Ÿ�ϸ�
    [SerializeField] private Tilemap tilemapforStory; //���丮 ������ ���� Ÿ�ϸ� //storyscriptalbe�� ���� �������, ��ũ��Ʈ ����̺�Ʈ ������ ���� Ÿ��.
    [SerializeField] private List<TileData> tileDatas; //���� ���������� ����ִ� Ÿ�� ������ ��ũ���ͺ� ������Ʈ.


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

        TileBase tile = tilemapforevent.GetTile(gridPosition);//�ش� Ÿ�ϸ��� �׸��� �����ǿ��� Ÿ���� ��������.

        if(tile ==null) //Ÿ���� �������� false ����. (ismonsterzone�� ���� Ÿ��) �̶�� false�� ��.
            //(��Ȯ���� ������ ������ tilemapforevent�� ��ϵ� Ÿ�ϸ��� Ÿ���� �ƴҰ��.
            return false;

        bool ismonsterzone = dataFromTiles[tile].isMonsterZone; //datafromtiles���� tile�� Ű�� ������ (tiledata��ũ��Ʈ��)�������� ismonsterzone�� ������.

        return ismonsterzone;
    }


    public void IsStoryTile(Vector2 worldPosition) //�÷��̾��� ��ġ�� �־ �ش���ġ�� Ÿ���� ��ũ��Ʈ Ÿ���� ��� �ش� Ÿ���� �����͸� ������ ���丮Ÿ�������� Ȯ��.
    {
        Vector3Int gridPosition = tilemapforStory.WorldToCell(worldPosition);

        TileBase tile = tilemapforStory.GetTile(gridPosition);//�ش� Ÿ�ϸ��� �׸��� �����ǿ��� Ÿ���� ��������.

        if (tile == null) //Ÿ���� �������� false ����. (ismonsterzone�� ���� Ÿ��) �̶�� false�� ��.
            return;
        int storyNum = dataFromTiles[tile].storyNum;
        if (!dataFromTiles[tile].isStoryTile) //���丮Ÿ���� �ƴ� ������ ����(ssobj ���� ����) Ÿ���� ���.
        {
            DataTile(storyNum);
        }
        else
        {
            GameManager.Instance.textManager.storyScriptPlay(storyNum);
        }
        return;
    }


    void DataTile(int storynum) //update�� ��� ��µǰ� ����.
    {
         switch (storynum)//���丮 �ѹ� Ÿ�Ͽ� ����, �ڵ� ssobj ������ ���� OR �̺�Ʈ �߻�. (ex. �˶� ���)
         {
            case 0:
                //���������� �����Ҷ�(istage1completed�� �ƴҶ���), (�̺�Ʈ ��ũ��Ʈtextbox�� ���� "����δ� �� �ʿ䰡 ������ ����" ��� )
                if (!GameManager.Instance.storyScriptable.isStage1Completed&&GameManager.Instance.Player.h>0)
                {
                    GameManager.Instance.Player.h = 0;
                    GameManager.Instance.Player.alarmOn = true;
                }
                if (GameManager.Instance.Player.alarmOn)
                {
                    GameManager.Instance.Player.ShowAlarm(storynum,2);//�˶� ���.(Ÿ���� �ִϸ��̼�)
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
                    GameManager.Instance.Player.v = 0;
                    GameManager.Instance.Player.alarmOn = true;
                }
                if(GameManager.Instance.Player.alarmOn)
                {
                    GameManager.Instance.Player.ShowAlarm(storynum,1);
                }
                break;
            default:
                break;
         }           
    }

    private void Update() //�˶� ���� �̱⿡ �ش� �ڵ尡 �ٸ� �ڵ�� �浹�ؼ� ���װ� ����� ������ �׻� ����ó�� ���ϱ�.
    {
        if (!GameManager.Instance.Player.isStory&GameManager.Instance.Player.alarmOn&&Input.GetKeyDown(KeyCode.Space)) //���� �˶� �����̽��� ����.
        {
            GameManager.Instance.Player.AlarmOff();
        }
    }
}
