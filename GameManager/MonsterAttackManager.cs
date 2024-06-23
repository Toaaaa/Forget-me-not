using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

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
        if (combatManager.isCombatStart)
        {
            if (monsterAttackAvailable)
            {
                if (playerTurnUsed >= 6)
                {
                    if (!isAttacking)
                    {
                        MonsterSpecialAttack();//is attacking�� �����Ϸ��� monsterSpecialAttack�ȿ� monsterattack�� �־�� �ҵ�.
                        playerTurnUsed -= 6;
                        monsterAttackAvailable = false;
                    }                   
                }
                else//3~5�Ͻð� ���� �Ϲ� ��������.
                {
                    if (!isAttacking)
                    {
                        MonsterAttack();
                        playerTurnUsed -= 3;
                        monsterAttackAvailable = false;
                    }
                }
            }
            else if (!combatDisplay.isPlayerTurn && combatManager.isCombatStart)//������ ���۵Ǿ��µ� �÷��̾��� ���� �ƴϰ� �Ǿ�����
            {
                if (combatManager.monsterTurnTime > 0 && !isAttacking)
                    MonsterExtraAttack();

                if (combatManager.monsterTurnTime <= 0)
                {
                    combatDisplay.isPlayerTurn = true;
                    combatManager.timerSet();

                }
            }

            if (playerTurnUsed >= 3 && combatManager.monsterTurnTime > 0)
            {
                monsterAttackAvailable = true;
            }
            else if(playerTurnUsed >= 3 && combatManager.monsterTurnTime <= 0) //�÷��̾��� ���� ���� ���ڸ��� ���������� ������ �ٷ� ����Ǵ°� ����.
            {
                playerTurnUsed = 0;
            }
        }
    }
    private async void AttackPattern(TestMob monster)
    {
        monster.target = null;
        if (!combatManager.isAggroOn)
        {
            //�÷��̾���� ü���� 30���� ������ Ÿ���� ������� Ÿ������.
            for (int i = 0; i < combatManager.playerList.Count; i++)
            {
                if (combatManager.playerList[i].hp <= combatManager.playerList[i].maxHp * 0.3f)
                {
                    if (!combatManager.playerList[i].isDead)
                    {
                        monster.target = combatManager.playerList[i];
                        break;
                    }
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
            for(int i = 0; i<combatManager.playerList.Count; i++)
            {
                if (!combatManager.playerList[i].isDead)
                {
                    monster.target = combatManager.playerList[i];
                    break;
                }
            }
            return;
        }

        if(monster.Hp >= monster.MaxHp * 0.8f)//������ ü���� 80% �̻��϶��� ������ ��ų�� ���.
        {
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            await AttackingDone();
        }
        else if(combatManager.alivePlayerCount == 1)//�÷��̾ 1�� ��������� ������ ��ų�����
        {
            Debug.Log("�÷��̾ 1�� ���������");
            monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            await AttackingDone();
        }
        else
        {
            monster.monsterSkill[Random.Range(0, monster.monsterSkill.Count)].UseSkill(monster);
            await AttackingDone();
        }
    }


    private async void MonsterAttack()
    {
        Debug.Log("MonsterAttack");
        isAttacking = true;
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        if(!monster.isDead)
        {
            combatManager.monsterTurnTime -= 3;
            await AttackStartDelay(monster);
            return;
        }
        else
        {
            MonsterAttack();
        }

    }
    private async void MonsterExtraAttack()//�÷��̾��� ���� ������ ���� ���� �Ͽ��� ����Ǵ� ����.
    {
        Debug.Log("MonsterExtraAttack");
        isAttacking = true;
        TestMob monster = monsters[Random.Range(0, monsters.Count)];
        if (!monster.isDead)
        {
            combatManager.monsterTurnTime -= 3;
            await AttackStartExtraDelay(monster);
            return;
        }
        else
        {
            MonsterExtraAttack();
        }
    }

    private async void MonsterSpecialAttack()
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
            await SpecialAttackStartDelay(0.7f, monster); //��Ÿ�� 6�� �̻��Ͻ� 0.7�� ������ �� Ư������ ����.
           
        }
    }

    private async UniTask SpecialAttackStartDelay(float delay, TestMob monster)
    {
        await UniTask.Delay((int)(delay * 1000));
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

    private void OnDisable()
    {
        monsters.Clear();
    }
    private async UniTask AttackStartDelay(TestMob monster)
    {
        await UniTask.Delay(600);
        AttackPattern(monster);
    }
    private async UniTask AttackStartExtraDelay(TestMob monster)//�÷��̾��� ���� ������ ���� ���� �Ͽ��� ����Ǵ� ����.
    {
        await UniTask.Delay(900);
        AttackPattern(monster);
    }


    private async UniTask AttackingDone() //������ useskill�� ����� ���ÿ� ����Ǵ� �½�ũ.
    {
        await UniTask.Delay(800);
        isAttacking = false;
    }
    
}
