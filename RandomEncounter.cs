using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEncounter : MonoBehaviour
{
    private Player player;

    [SerializeField] private int encounterStep=10; //ó�� ���� Ÿ�Ͽ� ������ ���� ��ī���Ͱ� �߻��Ҽ� �ִ� �ּ� ����(1���)Ƚ��.
    public int encounterCount=0; //���� ���� Ƚ��.
    public int encounterRate ; //���� ��ī���� Ȯ��. random(1~100) < encounterrate�̸� ���� ��ī����.
    private int extraEncounterRate; //��ī���� ���н� ��ī���� Ȯ���� �ø��� ���� ����.

    public float timeMoved;
    private float encounterTime = 1.2f; //���� ��ī���� �ð� ����.

    private bool checkEncounter; //���� ��ī���� üũ.
    private bool isMonsterZone; //��ī���� �߻��ϴ� Ÿ������ üũ.

    private TileManager tilemanager;

    private void Awake()
    {
        player = GetComponent<Player>();
        tilemanager = FindObjectOfType<TileManager>();
    }


    private void GetTimeMoved() //�÷��̾ �����϶� �ð��� ī����.
    {
        float deltaTime = Time.deltaTime;
        timeMoved += deltaTime;
        if(timeMoved >= encounterTime) //������ �ð��� encountetTime ���� ũ��
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
        if (checkEncounter)//������ �޼� �Ǹ� �� encounterTime ��ŭ �����϶� ���� ��ī���� üũ�� ��.
        {
            if(encounterRate != 0) //Ȥ�� ���Ͱ� �������� �ʴ� ���� ��츦 ����Ͽ� 0�϶��� Ȯ�� �ø�.
            {
                extraEncounterRate += 1; //���� ��ī���� ���н� �߰� Ȯ���� �ø�.
            }
            int random = Random.Range(1, 100);
            if (random < encounterRate + extraEncounterRate)
                Encountered();

            checkEncounter = false;
        }
    }


    private void Encountered()//encounter monster
    {
        if(encounterRate != 0 && CombatManager.Instance.mapData.encounterRate !=0) //������ ������ extraEncounterRate������ 0�� �ƴѰ����� ��ī���� �߻����ɼ� ����.
        {
            Debug.Log(encounterRate +"_"+ extraEncounterRate);
            encounterCount = 0; //���� ��ī���Ͱ� �߻��ϸ� ������ �ʱ�ȭ.
            extraEncounterRate = 0; //���� ��ī���Ͱ� �߻��ϸ� �߰� Ȯ���� �ʱ�ȭ.
            GameManager.Instance.combatManager.OnCombatStart();
        }
    }


    private void Update()
    {
        if(tilemanager != null)
        {
            isMonsterZone = tilemanager.IsMonsterZone(player.transform.position); //�÷��̾ �ִ� Ÿ���� ���� Ÿ������ üũ.
        }
         else 
        {
            tilemanager = FindObjectOfType<TileManager>();
        }

        if (player.isMoving && isMonsterZone)
        {
            GetTimeMoved(); //�÷��̾ �����̸� �ð��� ī������.
            EncounterCheck(); //���� ��ī���� üũ.
        }

        /////////////////////////// �� ���� ���丮 �̺�Ʈ Ÿ��.
        if(tilemanager != null)
        {
            tilemanager.IsStoryTile(player.transform.position); //�÷��̾ �ִ� Ÿ���� ���丮 Ÿ������ üũ.
        }
         else
        {
            tilemanager = FindObjectOfType<TileManager>();
        }
    }

}
