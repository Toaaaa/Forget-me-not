using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MonsterStatUI : MonoBehaviour//몬스터의 상단부에 표시될 버프 디버프 등의 상태 표시 ui
{
    public TestMob thisMob;
    //총 2+2+1으로 5칸의 버프 디버프 표시 ui.
    public GameObject[] BuffsUI;//버프 표시 UI (attack,defense)//2개밖에 없다고 상정하고 코드 작성//수정시 유의.
    public GameObject[] DebuffsUI;//디버프 표시 UI (defense,speed)
    public GameObject ElementNow;//현재 적용중인 속성 스택 //정속성 스택 변화시(추가데미지 변화) 바뀔때의 효과 와 함께 바뀜.
    public GameObject[] ElementalFX;//스택 변화시 아이콘위치에 적용될 이펙트.

    public Sprite[] BuffsSprites;//버프 스프라이트// 0:공버프 1:방버프 2:방디퍼프 3:속디버프 4:화염스택 5:물스택 6:땅스택
    private SkillType currentSkillT;//현재 적용중인 스킬의 속성.


    // Update is called once per frame
    async void Update()
    {
        if (thisMob.isAtkBuffed)
        {
            if(thisMob.isDefBuffed)//공버프 +방버프
            {
                BuffsUI[0].SetActive(true);//두칸
                BuffsUI[1].SetActive(true);//두칸
                BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격버프
                BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//방어버프
            }
            else//공버프만
            {
                BuffsUI[0].SetActive(true);//한칸
                BuffsUI[1].SetActive(false);
                BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격버프
            }
        }
        else if (thisMob.isDefBuffed)//방버프만
        {
            BuffsUI[0].SetActive(true);//한칸
            BuffsUI[1].SetActive(false);
            BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//방어버프
        }//방버프만
        else
        {
            BuffsUI[0].SetActive(false);
            BuffsUI[1].SetActive(false);
        }//버프X


        if (thisMob.isDefDebuffed)
        {
            if (thisMob.isSpeedDebuffed)//방디버프 + 속디버프
            {
                DebuffsUI[0].SetActive(true);//두칸
                DebuffsUI[1].SetActive(true);//두칸
                DebuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//방디버프
                DebuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속디버프
            }
            else//방디버프만
            {
                DebuffsUI[0].SetActive(true);//한칸
                DebuffsUI[1].SetActive(false);
                DebuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//방디버프
            }
        }
        else if (thisMob.isSpeedDebuffed)//속디버프만
        {
            DebuffsUI[0].SetActive(true);//한칸
            DebuffsUI[1].SetActive(false);
            DebuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속디버프
        }//속디버프만
        else
        {
            DebuffsUI[0].SetActive(false);
            DebuffsUI[1].SetActive(false);
        }//디버프X


        if (thisMob.stackedElement != currentSkillT)
        {
            currentSkillT = thisMob.stackedElement;
            switch (currentSkillT)
            {
                case SkillType.none:
                    ElementNow.SetActive(false);
                    break;
                case SkillType.Fire:
                    ElementNow.SetActive(true);
                    await TurnElementFire();
                    break;
                case SkillType.Water:
                    ElementNow.SetActive(true);
                    await TurnElementWater();
                    break;
                case SkillType.Wood:
                    ElementNow.SetActive(true);
                    await TurnElementWood();
                    break;
            }
        }//스택관련 업데이트 (아이콘 + 아이콘fx)
    }



    private async UniTask TurnElementFire()
    {
        //불꽃 아이콘 효과
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[4];
    }
    private async UniTask TurnElementWater()
    {
        //물 아이콘 효과
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[5];
    }
    private async UniTask TurnElementWood()
    {
        //땅 아이콘 효과
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[6];
    }

}
