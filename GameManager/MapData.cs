using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MapData : MonoBehaviour
{
    public int encounterRate;
    public DBManager database;
    public List<TestMob> monsters; //�ش� �ʿ� �����ϴ� ���͵�.
    public List<TestMob> specialMonsters; //�⺻ ���� ���Ͱ� �ƴ� Ư���� ���ǿ��� �����ϴ� ���͵�.
    public string battleSceneName; //�ش� �ʿ��� ������ �Ͼ �� ����� ��Ʋ �� �̸�.

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

    public void GoToBattle()
    {
        Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //�̵��� ���̸� �÷��̾ �޾��ֱ�.
        SceneManager.LoadScene(battleSceneName);
    }
}
