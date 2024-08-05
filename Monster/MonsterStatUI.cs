using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MonsterStatUI : MonoBehaviour//몬스터의 상단부에 표시될 버프 디버프 등의 상태 표시 ui
{
    public TestMob thisMob;
    //총 3+1으로 4칸의 버프 디버프 표시 ui.
    public GameObject[] BuffsUI;//버프 표시 UI (attack,defense,speed)//3개밖에 없다고 상정하고 코드 작성//수정시 유의.
    public GameObject ElementNow;//현재 적용중인 속성 스택 //정속성 스택 변화시(추가데미지 변화) 바뀔때의 효과 와 함께 바뀜.
    public GameObject[] ElementalFX;//스택 변화시 아이콘위치에 적용될 이펙트. 0:화염, 1:물, 2:땅

    public Sprite[] BuffsSprites;//버프 스프라이트// 0:공버프 1:방버프 2:방디퍼프 3:속디버프 4:화염스택 5:물스택 6:땅스택
    private SkillType currentSkillT;//현재 적용중인 스킬의 속성.


    // Update is called once per frame
    async void Update()
    {
        //////완전 수작업 방식 공,방,속도,속성 4가지상태에 대해서만 표시하는 경우의 ui
        if (thisMob.isAtkBuffed)//공격력 아이콘 사용시
        {
            if (thisMob.isDefBuffed || thisMob.isDefDebuffed)//방어력 관련 아이콘 사용시
            {
                if (thisMob.isDefBuffed)//방어력 버프 사용시
                {
                    if (thisMob.isSpeedDebuffed)//속도 디버프 사용시
                    {
                        //공버프 + 방어력 버프 + 속도 디버프
                        BuffsUI[0].SetActive(true);//세칸
                        BuffsUI[1].SetActive(true);//세칸
                        BuffsUI[2].SetActive(true);//세칸
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격력버프
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//방어력버프
                        BuffsUI[2].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속도디버프
                    }
                    else//속도 디버프 사용 안할때
                    {
                        //공버프 + 방어력버프
                        BuffsUI[0].SetActive(true);//두칸
                        BuffsUI[1].SetActive(true);//두칸
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격력버프
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//방어력버프
                    }
                }
                else//방어력 디버프 사용시
                {
                    if (thisMob.isSpeedDebuffed)//속도 디버프 사용시
                    {
                        //공버프 + 방어력 디버프 + 속도 디버프
                        BuffsUI[0].SetActive(true);//세칸
                        BuffsUI[1].SetActive(true);//세칸
                        BuffsUI[2].SetActive(true);//세칸
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격력버프
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//방어력디버프
                        BuffsUI[2].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속도디버프
                    }
                    else//속도 디버프 사용 안할때
                    {
                        //공버프+ 방어력 디버프
                        BuffsUI[0].SetActive(true);//두칸
                        BuffsUI[1].SetActive(true);//두칸
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격력버프
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//방어력디버프
                    }
                }
            }
            else//방어력 관련 아이콘 사용 안할때
            {
                if (thisMob.isSpeedDebuffed)//속도 관련 아이콘 사용시
                {
                    //공버프 + 속도 디버프
                    BuffsUI[0].SetActive(true);//두칸
                    BuffsUI[1].SetActive(true);//두칸
                    BuffsUI[2].SetActive(false);
                    BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격력버프
                    BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속도디버프
                }
                else
                {
                    //공버프만 있을때
                    BuffsUI[0].SetActive(true);//한칸
                    BuffsUI[1].SetActive(false);
                    BuffsUI[2].SetActive(false);
                    BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//공격력버프
                }
            }
        }
        else//공버프 없을때
        {
            if (thisMob.isDefBuffed || thisMob.isDefDebuffed)//방어력 관련 아이콘 사용시
            {
                if(thisMob.isDefBuffed)//방어력 버프 사용시
                {
                    if(thisMob.isSpeedDebuffed)//속도 디버프 사용시
                    {
                        //방어력 버프 + 속도 디버프
                        BuffsUI[0].SetActive(true);//두칸
                        BuffsUI[1].SetActive(true);//두칸
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//방어력버프
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속도디버프
                    }
                    else//속도 디버프 사용 안할때
                    {
                        //방어력 버프만
                        BuffsUI[0].SetActive(true);//한칸
                        BuffsUI[1].SetActive(false);
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//방어력버프
                    }
                }
                else//방어력 디버프 사용시
                {
                    if(thisMob.isSpeedDebuffed)//속도 디버프 사용시
                    {
                        //방어력 디버프 + 속도 디버프
                        BuffsUI[0].SetActive(true);//두칸
                        BuffsUI[1].SetActive(true);//두칸
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//방어력디버프
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속도디버프
                    }
                    else//속도 디버프 사용 안할때
                    {
                        //방어력 디버프만
                        BuffsUI[0].SetActive(true);//한칸
                        BuffsUI[1].SetActive(false);
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//방어력디버프
                    }
                }
            }
            else//방어력 관련 아이콘도 사용 안할때
            {
                if(thisMob.isSpeedDebuffed)//속도 관련 아이콘 사용시
                {
                    //속도 디버프만
                    BuffsUI[0].SetActive(true);//한칸
                    BuffsUI[1].SetActive(false);
                    BuffsUI[2].SetActive(false);
                    BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//속도디버프
                }
                else//아무런 버프 디버프도 없을때
                {
                    //아무런 아이콘 없음
                    BuffsUI[0].SetActive(false);
                    BuffsUI[1].SetActive(false);
                    BuffsUI[2].SetActive(false);
                }
            }
        }//공버프 없을때의 경우

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
        ElementalFX[0].SetActive(true);
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[4];
        await UniTask.Delay(300);
        ElementalFX[0].SetActive(false);
    }
    private async UniTask TurnElementWater()
    {
        //물 아이콘 효과
        ElementalFX[1].SetActive(true);
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[5];
        await UniTask.Delay(300);
        ElementalFX[1].SetActive(false);
    }
    private async UniTask TurnElementWood()
    {
        //땅 아이콘 효과
        await UniTask.Delay(800);
        ElementalFX[2].SetActive(true);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[6];
        await UniTask.Delay(300);
        ElementalFX[2].SetActive(false);
    }

}
