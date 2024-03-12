using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    private Player player;

    [SerializeField] private int encounterStep=10; //ó�� ���� Ÿ�Ͽ� ������ ���� ��ī���Ͱ� �߻��Ҽ� �ִ� �ּ� ����(1���)Ƚ��.
    [SerializeField] private int encounterCount=0; //���� ���� Ƚ��.
    [SerializeField] private int encounterRate = 9; //���� ��ī���� Ȯ��. random(1~100) < encounterrate�̸� ���� ��ī����.

    private float timeMoved;
    private float encounterTime = 1.2f; //���� ��ī���� �ð� ����.

    private bool checkEncounter; //���� ��ī���� üũ.
    private bool isMonsterZone; //��ī���� �߻��ϴ� Ÿ������ üũ.

    private TileManager tilemanager;

    private void Awake()
    {
        player = GetComponent<Player>();
        tilemanager = FindObjectOfType<TileManager>();
        encounterRate = GameManager.Instance.eventManager.encounterRate;
    }


    private void GetTimeMoved() //�÷��̾ �����϶� �ð��� ī����.
    {
        float deltaTime = Time.deltaTime;
        timeMoved += deltaTime;
        if(timeMoved >= encounterTime)
        {
            timeMoved = 0;
            encounterCount++; //every "encounterTime" seconds, it counts as 1 step.

            if (encounterCount >= encounterStep) //it starts to check encounterrate.
            {
                checkEncounter = true;//���� ��ī���� �Լ��� ����.
            }
        }
    }

    private void EncounterCheck()
    {
        if (checkEncounter)
        {
            int random = Random.Range(1, 100);
            if (random < encounterRate)
                Encountered();

            checkEncounter = false;
        }
    }


    private void Encountered()//encounter monster
    {
        encounterCount = 0; //���� ��ī���Ͱ� �߻��ϸ� ������ �ʱ�ȭ.
        GameManager.Instance.eventManager.encounterEvent(); //Start the encounter event.
    }


    private void Update()
    {
        
        if(tilemanager != null)
        {
            isMonsterZone = tilemanager.IsMonsterZone(player.transform.position); //�÷��̾ �ִ� Ÿ���� ���� Ÿ������ üũ.
        }
         else { Debug.Log("TileManager is null");}

        if (player.isMoving && isMonsterZone)
        {
            GetTimeMoved(); //�÷��̾ �����̸� �ð��� ī������.
            EncounterCheck(); //���� ��ī���� üũ.
        }

    }

}
