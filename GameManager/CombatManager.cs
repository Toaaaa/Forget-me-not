using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombatManager : Singleton<CombatManager>
{
    [SerializeField]
    PlayableManager playableManager;
    public DamagePrintManager damagePrintManager;
    public TurnTimeUsedShow turnTimeUsedShow;
    public CombatTimer combatTimer;
    public MonsterAttackManager monsterAttackManager;
    public MapData mapData;
    public CombatDisplay combatDisplay; //���� ui�� ���� ����.
    public GameObject mobplace;
    public SlotPlacement slotPlacement;
    public GameObject EndOfJourney;

    public List<PlayableC> playerList;//���� ������ �������� �÷��̾�(����� ���� ��������.)
    //public List<PlayableC> alivePlayerList;//���� ������ �������� ����ִ� �÷��̾�.
    public List<TestMob> monsterList;//������ ������ ���͵� << ���⿡ �ִ� ���͸� ���� �ش� ������ ��ų���ø� ���� (�̰Ŵ� prefab�� ��ũ��Ʈ�� ���� �ϴ°����� ����)
    public Dictionary<TestMob, GameObject> monstersInCombat = new Dictionary<TestMob, GameObject>(); //������ �����ϴ� ���͵�� �� ������Ʈ�� ��Ī��Ű�� ��ųʸ�.
    public List<GameObject> monsterObject; //���� ������Ʈ�� ���� ����Ʈ.
    public List<GameObject> monsterAliveList; //����ִ� ���� ����Ʈ.

    public float DeadMobExpCount; //���� �������� ���� ���͵��� �� ����ġ.
    public int DeadMobGoldCount; //���� �������� ���� ���͵��� �� ���.
    public List<Item> DeadMobItemDrop; //���� �������� ���� ���͵��� ����� �����۵�.
    public int alivePlayerCount; //����ִ� �÷��̾��� ��.
    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //������������ �ѹ��� �ϳ��� ���� �ǵ���. //���࿡ �ٸ� ���� ����߿� ������������ ��� �� ��� ���� ������ �����.
    public bool isAtkDebuff; //���ݷ� ������� �������� ���üũ >> �̸� ���� ������ ������ ����� ����.
    //���� ������ ����߿��� ��Ƽ���� �ο����� �Ұ���.
    public bool isBoss; //������������ �ƴ��� �Ǻ��ϴ� ����.   
    public bool isCombatStart; //������ ���۵Ǿ����� �Ǻ��ϴ� ����.
    public string battleSceneName; //���� ���� �̸��� �����ϴ� ����.
    public PlayableC tank;
    public bool isAggroOn; //��Ŀ�� ��׷ΰ� �����ִ���. //���� ����������� ������ ����� �׻� ��Ŀ��.

    //ui
    public bool isFirstSelection; //ó�� ����â�� �����ִ���.
    public PlayableC selectedPlayer; //���õ� �÷��̾�. (��ų+�������� ����Ҷ� ���) << �̰� combatdisplay���� ���.
    public GameObject monsterSelected; //����,��ų�� ����� ������ ����.//������ ��� mob.target ���� ������ Ư�����ǿ� �´� ��� ����.

    //turn
    public float playerTurnTime; //�÷��̾��� �� �ð�.
    public float monsterTurnTime; //������ �� �ð�.
    public float playerNoAttackTime; //�÷��̾ �ƹ��� ���þ��� 6�ʵ��� �ð��� �������, �ڵ����� ���� ���� ������ ���� ����.
    public int attackCostTime; //�⺻������ �ڽ�Ʈ �ð�unw.
    public int skillCostTime; //��ų ����� �ڽ�Ʈ �ð�.
    public int itemCostTime; //������ ����� �ڽ�Ʈ �ð�.
    public int fleeCostTime; //���������� �ڽ�Ʈ �ð�.
    public PlayableC lastAction; //���������� �ൿ�� �÷��̾�. (�Ƿε� �ý����� ���� ����)

    float tempMonst;



    private void Start()
    {
        playerList = new List<PlayableC>();
        playerList = playableManager.joinedPlayer;
    }
    public void OnCombatStart()//���� ���۽� ȣ��Ǵ� �Լ�.
    { 
        Player.Instance.wasInCombat = true;
        playerList = playableManager.joinedPlayer;
        //alivePlayerList = playableManager.joinedPlayer;
        /*for(int i = 0; i < alivePlayerList.Count; i++)
        {
            if (alivePlayerList[i].isDead)
            {
                alivePlayerList.Remove(alivePlayerList[i]);
            }
        }//���� ���۽� ����ִ� �÷��̾� ����Ʈ�� ����� �Լ�.*/
        isBoss = mapData.isBossMap;
        monsterList = isBoss ? mapData.specialMonsters : mapData.monsters;
        combatDisplay.playerList = playerList;
        combatDisplay.isPlayerTurn = true;
        combatDisplay.playerList = playerList;
        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetStat();
        }
        ResetPlayerBuff();
        mapData.GoToBattle();


        //...����ui �� �Ѿ�� �Լ� �߰�.

    }
    public void OnCombatEnd() //���� ����� ����� ������ ������ ����. (flee �ÿ��� �ٷ� �ش� �Լ��� ȣ��)
    {
        Debug.Log("������ ����Ǿ����ϴ�.");
        for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
            Debug.Log("���� ������Ʈ ����");
        }
        
        slotPlacement = null;
        combatDisplay.inAction = false;
        monstersInCombat.Clear();
        monsterObject.Clear();
        monsterAliveList.Clear();       
        selectedPlayer = null;
        monsterSelected = null;

        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetStat();
        }//������ ������ �ִ� hp, ġ��Ÿ, ���ݷ�, ���� ���� ������ �ʱ�ȭ.

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk +10);
            isAtkDebuff = false;
        }
        ResetPlayerBuff();
        ReviveIfDead();
        //�÷��̾��� ��ų ������ ���� ������ �ش� ������ ����. (���� ��Ÿ �����鵵 �� ���� �Ǵ��� Ȯ��.)
        isCombatStart = false;
        SceneChangeManager.Instance.LeaveBattleScene();
    }
    public void OnCombatLost() //�������� �й��ҽ�.
    {
        /*for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
        }
        monsterObject.Clear();*///���⼭ �����ع����� combatend�� ������ ������ ����. scenechangemanager���� �����ϵ��� ����.
        DeadMobItemDrop.Clear();
        DeadMobGoldCount = 0;
        DeadMobExpCount = 0;
        combatDisplay.inAction = false;
        monstersInCombat.Clear();
        monsterAliveList.Clear();
        selectedPlayer = null;
        monsterSelected = null;

        for (int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetStat();
        }//������ ������ �ִ� hp, ġ��Ÿ, ���ݷ�, ���� ���� ������ �ʱ�ȭ.

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk + 10);
            isAtkDebuff = false;
        }
        ResetPlayerBuff();
        //�÷��̾��� ��ų ������ ���� ������ �ش� ������ ����. (���� ��Ÿ �����鵵 �� ���� �Ǵ��� Ȯ��.)
        isCombatStart = false;
        SceneChangeManager.Instance.BlackOutEndJourney();//���ƿ� �� ������ �� ȭ�� Ȱ��ȭ.

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
        for(int i = 0; i < monsterObject.Count; i++)
        {
            if (monsterObject[i].GetComponent<TestMob>().Hp <= 0)
            {
                monsterDie(i);
            }
        }//������ ü���� 0���ϰ� �Ǹ� ������� + ����� ���͸� ����Ʈ,monsterobject���� ����.
        if(isCombatStart) //������ ���۵Ǿ�����
        {
            PlayerDieCheck();//�÷��̾��� ������θ� üũ�ϴ� �Լ�.+ ����� ������ �ӽ÷� ����
            MonsterAllDeadCount();//���� ����� combat �����Ű�� �޼���.
            alivePlayerCount = AliveCounting(); //����ִ� �÷��̾��� ���� ���� �Լ�.
            PlayerTimerDelta();
            PlayerNoAttackTime();
            TankChecking();
            //MoblistSet();
            if(playerTurnTime <= 0)
            {
                if(monsterTurnTime <= 0)
                {
                    combatDisplay.isPlayerTurn = true;
                    timerSet();
                }//�÷��̾�� ������ ���� ��� �Ҹ�Ǿ�����, Ÿ�̸� ����.
                else
                {
                    //�ð��� ���� ������ combatdisplay.isplayerturn = false; ���� �ٲپ� ������ ���� ����.
                    //�÷��̾��� ���� �ൿ �ִϸ��̼ǰ� ��ġ�� �ʵ��� �ϴ°�.
                    combatDisplay.isPlayerTurn = false;
                    //�� �Բ� ������ �������� �Ҹ�., ismonsterturn = true; ���Բ� true�϶� ����Ǵ� �Լ��� ����?
                }
            }
            else 
            {
                combatDisplay.isPlayerTurn = true;
            }//playerTurnTime != 0
            if (combatDisplay.attackSelected ||combatDisplay.skillSelected)
            {
                monsterSelected = monsterAliveList[combatDisplay.selectedMobIndex];
            }
            else
            {
                monsterSelected = null;
            }
        }
    }

    public void updateMonster()//incombatscene���� �ش� �Լ� ȣ���Ͽ� ���.
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name != battleSceneName)
        {
            Debug.Log("�ٸ���.");
            return;
        }
        for (int i = 0; i < monsterList.Count; i++)
        {
            var obj = Instantiate(monsterList[i].gameObject, slotPlacement.M_slotPlace[i].transform.position, Quaternion.identity);
            monsterObject.Add(obj);
            combatDisplay.mobSlotList[i].GetComponent<MobSlot>().monster = obj;
            monsterAliveList.Add(obj);
            monstersInCombat.Add(monsterList[i], monsterObject[i]);
        }
        combatDisplay.MobList.Clear();
        monsterAttackManager.monsters.Clear();

        for (int i = 0; i < monsterObject.Count; i++) //�Ĺ� �Ŵ����� ���� ������Ʈ�� �Ĺ� ���÷����� ���͸���Ʈ���� �߰�.
        {
            combatDisplay.MobList.Add(monsterObject[i]);
        }
        for(int i =0; i< monsterObject.Count; i++)
        {
            monsterAttackManager.monsters.Add(monsterObject[i].GetComponent<TestMob>());
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
    private void PlayerNoAttackTime()
    {
        playerNoAttackTime += Time.deltaTime;
        if(playerNoAttackTime >= 6 && monsterTurnTime>0)
        {
            monsterAttackManager.playerTurnUsed += 3;//�÷��̾ 6�ʰ� �ƹ��� �ൿ�� ���� ������ playerturnused�� 3�ʸ� �߰��Ͽ� ������ ���� ����.
            playerNoAttackTime = 0;
        }
    }

    public void timerStart()//���� ó�� ���� ���۽��� Ÿ�̸�.
    {
        tempMonst = 0;
        playerTurnTime = 0;
        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {
                playerTurnTime +=1.5f* playerList[i].spd;
            }
        }
        playerTurnTime += 3f;
        if(GameManager.Instance.storyScriptable.isOnStage2 && !GameManager.Instance.storyScriptable.isOnStage3 && !GameManager.Instance.storyScriptable.Stage2Extra3)
        {
            playerTurnTime = playerTurnTime * 0.7f;
        }//���� �ٶ����� ����Ʈ�� ��ġ�� ���ѻ��·� �������� 2�� �ִ� ��� speed�� 30���� ����.
        CombatTimerSet();
        for (int i = 0; i < monsterList.Count; i++)
        {
            tempMonst +=1f* monsterList[i].GetComponent<TestMob>().monster.mSpeed;
        }
        monsterTurnTime = tempMonst;
    }
    public void timerSet()//���� ����� �ɶ� ����ϴ� �Լ�.
    {
        //������ �ӵ��� �ּҼӵ����� ������� �ּҼӵ��� ����. �������� �ӵ����� ��ų�� ������ ����.
        for (int i =0; i<monsterAliveList.Count; i++) 
        {
            TestMob m =monsterAliveList[i].GetComponent<TestMob>();
            if (m.Speed < m.MinimumSpeed)
            {
                monsterAliveList[i].GetComponent<TestMob>().Speed = m.MinimumSpeed;
            }
        }

        tempMonst = 0;
        playerTurnTime = 0;
        for(int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {               
                playerTurnTime += 1.5f*playerList[i].spd;
            }
        }
        playerTurnTime += 2.5f;
        if (GameManager.Instance.storyScriptable.isOnStage2 && !GameManager.Instance.storyScriptable.isOnStage3 && !GameManager.Instance.storyScriptable.Stage2Extra3)
        {
            playerTurnTime = playerTurnTime * 0.7f;
        }//���� �ٶ����� ����Ʈ�� ��ġ�� ���ѻ��·� �������� 2�� �ִ� ��� speed�� 30���� ����.
        CombatTimerSet();
        for (int i = 0; i < monsterAliveList.Count; i++)
        {
            tempMonst += 1* monsterAliveList[i].GetComponent<TestMob>().Speed;
        }
        monsterTurnTime = tempMonst;

        for (int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {
                combatDisplay.selectedSlotIndex = i;
                combatDisplay.slotList[i].combatSelection.charSelection.SetActive(true);
                isFirstSelection = false;
                combatDisplay.attackSelected = false;
                combatDisplay.skillSelected = false;
                combatDisplay.skillSelectedForPlayer = false;
                combatDisplay.skillForAllMob = false;
                combatDisplay.skillForAllPlayer = false;
                combatDisplay.skillForMe = false;
                combatDisplay.itemSelected = false;
                break;
            }
        }//���������� ��� ��ġ �ʱ�ȭ.
    }//�÷��̾�� ������ �ӵ��� ���� �Ͻð��� �������ִ� �Լ�.
    private void StartCombat()
    {

    }   
    private void monsterDie(int num)
    {
        combatDisplay.MobList.RemoveAt(num);
        monsterObject[num].GetComponent<TestMob>().Hp = 0.1f;//hp�� 0���ϰ� �Ǹ� ��� trigger ȣ���� �Ǽ� ��� �ִϸ��̼��� ���峲. ���� ������� �ٷ� hp 0.1����
        Debug.Log("���Ͱ� �׾����ϴ�.");
        monsterObject[num].GetComponent<TestMob>().isDead = true;
        monsterObject[num].GetComponent<MonsterAnimatorController>().Death("DEATH").Forget();//���� ������� ��� ��� ����.
    }
    private void PlayerDieCheck()//���� isdead������ ����ȭ�� �ƴ�. �ִϸ��̼��� �Ķ���͸� isdead�� �����Ͽ� ���� �ִϸ��̼��� �����ϵ��� �����Ұ�.
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].hp <= 0 /*&& !playerList[i].isDead*/)
            {
                playerList[i].isDead = true;
                /*for (int j = 0; j < alivePlayerList.Count; j++)
                {
                    if (alivePlayerList[j].isDead)
                    {
                        alivePlayerList.Remove(alivePlayerList[j]);
                    }
                }*/
            }
            if (playerList[i].isDead)
            {
                combatDisplay.slotList[i].GetComponent<Image>().color= new Color32(255, 0, 0, 255);
                playerList[i].fatigue = 0;
                playerList[i].isTired = false;
            }
            if (!playerList[i].isDead)
            {
                combatDisplay.slotList[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }//�÷��̾��� ü���� 0���ϰ� �Ǹ� �������.
        if(playerList.TrueForAll(x => x.isDead == true))
        {
            Debug.Log("��Ƽ�� �����Ͽ����ϴ�.");
            OnCombatLost();
        }//��Ƽ�� ��� �ο��� ����� ��������.

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

    private void ResetPlayerBuff()
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            playerList[i].ResetBUff();
        }
        isAggroOn = false;

    }
    private void ReviveIfDead()//������ ���� �÷��̾� ü�� 1�� ����α�.
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].isDead)
            {
                playerList[i].isDead = false;
                playerList[i].hp = 1;
            }
        }
    }
    private void MonsterAllDeadCount()
    {
        if (monsterAliveList.Count == 0&&!playerList.TrueForAll(x => x.isDead == true))//endofjourney�� ��Ȱ��ȭ �Ǿ��������� ����.
        {
            Debug.Log("���Ͱ� �����Ͽ����ϴ�.");
            //���⼭ ���������� ����ġ ������ ���� ������ Ȯ�� + �������� �������� ��ų �رݵ��� �ؽ�Ʈ ��� �Լ� + ���Լ��� ������ oncombatend����.
            OnCombatEnd();
        }
    }

    private int AliveCounting()
    {
        int alivePlayerCount=0;

        for(int i = 0; i < playerList.Count; i++)
        {
            if (!playerList[i].isDead)
            {
                alivePlayerCount++;
            }
        }
        return alivePlayerCount;
    }
    private void TankChecking()
    {
        for(int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].name == "Tank")
            {
                tank = playerList[i];
            }
        }
    }
    private void CombatTimerSet()
    {
        combatTimer.maxTime = playerTurnTime;
    }
    public void LostCombatMobClear()
    {
        for (int i = 0; i < monsterObject.Count; i++)
        {
            Destroy(monsterObject[i]);
        }
        monsterObject.Clear();
    }
    public void TimerShake()//�׼� �ڽ�Ʈ ��� ��Ÿ���� �����Ҷ� Ÿ�̸� ��鸲 ȿ��.
    {
        combatTimer.gameObject.transform.DOPunchPosition(new Vector3(5.5f, 0, 0), 0.6f, 10, 1);
    }
}



