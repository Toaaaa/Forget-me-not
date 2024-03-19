using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public Inventory inventory;
    public int inventype; // 0:weapon+acc, 1:consumable, 2:other

    public int X_Start;
    public int Y_Start;
    public int X_SpaceBetweenItems;
    public int Y_SpaceBetweenItems;
    public int NumberOfColumns;
    Dictionary<InvenSlot, GameObject> itemDisplayed = new Dictionary<InvenSlot, GameObject>();

    private void Start()
    {
        CreateDisplay(inventype);

    }
    private void Update()
    {

        UpdateDisplay(inventype);
    }

    public void UpdateDisplay(int inventype)
    {
        
        for (int i =0; i< inventory.Container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) 으로 나눠서 각각의 종류의 아이템 인벤 구분.
            if (inventory.Container[i]._itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //이미 들어가있는 경우.
                {
                    Debug.Log("아이템 갯수 업데이트");
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                }
                else //inventory에 새로 아이템이 추가됬을 경우.
                {
                    Debug.Log("test");
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<의 경우 본체의item 정보가 저장되어있음.
                    //여기있는 prefab으로 된 방식말고, item 정보의 sprite + text 정보를 가져와서 생성하는 방식으로 바꿀것.
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemDisplayed.Add(inventory.Container[i], obj);
                }
                
            }
            else
            {
                Debug.Log(inventory.Container[i]._itemType); //얘가 계속 출력 + 1의 값만 나옴. >> 처음 start() 에서는 0의 값을 가졌는데 update() 가 되면서 계속 1의 값만 나온다.               
            }

            if (inventory.Container[i].amount == 0) //아이템이 0개가 되면 인벤에서 삭제.
            {
                Destroy(itemDisplayed[inventory.Container[i]]);
                Debug.Log("아이템 삭제");
                itemDisplayed.Remove(inventory.Container[i]);
            }
        }
    }
    public void invenDisplay()
    {

    }
    //지금 문제가 inventory.Container[i].itemType의 값이 제대로 적용되지 않고 1로만 적용되고 있다.
    public void CreateDisplay(int inventype)
    {
        for(int i =0; i< inventory.Container.Count; i++)
        {
            if (inventory.Container[i]._itemType == inventype)
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemDisplayed.Add(inventory.Container[i], obj);
                Debug.Log("아이템 생성" + inventory.Container[i]._itemType);
            }
            else
            {
                Debug.Log(inventory.Container[i]._itemType);
            }

            if (inventory.Container[i]._itemType == 0) { Debug.Log("123123"); }
            
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_SpaceBetweenItems*(i%NumberOfColumns)), Y_Start+(-Y_SpaceBetweenItems*(i/NumberOfColumns)), 0f);
    }

   
}
