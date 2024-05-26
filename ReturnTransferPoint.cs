using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnTransferPoint : MonoBehaviour //���� ������.
{
    public string linkedMapName; //����� �� �̸�


    private void Start()
    {
        if (linkedMapName == Player.Instance.currentMapName) //�̵��� ���� �� �̸��� transferMapName�� ���ٸ�
        {
            if(GameManager.Instance.storyScriptable.isStage2Completed && this.name == "Return")
                Player.Instance.transform.position = this.transform.position; //�ش� ��Ż�� ��ġ�� �÷��̾� �̵������ֱ�

        }
    }
}
