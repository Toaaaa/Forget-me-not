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
            if (inventory.Container[i].itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i]))
                {
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<의 경우 본체의item 정보가 저장되어있음.
                    //여기있는 prefab으로 된 방식말고, item 정보의 sprite + text 정보를 가져와서 생성하는 방식으로 바꿀것.
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemDisplayed.Add(inventory.Container[i], obj);
                }
            }
        }
    }
    public void invenDisplay()
    {

    }

    public void CreateDisplay(int inventype)
    {
        for(int i =0; i< inventory.Container.Count; i++)
        {
            if (inventory.Container[i].itemType == inventype)
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            }
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_SpaceBetweenItems*(i%NumberOfColumns)), Y_Start+(-Y_SpaceBetweenItems*(i/NumberOfColumns)), 0f);
    }

   
}
