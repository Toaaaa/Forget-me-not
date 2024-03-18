using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory/Items/Default")]
public class DefaultItem : Item //aka "Other"
{
    public bool isQuestItem; //����Ʈ�������� ��� ������ �ȼ� ���� �� ����.
    [SerializeField]
    private int price; //��������
    [SerializeField]
    private int sellPrice; //�ǸŰ���

    private void Awake()
    {
        itemType = ItemType.Default;
    }

}
