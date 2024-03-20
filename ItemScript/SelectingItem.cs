using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingItem : MonoBehaviour //displayInventory와 같은 아이템 선택창에 둬서 해당 dictionary의 정보를 받아와서 사용할것. >>>그냥 displayinventory에 구현할것.
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
            //itemInInven[i] = displayInventory.itemDisplayed[inventory.Container[i]].item.prefab; //인벤토리에 있는 아이템을 
        }
    }
}
