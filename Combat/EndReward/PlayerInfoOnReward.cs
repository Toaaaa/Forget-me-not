using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoOnReward : MonoBehaviour
{
    public PlayableC character;
    public GameObject Level;
    public GameObject Exp;
    public Image ExpBar;//����ġ ��

    public bool levelUp;//�������� �ߴ��� üũ
    public GameObject levelUpEffect;//�������� ����� ����Ʈ(�ؽ�Ʈ)
    //public GameObject statIncreaseEffect;//������ �����Ҷ� ����� ����Ʈ(�ؽ�Ʈ)//atk,def,hp,spd
    public GameObject skillUnlockEffect;//��ų�� �رݵɶ� ����� ����Ʈ(�ؽ�Ʈ)
    //// statIncreaseEffect��skillUnlockEffect ������Ʈ�� ���������� dotween�Լ� �ֱ�.

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

    private void SetInfo()//������ exp�� �ش� ĳ���Ϳ� �°� ����
    {
        Level.GetComponent<TMPro.TextMeshProUGUI>().text = character.level.ToString();
        Exp.GetComponent<TMPro.TextMeshProUGUI>().text = ((int)character.exp).ToString() + "/" + ((int)character.maxExp).ToString();
    }
    private void CheckLevelUp()//�������� üũ�ϴ� �Լ�
    {
        if(character.level >= 15)//���� ĳ������ ������ 15�����̸�, ����ġ�� �þ�� �ʰ�, �״�� return.
        {
            character.exp = 0;
            return;
        }
        if(character.exp >= character.maxExp)//��������
        {
            character.exp = character.exp - character.maxExp;
            character.level++;
            levelUp = true;
            character.LevelUpStat();//�������� ���� ����.
            LevelUpEffect(character.LevelUpEffectInfo());//LevelUpEffectinfo���� ������� �ö����� ������ ������, �׳� ���� ��� ����Ʈ�� ��������, �ʹ� �������� ���ϵ�..
        }
    }
    private void LevelUpEffect(int statVar)//�������� ����Ʈ�� �����ִ� �Լ�.1:atk 2:def,hp 3:atk,def,hp 4:atk,spd 5:def,hp,spd 6:atk,def,hp,spd
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
                character.maxExp = 400;//����1050
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
                character.maxExp = 1600;//����5250
                break;
            case 10:
                character.maxExp = 2500;
                break;
            case 11:
                character.maxExp = 3400;
                break;
            case 12:
                character.maxExp = 4300;//����14450
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
