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

    public GameObject levelUpEffect;//레벨업시 사용할 이펙트(텍스트)
    public GameObject statIncreaseEffect;//스탯이 증가할때 사용할 이펙트(텍스트)
    public GameObject skillUnlockEffect;//스킬이 해금될때 사용할 이펙트(텍스트)

    // Start is called before the first frame update
    void Update()
    {
        SetInfo();
        CheckLevelUp();
        ExpMaxSet();
    }

    private void SetInfo()//레벨과 exp를 해당 캐릭터에 맞게 설정
    {
        Level.GetComponent<TMPro.TextMeshProUGUI>().text = character.level.ToString();
        Exp.GetComponent<TMPro.TextMeshProUGUI>().text = character.exp.ToString() + "/" + character.maxExp.ToString();
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
            character.LevelUpStat();//레벨업시 스텟 증가.
            LevelUpEffect();
        }
    }
    private void LevelUpEffect()//레벨업시 이펙트를 보여주는 함수
    {
        //levelUpEffect.SetActive(true);
        //levelUpEffect.GetComponent<TMPro.TextMeshProUGUI>().text = "Level Up!";
        //StartCoroutine(EffectOff(levelUpEffect));
    }
    private void ExpMaxSet()
    {
        switch (character.level)
        {
            case 1:
                character.maxExp = 100;
                break;
            case 2:
                character.maxExp = 200;
                break;
            case 3:
                character.maxExp = 400;
                break;
            case 4:
                character.maxExp = 800;
                break;
            case 5:
                character.maxExp = 1600;
                break;
            case 6:
                character.maxExp = 3200;
                break;
            case 7:
                character.maxExp = 6400;
                break;
            case 8:
                character.maxExp = 12800;
                break;
            case 9:
                character.maxExp = 25600;
                break;
            case 10:
                character.maxExp = 51200;
                break;
            case 11:
                character.maxExp = 102400;
                break;
            case 12:
                character.maxExp = 204800;
                break;
            case 13:
                character.maxExp = 409600;
                break;
            case 14:
                character.maxExp = 819200;
                break;
            case 15:
                character.maxExp = 9999;
                break;
            default:
                break;
        }
    }
}
