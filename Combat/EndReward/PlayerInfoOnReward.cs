using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoOnReward : MonoBehaviour
{
    public PlayableC character;
    public GameObject Level;
    public GameObject Exp;
    public Image ExpBar;//경험치 바

    public bool levelUp;//레벨업을 했는지 체크
    public GameObject levelUpEffect;//레벨업시 사용할 이펙트(텍스트)
    //public GameObject statIncreaseEffect;//스탯이 증가할때 사용할 이펙트(텍스트)//atk,def,hp,spd
    public GameObject skillUnlockEffect;//스킬이 해금될때 사용할 이펙트(텍스트)
    //// statIncreaseEffect와skillUnlockEffect 오브젝트에 개별적으로 dotween함수 넣기.

    private void OnEnable()
    {
        levelUpEffect.SetActive(false);
        //statIncreaseEffect.SetActive(false);
        skillUnlockEffect.SetActive(false);
        levelUp = false;
    }
    void Update()
    {
        SetInfo();
        CheckLevelUp();
        ExpMaxSet();
        ExpDisplay();
    }

    private void SetInfo()//레벨과 exp를 해당 캐릭터에 맞게 설정
    {
        Level.GetComponent<TMPro.TextMeshProUGUI>().text = character.level.ToString();
        Exp.GetComponent<TMPro.TextMeshProUGUI>().text = ((int)character.exp).ToString() + "/" + ((int)character.maxExp).ToString();
    }
    private void CheckLevelUp()//레벨업을 체크하는 함수
    {
        if(character.level >= 15)//만약 캐릭터으 레벨이 15레벨이면, 경험치가 늘어나지 않고, 그대로 return.
        {
            character.exp = 0;
            return;
        }
        if(character.exp >= character.maxExp)//레벨업시
        {
            character.exp = character.exp - character.maxExp;
            character.level++;
            levelUp = true;
            character.LevelUpStat();//레벨업시 스텟 증가.
            LevelUpEffect(character.LevelUpEffectInfo());//LevelUpEffectinfo에는 어떤스탯이 올랏는지 정보가 있지만, 그냥 스텟 상승 이펙트는 통일하자, 너무 지저분해 보일듯..
        }
    }
    private void LevelUpEffect(int statVar)//레벨업시 이펙트를 보여주는 함수.1:atk 2:def,hp 3:atk,def,hp 4:atk,spd 5:def,hp,spd 6:atk,def,hp,spd
    {
        levelUpEffect.SetActive(true);
        //statIncreaseEffect.SetActive(true);
        CheckSkillUnlock();
    }
    private void ExpMaxSet()
    {
        switch (character.level)
        {
            case 1:
                character.maxExp = 50;
                break;
            case 2:
                character.maxExp = 100;
                break;
            case 3:
                character.maxExp = 200;
                break;
            case 4:
                character.maxExp = 300;
                break;
            case 5:
                character.maxExp = 400;//누적1050
                break;
            case 6:
                character.maxExp = 600;
                break;
            case 7:
                character.maxExp = 800;
                break;
            case 8:
                character.maxExp = 1200;
                break;
            case 9:
                character.maxExp = 1600;//누적5250
                break;
            case 10:
                character.maxExp = 2500;
                break;
            case 11:
                character.maxExp = 3400;
                break;
            case 12:
                character.maxExp = 4300;//누적14450
                break;
            case 13:
                character.maxExp = 5200;
                break;
            case 14:
                character.maxExp = 6100;
                break;
            case 15:
                character.maxExp = 7000;
                break;
            default:
                break;
        }
    }
    private void CheckSkillUnlock()
    {
        if(character.level == 5)
        {
            skillUnlockEffect.SetActive(true);
        }
        if(character.level == 10)
        {
            if(character.name == "Tank")
            {
                return;
            }
            skillUnlockEffect.SetActive(true);
        }
    }
    private void ExpDisplay()
    {
        ExpBar.fillAmount = character.exp / character.maxExp;
    }
}
