using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MonsterStatUI : MonoBehaviour//������ ��ܺο� ǥ�õ� ���� ����� ���� ���� ǥ�� ui
{
    public TestMob thisMob;
    //�� 2+2+1���� 5ĭ�� ���� ����� ǥ�� ui.
    public GameObject[] BuffsUI;//���� ǥ�� UI (attack,defense)//2���ۿ� ���ٰ� �����ϰ� �ڵ� �ۼ�//������ ����.
    public GameObject[] DebuffsUI;//����� ǥ�� UI (defense,speed)
    public GameObject ElementNow;//���� �������� �Ӽ� ���� //���Ӽ� ���� ��ȭ��(�߰������� ��ȭ) �ٲ��� ȿ�� �� �Բ� �ٲ�.
    public GameObject[] ElementalFX;//���� ��ȭ�� ��������ġ�� ����� ����Ʈ.

    public Sprite[] BuffsSprites;//���� ��������Ʈ// 0:������ 1:����� 2:������� 3:�ӵ���� 4:ȭ������ 5:������ 6:������
    private SkillType currentSkillT;//���� �������� ��ų�� �Ӽ�.


    // Update is called once per frame
    async void Update()
    {
        if (thisMob.isAtkBuffed)
        {
            if(thisMob.isDefBuffed)//������ +�����
            {
                BuffsUI[0].SetActive(true);//��ĭ
                BuffsUI[1].SetActive(true);//��ĭ
                BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݹ���
                BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//������
            }
            else//��������
            {
                BuffsUI[0].SetActive(true);//��ĭ
                BuffsUI[1].SetActive(false);
                BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݹ���
            }
        }
        else if (thisMob.isDefBuffed)//�������
        {
            BuffsUI[0].SetActive(true);//��ĭ
            BuffsUI[1].SetActive(false);
            BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//������
        }//�������
        else
        {
            BuffsUI[0].SetActive(false);
            BuffsUI[1].SetActive(false);
        }//����X


        if (thisMob.isDefDebuffed)
        {
            if (thisMob.isSpeedDebuffed)//������ + �ӵ����
            {
                DebuffsUI[0].SetActive(true);//��ĭ
                DebuffsUI[1].SetActive(true);//��ĭ
                DebuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//������
                DebuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ����
            }
            else//��������
            {
                DebuffsUI[0].SetActive(true);//��ĭ
                DebuffsUI[1].SetActive(false);
                DebuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//������
            }
        }
        else if (thisMob.isSpeedDebuffed)//�ӵ������
        {
            DebuffsUI[0].SetActive(true);//��ĭ
            DebuffsUI[1].SetActive(false);
            DebuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ����
        }//�ӵ������
        else
        {
            DebuffsUI[0].SetActive(false);
            DebuffsUI[1].SetActive(false);
        }//�����X


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
        }//���ð��� ������Ʈ (������ + ������fx)
    }



    private async UniTask TurnElementFire()
    {
        //�Ҳ� ������ ȿ��
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[4];
    }
    private async UniTask TurnElementWater()
    {
        //�� ������ ȿ��
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[5];
    }
    private async UniTask TurnElementWood()
    {
        //�� ������ ȿ��
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[6];
    }

}
