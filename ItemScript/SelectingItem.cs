using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingItem : MonoBehaviour //displayInventory�� ���� ������ ����â�� �ּ� �ش� dictionary�� ������ �޾ƿͼ� ����Ұ�. >>>�׳� displayinventory�� �����Ұ�.
{
    [SerializeField]
    GameObject[] itemInInven;
    DisplayInventory displayInventory;
    Inventory inventory;
    int inventype;

    private void Start()
    {
        if(displayInventory.itemDisplayed == null)
        {
            Debug.Log("displayInventory.itemDisplayed is null");
        }
        inventype = displayInventory.inventype;
    }

    void Update()
    {

        if (displayInventory.itemDisplayed == null)
        {
            Debug.Log("displayInventory.itemDisplayed is null");
            return;
        }

        inventype = displayInventory.inventype;



        for (int i=0; i< displayInventory.itemDisplayed.Count; i++)
        {
            //itemInInven[i] = displayInventory.itemDisplayed[inventory.Container[i]].item.prefab; //�κ��丮�� �ִ� �������� 
        }
    }
}
