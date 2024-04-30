using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterAttackManager : MonoBehaviour
{
    Scene currentScene;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public List<TestMob> monsters;

    public bool monsterAttackAvailable; //������ ������ �������� �Ǻ��ϴ� ����. (�÷��̾ 3�Ͻð� �̻��� ����Ͽ�����)
    public int playerTurnUsed; //�÷��̾��� �Ͻð� ���� ���ð�. ���� ������ �ϸ� �����ϱ�.

    private void Update()
    {
        if(monsterAttackAvailable)
        {
                if(playerTurnUsed<6)
                {
                    MonsterSpecialAttack();
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
            MonsterAttack();
        }

        if(playerTurnUsed >= 3)
        {
            monsterAttackAvailable = true;
        }
    }

    private void MonsterAttack()
    {
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        Debug.Log("������ ����");
        if(!monster.isDead)
        {
            monster.target = combatManager.playerList[Random.Range(0, combatManager.playerList.Count)];
            monster.monsterSkill[Random.Range(0,monster.monsterSkill.Count)].UseSkill(monster);
            return;
        }
        else
        {
            MonsterAttack();
            Debug.Log("������ �õ��Ͽ����ϴ�. �ٽ� ���ݿ����� �����մϴ�.");
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
