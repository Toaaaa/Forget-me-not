using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour 
{
    public MenuManager menuManager;
    public PlayableManager playableManager; //inventype이 0일때만 접근하여 사용하도록 할것.
    public List<SlotManager> playerslot;
    public Inventory inventory;
    public int inventype; // 0:weapon+acc, 1:consumable, 2:other
    public InfoText infoText;
    public Item selectedItem;//인벤에서부터 엔터키를 통해 선택이된 아이템.

    public int Y_Start;
    public int Y_SpaceBetweenItems;
    public Dictionary<InvenSlot, GameObject> itemDisplayed = new Dictionary<InvenSlot, GameObject>();
    public List<GameObject> itemInInven;//현재 인벤토리에 있는 (같은 inventype의)모든 아이템   
    int invenNumber; // itemInInven의 [i] 번째를 저장하는 변수. //현재 선택된 아이템의 번호. >>0부터 시작
    int invenTotal; // itemInInven의 총 개수를 저장하는 변수. //1부터 시작 주의

    int itemPerPage = 9; //한페이지에 표시되는 아이템의 갯수를 저장하는 변수. 
    int invenPage; // 현재 인벤의 페이지를 저장하는 변수, 한번의 창에 표시되는 아이템의 갯수에 제한이 있음. >> 일단은 한페이지에 9개의 아이템으로.
    int p_slotNumber; //player슬롯
    int p_slotTotal; 

    bool isp_SlotOn;


    private void Start()
    {
        CreateDisplay(inventype);

    }

    private void Update()
    {

        p_slotTotal = playableManager.joinedPlayer.Count;
        itemReplace();
        UpdateDisplay(inventype);
        invenTotal = itemInInven.Count;
        invenPage = invenNumber/(itemPerPage);
        useSelectedItem(invenNumber);

        if (!isp_SlotOn)
            SelectingItem();//여기에 인벤토리 선택을 위해 키보드 입력을 받아서 아이템 선택하는 함수도 넣기.
        else
        {
            slotSelection();
            showSelectedSlot();
        }
        

    }
    private void OnEnable()
    {
        invenNumber = 0; 
        invenPage = 0;
    }


    public void SelectingItem()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) //위 방향키를 누르면
        {
            if(invenNumber > 0)
            {
                invenNumber--;
            }
            else
            {
                invenNumber = invenTotal-1; //inventotal은 1부터 시작이므로 -1을 해줌.
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) //아래 방향키를 누르면
        {
            if (invenNumber < invenTotal-1)
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
            if (i == invenNumber) //선택된 아이템의 정보 출력
            {
                itemInInven[i].GetComponent<Image>().color = new Color(0f, 66f, 0f);
                infoText.selectedItem = inventory.Container[itemInInven[i].GetComponent<IsGone>().itemID].item; //현재 선택된 아이템의 정보를 infoText에 전달.
            }
            else//선택되지 않은 아이템들
            {
                itemInInven[i].GetComponent<Image>().color = new Color(0f, 66f, 255f);
            }
        }
        if(invenTotal == 0) //인벤토리에 아이템이 없을경우
        {
            infoText.selectedItem = null;
        }

        //선택한 아이템을 사용하는 함수 추가. useSelectedItem();        
    }
    void slotSelection()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(p_slotNumber > 0)
            {
                p_slotNumber--;
            }
            else
            {
                p_slotNumber = p_slotTotal - 1;
            }
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(p_slotNumber < p_slotTotal - 1)
            {
                p_slotNumber++;
            }
            else
            {
                p_slotNumber = 0;
            }
        }
    }
    void showSelectedSlot()
    {
        for(int i = 0; i < p_slotTotal; i++)
        {
            if(i != p_slotNumber)
            {
                playerslot[i].isSelected = false;
            }
            else
            {
                playerslot[i].isSelected = true;
            }
        }
    }
    public void useSelectedItem(int invennum)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            selectedItem = inventory.Container[itemInInven[invennum].GetComponent<IsGone>().itemID].item;
            switch (inventype)
            {
                case 0:
                    isp_SlotOn = true;//여기서 코루틴을..쓰나./.??
                    menuManager.isItemUsing = true;
                    Debug.Log("장비 아이템 사용");
                    break;
                case 1:
                    isp_SlotOn = true;//여기서 코루틴을..쓰나./.??
                    menuManager.isItemUsing = true;
                    Debug.Log("소비 아이템 사용");
                    break;
                case 2:
                    Debug.Log("기타 아이템은 사용을 하지 못해요");
                    break;
            }
        }
    }


    public void UpdateDisplay(int inventype)
    {

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) 으로 나눠서 각각의 종류의 아이템 인벤 구분.
            if (inventory.Container[i].amount !=0 &&inventory.Container[i]._itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //이미 들어가있는 경우.
                {
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[0].text = inventory.Container[i].item.name;
                    switch(inventory.Container[i]._itemType) // 해당 아이템의 타입의 텍스트를 출력해줌<<<<<<<<<<<<<<<<<<<<<<<<< 추후 조금더 세련되게 수정할것.
                    {
                        case 0: //장비 아이템의 경우
                            EquipItem equipItem = (EquipItem)inventory.Container[i].item;
                            itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[1].text = equipItem.equipType.ToString();
                            break;
                        case 1:
                            ConsumeItem consumeItem = (ConsumeItem)inventory.Container[i].item;
                            itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[1].text = consumeItem.consumeType.ToString();
                            break;
                        case 2:
                            itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[1].text = "";
                            break;
                    }
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[2].text = inventory.Container[i].amount.ToString("n0"); //inventoy.container 키를 gameobject를 가져왓음.
                    //이미 들어가있는 경우 해당아이템의 인벤토리 정보만 업데이트 해주는 작업.
                }
                else //inventory에 새로 아이템이 추가됬을 경우.
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<의 경우 본체의item 정보가 저장되어있음.
                    //여기있는 prefab으로 된 방식말고, item 정보의 sprite + text 정보를 가져와서 생성하는 방식으로 바꿀것.
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    obj.GetComponent<IsGone>().itemID = inventory.Container[i].ID; //처음 만들때 itemID를 저장. 해서 자체적으로 아이템이 0개가 되면 삭제되게 유도.
                    itemDisplayed.Add(inventory.Container[i], obj);
                    itemInInven.Add(obj); //현재 display되고있는 아이템들을 저장.                   
                }
            }
        }
    }
    void itemReplace() //어떤 아이템이0개가 되면 가방에서 삭제된것을 display에 업데이트 해주는 함수.
    {

        
        for (int i = itemInInven.Count -1; i>=0; i--) //뒤에서부터 진행하여 정방향으로 진행할때 발생할 수 있는 오류 방지.
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

            if(i>= itemPerPage*invenPage && i < itemPerPage*(invenPage+1))
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
            if(i + (itemPerPage) * invenPage <= invenTotal -1) //itemInInven의 범위를 넘어가지 않도록 설정.
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

    public void CreateDisplay(int inventype)
    {
        for(int i =0; i< inventory.Container.Count; i++)
        {
            if (inventory.Container[i]._itemType == inventype)
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

    public Vector3 GetPosition(int i)
    {
        return new Vector3(0, Y_Start+(-Y_SpaceBetweenItems*i), 0f);
        //위의 방식이 아닌 새로운 방식으로. 세로 방향으로만 나열되는 방식으로 바꿀것.
    }

  
}
