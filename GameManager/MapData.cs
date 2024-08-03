using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;



public class MapData : MonoBehaviour
{
    public int encounterRate;
    public DBManager database;
    public bool isBossMap;
    public List<TestMob> monsters; //해당 맵에 등장하는 몬스터들.
    public List<TestMob> specialMonsters; //기본 등장 몬스터가 아닌 특수한 조건에서 등장하는 몬스터들.
    public List<GameObject> StoryObject;//스토리의 진행 상황 등에 따라 활성화/비활성화를 해 줄 맵의 오브젝트 or 타일.
    public string battleSceneName; //해당 맵에서 전투가 일어날 때 사용할 배틀 씬 이름.
    public Vector3 playerPosition; //플레이어가 해당 맵에 처음 들어왔을 때 위치할 좌표. (플레이어는 안보이게 할것.)

    private void Start()
    {
        GameManager.Instance.Player.GetComponent<RandomEncounter>().encounterRate = encounterRate;
        GameManager.Instance.mapData = this;
        CombatManager.Instance.mapData = this;
    }

   

    private void Update()
    {
        if (GameManager.Instance.Player == null)
        {
            GameManager.Instance.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            GameManager.Instance.Player.GetComponent<RandomEncounter>().encounterRate = encounterRate;
        }
        GameManager.Instance.virtualCamera.m_Lens.OrthographicSize = 5;
    }

    public void GoToBattle()
    {
        Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //이동전 맵이름 플레이어에 받아주기.
        SceneChangeManager.Instance.battleSceneName = battleSceneName;
        CombatManager.Instance.battleSceneName = battleSceneName;
        Player.Instance.combatPosition = playerPosition;
        GameManager.Instance.Camera.GetComponent<PixelPerfectCamera>().enabled = false;//전투 씬에서는 픽셀퍼펙트 비활성화
        SceneChangeManager.Instance.ChangeBattleScene();
    }

}
