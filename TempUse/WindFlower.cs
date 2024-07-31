using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindFlower : MonoBehaviour //�Ǽ��� wflower�� �̰Ŷ� 2���� ����� ������..
{
    public bool isThisCliff; //������ �ִ� ������(true), �ٶ������� �ִ� ������(false)
    public StoryScriptable story;

    private void Update()
    {
        if (isThisCliff)//������ ���� ���
        {
            if (story.Stage2Extra2)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
        else//�ٶ������� ���� ���
        {
            if (story.Stage2Extra3)
            {
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

}
