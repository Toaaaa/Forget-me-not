using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public Dictionary<InvenSlot, GameObject> itemDisplayed = new Dictionary<InvenSlot, GameObject>();
    [SerializeField]
    List<GameObject> itemInInven;//현재 display되고있는 아이템들을 저장할 배열.
    int invenNumber; // itemInInven의 [i] 번째를 저장하는 변수.
    int invenTotal; // itemInInven의 총 개수를 저장하는 변수.

    private void Start()
    {
        CreateDisplay(inventype);

    }
    private void Update()
    {

        UpdateDisplay(inventype);
        SelectingItem();//여기에 인벤토리 선택을 위해 키보드 입력을 받아서 아이템 선택하는 함수도 넣기.

    }
    /*
    public void UpdateDisplay(int inventype)
    {
        
        for (int i =0; i< inventory.Container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) 으로 나눠서 각각의 종류의 아이템 인벤 구분.
            if (inventory.Container[i]._itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //이미 들어가있는 경우.
                {
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                }
                else //inventory에 새로 아이템이 추가됬을 경우.
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<의 경우 본체의item 정보가 저장되어있음.
                    //여기있는 prefab으로 된 방식말고, item 정보의 sprite + text 정보를 가져와서 생성하는 방식으로 바꿀것.
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemDisplayed.Add(inventory.Container[i], obj);
                }
                
            }
            else
            {
                Debug.Log(inventory.Container[i]._itemType);               
            }

            if (inventory.Container[i].amount == 0) //아이템이 0개가 되면 인벤에서 삭제.
            {
                Destroy(itemDisplayed[inventory.Container[i]]);
                itemDisplayed.Remove(inventory.Container[i]);
            }
        }
    }*/
    public void SelectingItem()
    {

    }
    public void UpdateDisplay(int inventype)
    {

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) 으로 나눠서 각각의 종류의 아이템 인벤 구분.
            if (inventory.Container[i]._itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //이미 들어가있는 경우.
                {
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                }
                else //inventory에 새로 아이템이 추가됬을 경우.
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<의 경우 본체의item 정보가 저장되어있음.
                    //여기있는 prefab으로 된 방식말고, item 정보의 sprite + text 정보를 가져와서 생성하는 방식으로 바꿀것.
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemDisplayed.Add(inventory.Container[i], obj);
                    itemInInven.Add(obj); //현재 display되고있는 아이템들을 저장.
                }

            }
            else
            {
                Debug.Log(inventory.Container[i]._itemType);
            }

            if (inventory.Container[i].amount == 0) //아이템이 0개가 되면 인벤에서 삭제.
            {
                Destroy(itemDisplayed[inventory.Container[i]]); //destroy가 필요한가? //필요한듯.. 게임 오브젝트 자체를 없애줘야 하니깐. dictionary 와 list에 저장된건 정보일뿐 오브젝트 자체가 아니니깐.
                itemDisplayed.Remove(inventory.Container[i]);
                itemInInven.Remove(itemInInven[i]);
            }
        }
    }

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
                itemInInven.Add(obj); //현재 display되고있는 아이템들을 저장.
            }
            else
            {
                Debug.Log(inventory.Container[i]._itemType);
            }

            
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_Start + (X_SpaceBetweenItems*(i%NumberOfColumns)), Y_Start+(-Y_SpaceBetweenItems*(i/NumberOfColumns)), 0f);
    }

   
}
