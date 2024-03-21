using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsGone : MonoBehaviour //아이템의 갯수가 0개일때를 확인하는 변수를 담는 함수.
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
