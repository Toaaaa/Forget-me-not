using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsGone : MonoBehaviour //�������� ������ 0���϶��� Ȯ���ϴ� ������ ��� �Լ�.
{
    [SerializeField]
    Inventory inventory;
    public int itemID;
    public bool isGone;

    private void Update()
    {
        if (inventory.Container[itemID].amount ==0)
        {
            isGone = true;
        }
        else
        {
            isGone = false;
        }
    }
}
