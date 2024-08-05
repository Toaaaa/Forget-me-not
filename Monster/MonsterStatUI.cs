using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MonsterStatUI : MonoBehaviour//������ ��ܺο� ǥ�õ� ���� ����� ���� ���� ǥ�� ui
{
    public TestMob thisMob;
    //�� 3+1���� 4ĭ�� ���� ����� ǥ�� ui.
    public GameObject[] BuffsUI;//���� ǥ�� UI (attack,defense,speed)//3���ۿ� ���ٰ� �����ϰ� �ڵ� �ۼ�//������ ����.
    public GameObject ElementNow;//���� �������� �Ӽ� ���� //���Ӽ� ���� ��ȭ��(�߰������� ��ȭ) �ٲ��� ȿ�� �� �Բ� �ٲ�.
    public GameObject[] ElementalFX;//���� ��ȭ�� ��������ġ�� ����� ����Ʈ. 0:ȭ��, 1:��, 2:��

    public Sprite[] BuffsSprites;//���� ��������Ʈ// 0:������ 1:����� 2:������� 3:�ӵ���� 4:ȭ������ 5:������ 6:������
    private SkillType currentSkillT;//���� �������� ��ų�� �Ӽ�.


    // Update is called once per frame
    async void Update()
    {
        //////���� ���۾� ��� ��,��,�ӵ�,�Ӽ� 4�������¿� ���ؼ��� ǥ���ϴ� ����� ui
        if (thisMob.isAtkBuffed)//���ݷ� ������ ����
        {
            if (thisMob.isDefBuffed || thisMob.isDefDebuffed)//���� ���� ������ ����
            {
                if (thisMob.isDefBuffed)//���� ���� ����
                {
                    if (thisMob.isSpeedDebuffed)//�ӵ� ����� ����
                    {
                        //������ + ���� ���� + �ӵ� �����
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(true);//��ĭ
                        BuffsUI[2].SetActive(true);//��ĭ
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݷ¹���
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//���¹���
                        BuffsUI[2].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ������
                    }
                    else//�ӵ� ����� ��� ���Ҷ�
                    {
                        //������ + ���¹���
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(true);//��ĭ
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݷ¹���
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//���¹���
                    }
                }
                else//���� ����� ����
                {
                    if (thisMob.isSpeedDebuffed)//�ӵ� ����� ����
                    {
                        //������ + ���� ����� + �ӵ� �����
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(true);//��ĭ
                        BuffsUI[2].SetActive(true);//��ĭ
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݷ¹���
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//���µ����
                        BuffsUI[2].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ������
                    }
                    else//�ӵ� ����� ��� ���Ҷ�
                    {
                        //������+ ���� �����
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(true);//��ĭ
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݷ¹���
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//���µ����
                    }
                }
            }
            else//���� ���� ������ ��� ���Ҷ�
            {
                if (thisMob.isSpeedDebuffed)//�ӵ� ���� ������ ����
                {
                    //������ + �ӵ� �����
                    BuffsUI[0].SetActive(true);//��ĭ
                    BuffsUI[1].SetActive(true);//��ĭ
                    BuffsUI[2].SetActive(false);
                    BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݷ¹���
                    BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ������
                }
                else
                {
                    //�������� ������
                    BuffsUI[0].SetActive(true);//��ĭ
                    BuffsUI[1].SetActive(false);
                    BuffsUI[2].SetActive(false);
                    BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[0];//���ݷ¹���
                }
            }
        }
        else//������ ������
        {
            if (thisMob.isDefBuffed || thisMob.isDefDebuffed)//���� ���� ������ ����
            {
                if(thisMob.isDefBuffed)//���� ���� ����
                {
                    if(thisMob.isSpeedDebuffed)//�ӵ� ����� ����
                    {
                        //���� ���� + �ӵ� �����
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(true);//��ĭ
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//���¹���
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ������
                    }
                    else//�ӵ� ����� ��� ���Ҷ�
                    {
                        //���� ������
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(false);
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[1];//���¹���
                    }
                }
                else//���� ����� ����
                {
                    if(thisMob.isSpeedDebuffed)//�ӵ� ����� ����
                    {
                        //���� ����� + �ӵ� �����
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(true);//��ĭ
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//���µ����
                        BuffsUI[1].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ������
                    }
                    else//�ӵ� ����� ��� ���Ҷ�
                    {
                        //���� �������
                        BuffsUI[0].SetActive(true);//��ĭ
                        BuffsUI[1].SetActive(false);
                        BuffsUI[2].SetActive(false);
                        BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[2];//���µ����
                    }
                }
            }
            else//���� ���� �����ܵ� ��� ���Ҷ�
            {
                if(thisMob.isSpeedDebuffed)//�ӵ� ���� ������ ����
                {
                    //�ӵ� �������
                    BuffsUI[0].SetActive(true);//��ĭ
                    BuffsUI[1].SetActive(false);
                    BuffsUI[2].SetActive(false);
                    BuffsUI[0].GetComponent<SpriteRenderer>().sprite = BuffsSprites[3];//�ӵ������
                }
                else//�ƹ��� ���� ������� ������
                {
                    //�ƹ��� ������ ����
                    BuffsUI[0].SetActive(false);
                    BuffsUI[1].SetActive(false);
                    BuffsUI[2].SetActive(false);
                }
            }
        }//������ �������� ���

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
        ElementalFX[0].SetActive(true);
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[4];
        await UniTask.Delay(300);
        ElementalFX[0].SetActive(false);
    }
    private async UniTask TurnElementWater()
    {
        //�� ������ ȿ��
        ElementalFX[1].SetActive(true);
        await UniTask.Delay(800);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[5];
        await UniTask.Delay(300);
        ElementalFX[1].SetActive(false);
    }
    private async UniTask TurnElementWood()
    {
        //�� ������ ȿ��
        await UniTask.Delay(800);
        ElementalFX[2].SetActive(true);
        ElementNow.GetComponent<SpriteRenderer>().sprite = BuffsSprites[6];
        await UniTask.Delay(300);
        ElementalFX[2].SetActive(false);
    }

}
