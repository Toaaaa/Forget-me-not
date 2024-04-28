using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
    public Inventory inventory;
    public CombatManager combatManager;
    public CombatDisplay combatDisplay;

    public int Y_Start;
    public int Y_SpaceBetween;
    int invenNumber; //itemInInven의 [i[]번째를 저장하는 변수
    int invenTotal; //itemInInven의 총 개수를 저장하는 변수 (1부터 시작)
    int invenPage; //현재 페이지를 저장하는 변수.
    public int itemPerPage = 6; //한 페이지에 표시할 아이템의 개수.
    public Dictionary<InvenSlot, GameObject> itemDisplayed = new Dictionary<InvenSlot, GameObject>();
    public List<GameObject> itemInInven = new List<GameObject>();



    private void Start()
    {
        CreateDisplay();
    }
    private void OnEnable()
    {
        invenNumber = 0;
        invenPage = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !combatDisplay.itemSelected)
        {
            Debug.Log("esc");
            combatDisplay.noCharObj = true;
            combatDisplay.courountineGo();
            gameObject.SetActive(false);
        }//인벤토리화면에서 esc누르면 firstselection으로 돌아가기.
        ItemReplace();
        UpdateDisplay();
        invenTotal = itemInInven.Count;
        invenPage = invenNumber / itemPerPage;
        SelectingItem();
        if(Input.GetKeyDown(KeyCode.Space) && !combatDisplay.itemSelected)//인벤토리에서 아이템을 골라서 스페이스바를 누르면.
        {
            combatDisplay.selectingItem = inventory.Container[itemInInven[invenNumber].GetComponent<IsGone>().itemID].item;
            combatDisplay.tempIndex = combatDisplay.selectedSlotIndex;
            combatDisplay.selectedSlotIndex = 0;
            combatDisplay.slotList[combatDisplay.selectedSlotIndex].combatSelection.charSelection.SetActive(true);
            ConsumeItem consumeItem = (ConsumeItem)combatDisplay.selectingItem;
            if(consumeItem.consumeType == ConsumeItem.ConsumeType.Buff)
            {
                combatDisplay.BuffItemSelected =true;
            }
            else
            {
                combatDisplay.itemSelected = true;
            }
            gameObject.SetActive(false);
        }

    }
    private void SelectingItem()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(invenNumber > 0)
            {
                invenNumber--;
            }
            else
            {
                invenNumber = invenTotal - 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(invenNumber < invenTotal - 1)
            {
                invenNumber++;
            }
            else
            {
                invenNumber = 0;
            }
        }
        for(int i = 0; i < invenTotal; i++)
        {
            if(i == invenNumber)
            {
                itemInInven[i].GetComponent<Image>().color = new Color32(0, 66, 0, 255);
                combatDisplay.selectingItem = inventory.Container[itemInInven[i].GetComponent<IsGone>().itemID].item;
            }
            else
            {
                itemInInven[i].GetComponent<Image>().color = new Color32(0, 66, 255, 255);
            }
        }
        if(invenTotal == 0)
        {
            combatDisplay.selectingItem = null;
        }
    }

    private void UpdateDisplay()
    {
        for(int i=0; i< inventory.Container.Count; i++)
        {
            if (inventory.Container[i].amount !=0 &&inventory.Container[i]._itemType == 1)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i]))
                {
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[0].text = inventory.Container[i].item.name;
                    ConsumeItem consumeItem = (ConsumeItem)inventory.Container[i].item;
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[1].text = consumeItem.consumeType.ToString();
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[2].text = inventory.Container[i].amount.ToString("n0");
                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    obj.GetComponent<IsGone>().itemID = inventory.Container[i].ID;
                    itemDisplayed.Add(inventory.Container[i], obj);
                    obj.SetActive(false);
                    itemInInven.Add(obj);
                }
            }
        }
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i]._itemType == 1)
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                obj.GetComponent<IsGone>().itemID = inventory.Container[i].ID;
                itemDisplayed.Add(inventory.Container[i], obj);
                obj.SetActive(false); //처음에는 모든 아이템을 비활성화 시키고, itemReplace에서 활성화 시킬것.
                itemInInven.Add(obj); //현재 display되고있는 아이템들을 저장.
            }
        }
    }
    void ItemReplace()
    {
        for (int i = itemInInven.Count - 1; i >= 0; i--) //뒤에서부터 진행하여 정방향으로 진행할때 발생할 수 있는 오류 방지.
        {
            if (itemInInven[i] != null && itemInInven[i].GetComponent<IsGone>().isGone)
            {
                itemDisplayed.Remove(inventory.Container[itemInInven[i].GetComponent<IsGone>().itemID]);
                Destroy(itemInInven[i].gameObject);
                itemInInven.RemoveAt(i);
                invenTotal = itemInInven.Count;
                invenNumber = 0;
            }
        }
        //이전에 같은 위치에 getposition 해준 아이템이 있을경우 겹쳐서 표시되는 문제가 발생하지 않도록 아래의 코드를 추가.
        for (int i = 0; i < invenTotal; i++)
        {

            if (i >= itemPerPage * invenPage && i < itemPerPage * (invenPage + 1))
            {
                itemInInven[i].gameObject.SetActive(true);
            }
            else
            {
                itemInInven[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < itemPerPage; i++)//itemininven의 리스트 순서대로 아이템의 위치를 세팅 해주는 함수
        {
            if (i + (itemPerPage) * invenPage <= invenTotal - 1) //itemInInven의 범위를 넘어가지 않도록 설정.
            {
                itemInInven[i + (itemPerPage) * invenPage].gameObject.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
            else
            {
                break;//반복문 종료
                // return;의 경우 함수를 종료 시키기에 break를 사용.
            }

        }
    }
    public Vector3 GetPosition(int i)
    {
        return new Vector3(0, Y_Start + (Y_SpaceBetween * i), 0);
    }
}
