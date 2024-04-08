using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MapData : MonoBehaviour
{
    public int encounterRate;
    public DBManager database;
    public List<TestMob> monsters; //�ش� �ʿ� �����ϴ� ���͵�.
    public List<TestMob> specialMonsters; //�⺻ ���� ���Ͱ� �ƴ� Ư���� ���ǿ��� �����ϴ� ���͵�.

    private void Start() //���ο� ���� �ε�Ǹ� �ش� ���� �����͸� �Ŵ����� ����.
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
