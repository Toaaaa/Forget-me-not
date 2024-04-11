using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;
    public MapData mapData;
    public CombatDisplay combatDisplay; //���� ui�� ���� ����.

    public List<PlayableC> playerList;//���� ������ �������� �÷��̾�(����� ���� ��������.)
    public List<TestMob> monsterList;//������ ������ ���͵� << ���⿡ �ִ� ���͸� ���� �ش� ������ ��ų�� ���
    public Dictionary<TestMob, GameObject> monstersInCombat; //������ �����ϴ� ���͵�� �� ������Ʈ�� ��Ī��Ű�� ��ųʸ�.
    public List<GameObject> monsterObject; //���� ������Ʈ�� ���� ����Ʈ.


    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //������������ �ѹ��� �ϳ��� ���� �ǵ���. //���࿡ �ٸ� ���� ����߿� ������������ ��� �� ��� ���� ������ �����.
    public bool isAtkDebuff; //���ݷ� ������� �������� ���üũ >> �̸� ���� ������ ������ ����� ����.
    //���� ������ ����߿��� ��Ƽ���� �ο����� �Ұ���.
    public bool isBoss; //������������ �ƴ��� �Ǻ��ϴ� ����.
    


    private void Start()
    {
        playerList = new List<PlayableC>();
        playerList = playableManager.joinedPlayer;
        
    }
    public void OnCombatStart()//���� ���۽� ȣ��Ǵ� �Լ�.
    { 
        playerList = playableManager.joinedPlayer;
        monsterList = isBoss ? mapData.specialMonsters : mapData.monsters;
        combatDisplay.playerList = playerList;
        GoToFightScene();//���� ������ �Ѿ�� �Լ�. (�ش� �ʿ� �´� ���� �޹������ �̵���)
        updateMonster();
        //...����ui �� �Ѿ�� �Լ� �߰�.

    }
    public void OnCombatEnd() //���� ����� ����� ������ ������ ����.
    {
        monstersInCombat.Clear();
        monsterObject.Clear();

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk +10);
            isAtkDebuff = false;
        }

    }

    private void Update()
    {
        timerDelta();
        if(BuffIsOn&&consumeTimer == 0)
        {
            consumeOnUse.OnEnd();
            BuffIsOn = false;
            consumeOnUse = null;
        }
    }

    private void updateMonster()
    {
        for (int i = 0; i < monsterList.Count; i++)
        {
            var obj = Instantiate(monsterList[i].gameObject, new Vector3(0, 0, 0), Quaternion.identity);
            monsterObject.Add(obj);
            monstersInCombat.Add(monsterList[i], monsterObject[i]);
        }
    }

    private void timerDelta() //deltatime���� ���� consumeTimer�� �ð��� ��� �Լ�.
    {
        if(consumeTimer > 0)
        {
            consumeTimer -= Time.deltaTime;
        }
        else
        {
            consumeTimer = 0;

        }
    }
    private void StartCombat()
    {

    }
    private void monsterDie()
    {

    }
    private void DebuffDamageCount() //���� �� ����Ŭ�� ���������� ����� �������� ����ϴ� �Լ�.
    {
        for(int i=0; i<playerList.Count; i++)
        {
            if (playerList[i].isPoisoned)
            {
                playerList[i].hp -= 10;
            }
        }
    }
    private void GoToFightScene() //�ش� ���� ���� �� �ʿ� �ش�Ǵ� ���� ������ �̵�. (�� �������� ���� ������ ����.)
    {
        //mapdata���� ������ �޾� �ش� ������ �̵�.
    }
}



