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

    public GameObject levelUpEffect;//�������� ����� ����Ʈ(�ؽ�Ʈ)
    public GameObject statIncreaseEffect;//������ �����Ҷ� ����� ����Ʈ(�ؽ�Ʈ)
    public GameObject skillUnlockEffect;//��ų�� �رݵɶ� ����� ����Ʈ(�ؽ�Ʈ)

    // Start is called before the first frame update
    void Update()
    {
        SetInfo();
        CheckLevelUp();
        ExpMaxSet();
    }

    private void SetInfo()//������ exp�� �ش� ĳ���Ϳ� �°� ����
    {
        Level.GetComponent<TMPro.TextMeshProUGUI>().text = character.level.ToString();
        Exp.GetComponent<TMPro.TextMeshProUGUI>().text = character.exp.ToString() + "/" + character.maxExp.ToString();
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
            character.LevelUpStat();//�������� ���� ����.
            LevelUpEffect();
        }
    }
    private void LevelUpEffect()//�������� ����Ʈ�� �����ִ� �Լ�
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
