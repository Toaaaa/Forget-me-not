using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Inventory inventory;

    private void Update() //새로운 아이템을 게임내에 추가 + 테스팅 단계에서는 스크립트를 꺼주고 더이상 새로운 아이템이 추가될 일 없을때 스크립트를 켜주도록 할것.
    {
        for(int i = 0; i < inventory.Container.Count-1; i++)
        {
            if (inventory.Container[i].item == inventory.Container[inventory.Container.Count - 1].item) //방금 새로 들어온 아이템이 기존에 존재할 경우.
            {
                inventory.Container[i].AddAmount(inventory.Container[inventory.Container.Count - 1].amount); //기존 아이템의 갯수를 증가시킴.
                inventory.Container.RemoveAt(inventory.Container.Count - 1); //새로 들어온 아이템 삭제.
                break;
            }
        }
    }
}
