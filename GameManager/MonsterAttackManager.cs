using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using Cysharp.Threading.Tasks.Triggers;
using System.Linq;

public class MonsterAttackManager : MonoBehaviour
{
    Scene currentScene;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;
    public List<TestMob> monsters;

    [SerializeField]
    List<Image> monsterTurnCard;//������ ���� �� Ƚ���� �ð������� �����ִ� ī��. 0~9����
    int monsterTurnCount;//������ �� ī�� ���� int��.
    public int SpecialCardStack;//���� �ܿ� �� ī�尡 ���� ��� Ư��ī�� ��ȯ ������ �����Ͽ� ��ī�尡 �����ɶ� �����.

    public bool monsterAttackAvailable; //������ ������ �������� �Ǻ��ϴ� ����. (�÷��̾ 3�Ͻð� �̻��� ����Ͽ�����)
    public float playerTurnUsed; //�÷��̾��� �Ͻð� ���� ���ð�. ���� ������ �ϸ� �����ϱ�.

    public bool isAttacking;//���� ���϶�����ϴ� bool. >> ���� �ִϸ��̼��� ������ false�� ������. +Ư�� ������ ��� �ϴ��� MonsterSkill.@@@ ���� false�� ������.

    private void Update()
    {

        if(monsters.Count == 0) //��ϵ� ���Ͱ� �����Ǿ��� ��� �ٽ� ���
        {
            for (int i = 0; i < combatManager.monsterObject.Count; i++)
            {
                monsters.Add(combatManager.monsterObject[i].GetComponent<TestMob>());
            }
        }

        if (combatManager.isCombatStart)
        {
            if (monsterAttackAvailable)
            {
                if (playerTurnUsed >= 6)
                {
                    if (!isAttacking)
                    {
                        //MonsterSpecialAttack();//is attacking�� �����Ϸ��� monsterSpecialAttack�ȿ� monsterattack�� �־�� �ҵ�.
                        MonsterSpecialCard();//������ �� ī���� �ϳ��� Ư��ī��� ����.
                        playerTurnUsed -= 3;
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

            if (playerTurnUsed >= 3 && monsterTurnCount > 0) //combatManager.monsterTurnTime ���� monsterturncount �� ����. ī�尡 ������ ���Ŀ� ���Ͱ� �ൿ�� �����آZ���� ��.
            {
                monsterAttackAvailable = true;
            }
            else if(playerTurnUsed >= 3 && combatManager.monsterTurnTime <= 0) //�÷��̾��� ���� ���� ���ڸ��� ���������� ������ �ٷ� ����Ǵ°� ����.
            {
                playerTurnUsed = 0;
            }
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
        else if (combatManager.isAggroOn && combatManager.tank.isDead)//��׷ΰ� �����ְ� ��Ŀ�� �׾�������
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
        if (monster.target.isDead)//���� ���õ� �÷��̾ �̹� ����� ���¶�� �ٽ� Ÿ�� ����.
        {
            for (int i = 0; i < combatManager.playerList.Count; i++)
            {
                if (!combatManager.playerList[i].isDead)
                {
                    monster.target = combatManager.playerList[i];
                    break;
                }
            }
            return;
        }

        if (monster.Hp >= monster.MaxHp * 0.8f)//������ ü���� 80% �̻��϶��� ������ ��ų�� ���.
        {
            CardUse();//������ �� ī�� ȿ��.
            if (monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardIsSpecial)//Ư��ī�尡 ���Ǿ�����
            {
                MonsterSpecialAttack();//Ư�� ����
            }
            else
            {
                monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            }
            monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().IsCardOn = false;//ī�� ����� ī�带 ���� ���·� ����.
        }
        else if (combatManager.alivePlayerCount == 1)//�÷��̾ 1�� ��������� ������ ��ų�����
        {
            CardUse();//������ �� ī�� ȿ��.
            if (monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardIsSpecial)//Ư��ī�尡 ���Ǿ�����
            {
                MonsterSpecialAttack();//Ư�� ����
            }
            else
            {
                monster.monsterOnlyAttack[Random.Range(0, monster.monsterOnlyAttack.Count)].UseSkill(monster);
            }
            monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().IsCardOn = false;//ī�� ����� ī�带 ���� ���·� ����.
        }
        else//�׿��� ��Ȳ������ ��� ��ų�� ���.
        {
            CardUse();//������ �� ī�� ȿ��.
            if (monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardIsSpecial)//Ư��ī�尡 ���Ǿ�����
            {
                MonsterSpecialAttack();//Ư�� ����
            }
            else
            {
                monster.monsterSkill[Random.Range(0, monster.monsterSkill.Count)].UseSkill(monster);
            }
            monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().IsCardOn = false;//ī�� ����� ī�带 ���� ���·� ����.
        }
    }


    private async void MonsterAttack()
    {
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
            MonsterAttack();//���� ���Ͱ� ���õǾ��� ���, ��õ�.
        }

    }
    private async void MonsterExtraAttack()//�÷��̾��� ���� ������ ���� ���� �Ͽ��� ����Ǵ� ����.
    {
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
            MonsterExtraAttack();//���� ���Ͱ� ���õǾ��� ���, ��õ�.
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
        else //�������� �ƴҶ��� Ư������
        {
            await SpecialAttackStartDelay(0.3f, monster);
           
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
                monster.monsterSkill[0].BattleCry(monster);//������ �Լ�
                break;
            case "battle in stage2":

                break;
            case "battle in stage3":
                break;
            case "battle in stage4"://���ռ�
                break;
        }

    }


    public async void MonsterTurnCardSet()//���� ��Ÿ���� ���� �Ҷ� ī�尡 ���õǴ� ȿ��.
    {
        await UniTask.Delay(1800);//������ ����� ī�尡 ���Ǵ� ȿ���� ��ġ�� �ʰ� �����̸� ��.
        monsterTurnCount = combatManager.monsterTurnTime / 3;
        monsterTurnCount += (combatManager.monsterTurnTime%3) == 0 ? 0 : 1;//��Ÿ���� 3�� ����� ��� ���°�, �� �̻��� ��� ī�� ���� �߰�.
        for(int i=0; i< monsterTurnCount; i++)
        {
            monsterTurnCard[i].GetComponent<MonsterCardEffect>().CardReset();//ī�尡 �����Ǵ� ȿ��.
        }
        SpecialCardStackUse();//Ư��ī�� ������ ����ϴ� �Լ�.
    }
    public async void DeadMonsterTurnCardSet()//���Ͱ� �׾����� �ܿ� �� ī���� ������ �����ϴ� �Լ�.
    {
        await UniTask.Delay(600);
        int tempCount = combatManager.GetNewTurnTime() / 3;//GetNewTurnTime() == ���� ������ ��Ÿ���� ������ ������ ��Ÿ��.
        tempCount += (combatManager.GetNewTurnTime() % 3) == 0 ? 0 : 1;
        if(tempCount < monsterTurnCount)
        {
            for (int i = tempCount; i < monsterTurnCount; i++)
            {
                monsterTurnCard[i].GetComponent<MonsterCardEffect>().CardUsed();//ī�尡 ���Ǵ� ȿ��.
            }
            monsterTurnCount = tempCount;
        }
    }
    public void CardUse()//������ ���� ����Ҷ� ī�尡 ���Ǵ� ȿ��.
    {
        monsterTurnCount--;
        monsterTurnCard[monsterTurnCount].GetComponent<MonsterCardEffect>().CardUsed();//ī�尡 ���Ǵ� ȿ��.
    }
    private void MonsterSpecialCard()//�ܿ� ���� �� ī���� �ϳ��� Ư��ī��� ����.
    {
        if (monsterTurnCount == 0)
        {
            SpecialCardStack++; // ������ �ܿ� �� ī�尡 ���� ��� ������ �׾Ƶ�
            return;
        }

        //�ܿ� �� ī���� Ư��ī�尡 �ƴ� ī�常 ���͸�
        var nonSpecialCards = monsterTurnCard
            .Where(card => !card.GetComponent<MonsterCardEffect>().CardIsSpecial) //Ư��ī�尡 �ƴ� ī�常 ���͸�
            .ToList(); // ���͸��� ī�带 ����Ʈ�� ��ȯ

        if (nonSpecialCards.Count == 0)
        {
            SpecialCardStack++; // ������ �� �ִ� �ܿ� ī�尡 ���� ��� ���� �װ� ����
            return;
        }

        // �������� �ϳ��� ī�带 Ư�� ī��� ����
        int x = Random.Range(0, nonSpecialCards.Count);
        nonSpecialCards[x].GetComponent<MonsterCardEffect>().SpecialCard();
    }
    private void SpecialCardStackUse()//Ư��ī�� ������ ����ϴ� �Լ�. (�� ī�� ���½� ����ϴ� �Լ�)
    {
        for(int i=0; i<SpecialCardStack; i++)
        {
            SpecialCardStack--;
            MonsterSpecialCard();
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
        await UniTask.Delay(600);
        AttackPattern(monster);
    }


    
}
