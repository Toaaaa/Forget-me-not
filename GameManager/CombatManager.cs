using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;
    public MapData mapData;
    public CombatDisplay combatDisplay; //���� ui�� ���� ����.

    public List<PlayableC> playerList;//���� ������ �������� �÷��̾�(����� ���� ��������.)
    public List<TestMob> monsterList;//������ ������ ���͵� << ���⿡ �ִ� ���͸� ���� �ش� ������ ��ų�� ��� (�̰Ŵ� prefab�� ��ũ��Ʈ�� ���� �ϴ°����� ����)
    public Dictionary<TestMob, GameObject> monstersInCombat = new Dictionary<TestMob, GameObject>(); //������ �����ϴ� ���͵�� �� ������Ʈ�� ��Ī��Ű�� ��ųʸ�.
    public List<GameObject> monsterObject; //���� ������Ʈ�� ���� ����Ʈ.
    public List<GameObject> monsterAliveList; //����ִ� ���� ����Ʈ.


    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //������������ �ѹ��� �ϳ��� ���� �ǵ���. //���࿡ �ٸ� ���� ����߿� ������������ ��� �� ��� ���� ������ �����.
    public bool isAtkDebuff; //���ݷ� ������� �������� ���üũ >> �̸� ���� ������ ������ ����� ����.
    //���� ������ ����߿��� ��Ƽ���� �ο����� �Ұ���.
    public bool isBoss; //������������ �ƴ��� �Ǻ��ϴ� ����.   
    public bool isCombatStart; //������ ���۵Ǿ����� �Ǻ��ϴ� ����.
    public string battleSceneName; //���� ���� �̸��� �����ϴ� ����.

    //ui
    public bool isFirstSelection; //ó�� ����â�� �����ִ���.
    public PlayableC selectedPlayer; //���õ� �÷��̾�. (��ų�� ����Ҷ� ���)
    public GameObject monsterSelected; //����,��ų�� ����� ������ ����.//������ ��� mob.target ���� ������ Ư�����ǿ� �´� ��� ����.

    //turn
    public float playerTurnTime; //�÷��̾��� �� �ð�.
    public float monsterTurnTime; //������ �� �ð�.
    float tempMonst;



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
        combatDisplay.gameObject.SetActive(true);
        combatDisplay.playerList = playerList;
        timerStart();//iscombatstart���� �̸� �Ͻð� ����.
        isCombatStart = true;
        mapData.GoToBattle();
        Player.Instance.combatPosition = mapData.playerPosition;
        Player.Instance.CombatPositioning();


        //...����ui �� �Ѿ�� �Լ� �߰�.

    }
    public void OnCombatEnd() //���� ����� ����� ������ ������ ����.
    {
        Debug.Log("������ ����Ǿ����ϴ�.");
        for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
        }
        monstersInCombat.Clear();
        monsterObject.Clear();
        monsterAliveList.Clear();       
        selectedPlayer = null;
        monsterSelected = null;

        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].resetStat();
        }//������ ������ �ִ� hp, ġ��Ÿ, ���ݷ�, ���� ���� ������ �ʱ�ȭ.

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk +10);
            isAtkDebuff = false;
        }
        //�÷��̾��� ��ų ������ ���� ������ �ش� ������ ����. (���� ��Ÿ �����鵵 �� ���� �Ǵ��� Ȯ��.)
        isCombatStart = false;
        SceneManager.LoadScene(Player.Instance.currentMapName);//������ ������ ���� ������ ���ư��� �Լ�.
        combatDisplay.gameObject.SetActive(false);
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
        //���Ͱ� ���� �׾����� oncombatend�� ��������ִ� �Լ�.
        for(int i = 0; i < monsterObject.Count; i++)
        {
            if (monsterObject[i].GetComponent<TestMob>().Hp <= 0)
            {
                monsterDie(i);
                break;
            }
        }//������ ü���� 0���ϰ� �Ǹ� ������� + ����� ���͸� ����Ʈ,monsterobject���� ����.
        if(isCombatStart)//������ ���۵Ǿ�����
        {
            if (monsterAliveList.Count == 0)
            {
                Debug.Log("���Ͱ� �����Ͽ����ϴ�.");
                OnCombatEnd();
            }//���� �����.           

            PlayerTimerDelta();
            //MoblistSet();
            if(playerTurnTime == 0)
            {
                if(monsterTurnTime == 0)
                {
                    timerSet();
                }//�÷��̾�� ������ ���� ��� �Ҹ�Ǿ�����, Ÿ�̸� ����.
                else
                {
                    combatDisplay.isPlayerTurn = false;
                    //�� �Բ� ������ �������� �Ҹ�.
                }
            }
            else 
            {
                combatDisplay.isPlayerTurn = true;
            }//playerTurnTime != 0
            if (combatDisplay.attackSelected /*||skillSelected*/)
            {
                monsterSelected = monsterObject[combatDisplay.selectedMobIndex];
            }
            else
            {
                monsterSelected = null;
            }
        }
    }

    public void updateMonster()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != battleSceneName)
        {
            Debug.Log("�ٸ���.");
            return;
        }
        for (int i = 0; i < monsterList.Count; i++)
        {
            var obj = Instantiate(monsterList[i].gameObject, combatDisplay.mobSlotList[i].transform.position, Quaternion.identity,GameObject.Find("Combat system").transform);
            monsterObject.Add(obj);
            combatDisplay.mobSlotList[i].GetComponent<MobSlot>().monster = obj;
            monsterAliveList.Add(obj);
            monstersInCombat.Add(monsterList[i], monsterObject[i]);
        }
        combatDisplay.MobList.Clear();

        for (int i = 0; i < monsterObject.Count; i++) //�Ĺ� �Ŵ����� ���� ������Ʈ�� �Ĺ� ���÷����� ���͸���Ʈ���� �߰�.
        {
            combatDisplay.MobList.Add(monsterObject[i]);
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
    private void PlayerTimerDelta()
    {
        if(playerTurnTime > 0)
        {
            playerTurnTime -= Time.deltaTime;
        }
        else
        {
            playerTurnTime = 0;
        }
    }
    private void timerStart()//���� ó�� ���� ���۽��� Ÿ�̸�.
    {
        tempMonst = 0;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {
                playerTurnTime +=1.5f* playerList[i].spd;
            }
        }
        for (int i = 0; i < monsterList.Count; i++)
        {
            tempMonst +=1.5f* monsterList[i].GetComponent<TestMob>().Speed;
        }
        monsterTurnTime = tempMonst;
    }
    private void timerSet()
    {
        tempMonst = 0;
        for(int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {               
                playerTurnTime += 1.5f*playerList[i].spd;
            }
        }
        for(int i = 0; i < monsterObject.Count; i++)
        {
            tempMonst += 1.5f*monsterObject[i].GetComponent<TestMob>().Speed;
        }
        monsterTurnTime = tempMonst;
    }//�÷��̾�� ������ �ӵ��� ���� �Ͻð��� �������ִ� �Լ�.
    private void StartCombat()
    {

    }
    private void monsterDie(int num)
    {
        Debug.Log("���Ͱ� �׾����ϴ�.");
        monsterObject[num].GetComponent<TestMob>().isDead = true;
        monsterAliveList.Remove(monsterObject[num]);
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



    public void isAttackSelected(PlayableC player)
    {

    }
    public void MoblistSet()
    {
            for (int i = 0; i < monsterObject.Count; i++)
            {
                combatDisplay.mobSlotList[i].GetComponent<MobSlot>().monster = monsterObject[i];
            }
    }
}



