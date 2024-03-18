using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Item", menuName = "Inventory/Items/Default")]
public class DefaultItem : Item //aka "Other"
{
    public bool isQuestItem; //퀘스트아이템의 경우 상점에 팔수 없게 할 변수.
    [SerializeField]
    private int price; //상점가격
    [SerializeField]
    private int sellPrice; //판매가격

    private void Awake()
    {
        itemType = ItemType.Default;
    }

}
