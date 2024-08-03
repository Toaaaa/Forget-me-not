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
            GameManager.Instance.Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            GameManager.Instance.Player.GetComponent<RandomEncounter>().encounterRate = encounterRate;
        }
        GameManager.Instance.virtualCamera.m_Lens.OrthographicSize = 5;
    }

    public void GoToBattle()
    {
        Player.Instance.currentMapName = SceneManager.GetActiveScene().name; //�̵��� ���̸� �÷��̾ �޾��ֱ�.
        SceneChangeManager.Instance.battleSceneName = battleSceneName;
        CombatManager.Instance.battleSceneName = battleSceneName;
        Player.Instance.combatPosition = playerPosition;
        GameManager.Instance.Camera.GetComponent<PixelPerfectCamera>().enabled = false;//���� �������� �ȼ�����Ʈ ��Ȱ��ȭ
        SceneChangeManager.Instance.ChangeBattleScene();
    }

}
