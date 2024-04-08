using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapData : MonoBehaviour
{
    public int encounterRate;
    public DBManager database;
    public List<TestMob> monsters; //해당 맵에 등장하는 몬스터들.
    public List<TestMob> specialMonsters; //기본 등장 몬스터가 아닌 특수한 조건에서 등장하는 몬스터들.

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

}
