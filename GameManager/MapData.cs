using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MapData : MonoBehaviour
{
    public int encounterRate;
    public DBManager database;
    public List<TestMob> monsters; //해당 맵에 등장하는 몬스터들.
    public List<TestMob> specialMonsters; //기본 등장 몬스터가 아닌 특수한 조건에서 등장하는 몬스터들.
    public string battleSceneName; //해당 맵에서 전투가 일어날 때 사용할 배틀 씬 이름.

    private void Start() //새로운 씬이 로드되면 해당 맵의 데이터를 매니저에 전달.
    {
        GameManager.Instance.Player.GetComponent<RandomEncounter>().encounterRate = encounterRate;
        GameManager.Instance.mapData = this;
        CombatManager.Instance.mapData = this;
        
    }

    private void Update()
    {
        if (GameManager.Instance.Player == null)
        {
            GameManager.Instance.Player = FindObjectOfType<Player>();
            GameManager.Instance.Player.GetComponent<RandomEncounter>().encounterRate = encounterRate;
        }
    }

    public void GoToBattle()
    {
        Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //이동전 맵이름 플레이어에 받아주기.
        SceneManager.LoadScene(battleSceneName);
    }
}
