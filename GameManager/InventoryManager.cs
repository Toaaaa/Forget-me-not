using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;

    private void Update() //���ο� �������� ���ӳ��� �߰� + �׽��� �ܰ迡���� ��ũ��Ʈ�� ���ְ� ���̻� ���ο� �������� �߰��� �� ������ ��ũ��Ʈ�� ���ֵ��� �Ұ�.
    {
        for(int i = 0; i < inventory.Container.Count-1; i++)
        {
            if (inventory.Container[i].item == inventory.Container[inventory.Container.Count - 1].item) //��� ���� ���� �������� ������ ������ ���.
            {
                inventory.Container[i].AddAmount(inventory.Container[inventory.Container.Count - 1].amount); //���� �������� ������ ������Ŵ.
                inventory.Container.RemoveAt(inventory.Container.Count - 1); //���� ���� ������ ����.
                break;
            }
        }
    }
}
