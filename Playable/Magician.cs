using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Magician", menuName = "PlayableCharacter/Magician")]
public class Magician : PlayableC
{

    override public void Attack()
    {
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk , this.crit);
        Debug.Log("마법사의 기본 공격");
    }
    override public void Skill1() //블레이즈
    {
        for(int i =0; i<CombatManager.Instance.monsterList.Count; i++) //모든 몬스터에게 1.5배의 공격력으로 공격
        {
            CombatManager.Instance.monsterList[i].Hp -= CheckCrit(atk * 1.5f, this.crit);
        }
    }
    override public void Skill2() //모든 플레이어들 치명타 확률 증가
    {
        for(int i = 0; i < CombatManager.Instance.playerList.Count; i++)
        {
            CombatManager.Instance.playerList[i].crit += 15;
            CombatManager.Instance.playerList[i].critBuff = true;
        }
    }
    override public void Skill3() //속도 감소
    {//코스트 상 (적의 속도를 감소키기기에 밸류가 높음)
        for (int i = 0; i < CombatManager.Instance.monsterObject.Count; i++)
        {
            if (CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isslowed == false)
            {
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Speed -= 3;
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().isslowed = true;
            }
            else
            {
                Debug.Log("이미 속도가 감소되어 있습니다.");
                CombatManager.Instance.monsterObject[i].GetComponent<TestMob>().Speed -= 1;
            }
        }
    }
    override public void Skill4() //피어싱 라이트닝. 3연속 관통 공격. (단일기) (빛의 봉인검 비주얼)
    { //>>높은 데미지 높은 코스트
        CombatManager.Instance.monsterSelected.GetComponent<TestMob>().Hp -= CheckCrit(atk * 3.5f,this.crit);
    }
    
}

