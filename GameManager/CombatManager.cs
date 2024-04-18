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
    public List<TestMob> monsterList;//������ ������ ���͵� << ���⿡ �ִ� ���͸� ���� �ش� ������ ��ų�� ���
    public Dictionary<TestMob, GameObject> monstersInCombat = new Dictionary<TestMob, GameObject>(); //������ �����ϴ� ���͵�� �� ������Ʈ�� ��Ī��Ű�� ��ųʸ�.
    public List<GameObject> monsterObject; //���� ������Ʈ�� ���� ����Ʈ.


    public GameObject monsterSelected; //����,��ų�� ����� ������ ����.//������ ��� mob.target ���� ������ Ư�����ǿ� �´� ��� ����.
    public ConsumeItem consumeOnUse;
    public float consumeTimer;
    public bool BuffIsOn; //������������ �ѹ��� �ϳ��� ���� �ǵ���. //���࿡ �ٸ� ���� ����߿� ������������ ��� �� ��� ���� ������ �����.
    public bool isAtkDebuff; //���ݷ� ������� �������� ���üũ >> �̸� ���� ������ ������ ����� ����.
    //���� ������ ����߿��� ��Ƽ���� �ο����� �Ұ���.
    public bool isBoss; //������������ �ƴ��� �Ǻ��ϴ� ����.
    
    public bool isCombatStart; //������ ���۵Ǿ����� �Ǻ��ϴ� ����.
    public string battleSceneName; //���� ���� �̸��� �����ϴ� ����.


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
        isCombatStart = true;
        mapData.GoToBattle();
        Player.Instance.combatPosition = mapData.playerPosition;
        Player.Instance.CombatPositioning();
        //...����ui �� �Ѿ�� �Լ� �߰�.

    }
    public void OnCombatEnd() //���� ����� ����� ������ ������ ����.
    {
        Debug.Log("������ ����Ǿ����ϴ�.");
        monstersInCombat.Clear();
        monsterObject.Clear();

        if (isAtkDebuff)
        {
            playerList.ForEach(x => x.atk = x.atk +10);
            isAtkDebuff = false;
        }

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
        if(Input.GetKeyDown(KeyCode.F5))//�׽�Ʈ�� �ڵ�(0�� ���Ϳ��� �÷��̾ ��ų�� ���)
        {
            monsterSelected = monstersInCombat[monsterList[0]];
            playerList[0].Skill1();
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            playerList[0].hp -=5;
            Debug.Log("�÷��̾� ü�� ����");
        }
        //���Ͱ� ���� �׾����� oncombatend�� ��������ִ� �Լ�.
        for(int i = 0; i < monsterObject.Count; i++)
        {
            if (monsterObject[i].GetComponent<TestMob>().Hp <= 0)
            {
                monsterDie(i);
                break;
            }
        }
        if(isCombatStart &&monsterObject.Count == 0)
        {
            Debug.Log("���Ͱ� �����Ͽ����ϴ�.");
            OnCombatEnd();
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
        Debug.Log(scene.name);
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
    private void monsterDie(int num)
    {
        Debug.Log("���Ͱ� �׾����ϴ�.");
        monsterObject.RemoveAt(num);
        monstersInCombat.Remove(monsterList[num]);
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
}



