using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MapData : MonoBehaviour
{
    public int encounterRate;
    public DBManager database;
    public bool isBossMap;
    public List<TestMob> monsters; //�ش� �ʿ� �����ϴ� ���͵�.
    public List<TestMob> specialMonsters; //�⺻ ���� ���Ͱ� �ƴ� Ư���� ���ǿ��� �����ϴ� ���͵�.
    public List<GameObject> StoryObject;//���丮�� ���� ��Ȳ � ���� Ȱ��ȭ/��Ȱ��ȭ�� �� �� ���� ������Ʈ or Ÿ��.
    public string battleSceneName; //�ش� �ʿ��� ������ �Ͼ �� ����� ��Ʋ �� �̸�.
    public Vector3 playerPosition; //�÷��̾ �ش� �ʿ� ó�� ������ �� ��ġ�� ��ǥ. (�÷��̾�� �Ⱥ��̰� �Ұ�.)

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
            GameManager.Instance.Player = FindObjectOfType<Player>();
            GameManager.Instance.Player.GetComponent<RandomEncounter>().encounterRate = encounterRate;
        }

    }

    public void GoToBattle()
    {
        Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //�̵��� ���̸� �÷��̾ �޾��ֱ�.
        SceneChangeManager.Instance.battleSceneName = battleSceneName;
        CombatManager.Instance.battleSceneName = battleSceneName;
        Player.Instance.combatPosition = playerPosition;
        SceneChangeManager.Instance.ChangeBattleScene();
    }

}
