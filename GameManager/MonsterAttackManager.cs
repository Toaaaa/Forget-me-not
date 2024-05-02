using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAttackManager : MonoBehaviour
{
    Scene currentScene;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public List<TestMob> monsters;

    public bool monsterAttackAvailable; //������ ������ �������� �Ǻ��ϴ� ����. (�÷��̾ 3�Ͻð� �̻��� ����Ͽ�����)
    public float playerTurnUsed; //�÷��̾��� �Ͻð� ���� ���ð�. ���� ������ �ϸ� �����ϱ�.

    public bool isAttacking;//���� ���϶�����ϴ� bool.

    private void Update()
    {
        if(monsterAttackAvailable)
        {
                if(playerTurnUsed>=6)
                {
                    MonsterSpecialAttack();//is attacking�� �����Ϸ��� monsterSpecialAttack�ȿ� monsterattack�� �־�� �ҵ�.
                    MonsterAttack();
                    playerTurnUsed -= 6;
                    monsterAttackAvailable = false;
                }
                else//3~5�Ͻð� ���� �Ϲ� ��������.
                {
                    MonsterAttack();
                    playerTurnUsed -= 3;
                    monsterAttackAvailable = false;
                }
        }
        else if(!combatDisplay.isPlayerTurn && combatManager.isCombatStart)//������ ���۵Ǿ��µ� �÷��̾��� ���� �ƴϰ� �Ǿ�����
        {
            if(combatManager.monsterTurnTime >0)
                MonsterAttack();
            if(combatManager.monsterTurnTime <= 0)
            {
                combatDisplay.isPlayerTurn = true;
                combatManager.timerSet();

            }
        }

        if(playerTurnUsed >= 3)
        {
            monsterAttackAvailable = true;
        }
    }
    private void AttackPattern(TestMob monster)
    {
        monster.target = null;
        if (!combatManager.isAggroOn)
        {
            //�÷��̾���� ü���� 30���� ������ Ÿ���� ������� Ÿ������.
            for (int i = 0; i < combatManager.playerList.Count; i++)
            {
                if (combatManager.playerList[i].hp <= combatManager.playerList[i].maxHp * 0.3f)
                {
                    monster.target = combatManager.playerList[i];
                    break;
                }
            }
            //���� 30���� ������ Ÿ���� ���ٸ�.
            if (monster.target == null)
            {
                monster.target = combatManager.playerList[Random.Range(0, combatManager.playerList.Count)];
            }
        }
        else if(combatManager.isAggroOn&&combatManager.tank.isDead)//��׷ΰ� �����ְ� ��Ŀ�� �׾�������
        {
            combatManager.isAggroOn = false;
            if (monster.target == null)
            {
                monster.target = combatManager.playerList[Random.Range(0, combatManager.playerList.Count)];
            }
        }
        else
        {
            monster.target = combatManager.tank; //��׷ΰ� ���������� ��Ŀ�� Ÿ������.
        }
        if(monster.target.isDead)//���� ���õ� �÷��̾ �̹� ����� ���¶�� �ٽ� Ÿ�� ����.
        {
            AttackPattern(monster);
            return;
        }
        if(monster.Hp >= monster.MaxHp * 0.8f)//������ ü���� 80% �̻��϶��� ������ ��ų�� ���.
        {
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
        }
        else if(combatManager.alivePlayerCount == 1)//�÷��̾ 1�� ��������� ������ ��ų�����
        {
            Debug.Log("�÷��̾ 1�� ���������");
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
        }
        else
        {
            monster.monsterSkill[Random.Range(0, monster.monsterSkill.Count)].UseSkill(monster);
        }
    }


    private void MonsterAttack()
    {
        combatManager.monsterTurnTime -= 3;
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        Debug.Log("������ ����");
        if(!monster.isDead)
        {
            AttackPattern(monster);
            return;
        }
        else
        {
            MonsterAttack();
        }

    }
    private void MonsterSpecialAttack()
    {
        Debug.Log("������ Ư������");
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        currentScene = SceneManager.GetActiveScene();
        if (combatManager.isBoss)//�������� Ư������
        {
            switch (currentScene.name)
            {
                case "bossbattle in stage1"://1�� �� ����
                    break;
                case "bossbattle in stage2":
                    break;
                case "bossbattle in stage3":
                    break;
                case "bossbattle in stage4"://���ռ� ����
                    break;
            }
        }
        else //�Ϲ� ������ Ư�� ����
        {
            switch (currentScene.name)
            {
                case "battle in stage0"://Ʃ�丮���
                    monster.monsterSkill[0].BattleCry(monster);//������ �Լ�
                    break;
                case "battle in stage1":
                    break;
                case "battle in stage2":
                    break;
                case "battle in stage3":
                    break;
                case "battle in stage4"://���ռ�
                    break;
            }
        }
        //���⿡ �ڷ�ƾ..?
    }


}
