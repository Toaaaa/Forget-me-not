using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopDisplay : MonoBehaviour
{
    
    public Inventory inventory;
    public List<shopSlot> container;//shop의 각종아이템이 저장된 리스트, 처음 npc와 대화시 , shopdate의 값을 받아 초기화됨
    public ShopInfoText infoText; //아이템의 정보를 출력하는 ui
    public Item selectedItem;//인벤에서부터 엔터키를 통해 선택이된 아이템.

    public GameObject itemBuyUI; //아이템 사용시 나타나는 ui
    public GameObject itemInfoUI; //가격 등 아이템 정보의 ui
    public UISelectingSystem uISelectingSystem;

    public int Y_Start;
    public int Y_SpaceBetweenItems;
    public Dictionary<shopSlot, GameObject> itemDisplayed = new Dictionary<shopSlot, GameObject>();
    public List<GameObject> itemInShop;//현재 shop에 있는 (같은 inventype의)모든 아이템   
    public int shopNumber; // itemInInven의 [i] 번째를 저장하는 변수. //현재 선택된 아이템의 번호. >>0부터 시작
    int shopTotal; // itemInInven의 총 개수를 저장하는 변수. //1부터 시작 주의

    int itemPerPage = 9; //한페이지에 표시되는 아이템의 갯수를 저장하는 변수. 
    int invenPage; // 현재 인벤의 페이지를 저장하는 변수, 한번의 창에 표시되는 아이템의 갯수에 제한이 있음. >> 일단은 한페이지에 9개의 아이템으로.

    private void OnDisable()
    {
        itemInfoUI.SetActive(false);
    }

    private void Start()
    {
        CreateDisplay();
    }

    private void Update()
    {
        infoText.gameObject.SetActive(true);
        itemReplace();
        UpdateDisplay();
        shopTotal = itemInShop.Count;
        invenPage = shopNumber / (itemPerPage);
        if (container[itemInShop[shopNumber].GetComponent<IsGone>().itemID].item != null)
        {
            selectedItem = container[itemInShop[shopNumber].GetComponent<IsGone>().itemID].item;
            infoText.itemPrice = container[itemInShop[shopNumber].GetComponent<IsGone>().itemID].price;
        }
        useSelectedItem();
        SelectingItem();//여기에 인벤토리 선택을 위해 키보드 입력을 받아서 아이템 선택하는 함수도 넣기.



    }
    private void OnEnable()
    {
        shopNumber = 0;
        invenPage = 0;
        //itemBuyUI 의 displayinventory에 this를 넣어줌.
        itemBuyUI.GetComponent<itemBuyUI>().displayShop = this;
    }


    public void SelectingItem()
    {
        if(itemBuyUI.activeSelf == false)//아이템 구매창 >> 갯수 선택시 에는 아이템 변경이 안되게 고정.
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) //위 방향키를 누르면
            {
                if (shopNumber > 0)
                {
                    shopNumber--;
                }
                else
                {
                    shopNumber = shopTotal - 1; //inventotal은 1부터 시작이므로 -1을 해줌.
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow)) //아래 방향키를 누르면
            {
                if (shopNumber < shopTotal - 1)
                {
                    shopNumber++;
                }
                else
                {
                    shopNumber = 0;
                }
            }
        }
        for (int i = 0; i < shopTotal; i++)
        {
            if (i == shopNumber) //선택된 아이템의 정보 출력
            {
                itemInShop[i].GetComponent<Image>().color = new Color(0f, 66f, 0f);
                infoText.selectedItem = selectedItem; //현재 선택된 아이템의 정보를 infoText에 전달.
                infoText.itemPrice = container[itemInShop[shopNumber].GetComponent<IsGone>().itemID].price;
            }
            else//선택되지 않은 아이템들
            {
                itemInShop[i].GetComponent<Image>().color = new Color(0f, 66f, 255f);
            }
        }
        if (shopTotal == 0) //인벤토리에 아이템이 없을경우
        {
            infoText.selectedItem = null;
            infoText.itemPrice = 0;
        }

        //선택한 아이템을 사용하는 함수 추가. useSelectedItem();        
    }

    public void useSelectedItem() //해당 아이템을 사용하는 함수를, 관리에 용이하도록,  추후 장비/소비 에 따라 각각의 개별 메서드를 이용해 사용할것.
    {
        if (Input.GetKeyDown(KeyCode.Space) && selectedItem != null)
        {
            itemBuyUI.SetActive(true);

        }
    }

    public void buyItem(Item selected,int amount) //itemuseUI에서 사용버튼을 누를경우 실행되는 함수.
    {
        int price = container[itemInShop[shopNumber].GetComponent<IsGone>().itemID].price;
        int itemtype = container[itemInShop[shopNumber].GetComponent<IsGone>().itemID]._itemType;
        if (inventory.goldHave >= price * amount) //돈이 충분할경우
        {
            inventory.goldHave -= price * amount; //돈을 차감.
            inventory.AddItem(selected, amount, itemtype); //해당 아이템을 인벤에 추가.
            container[shopNumber].amount -= amount; //상점의 아이템 갯수를 차감.
            if (container[shopNumber].amount == 0) //상점의 아이템 갯수가 0개가 되면
            {
                itemInShop[shopNumber].GetComponent<IsGone>().isGoneinShop = true; //해당 아이템을 display에서 삭제.
            }
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void UpdateDisplay()
    {

        for (int i = 0; i < container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) 으로 나눠서 각각의 종류의 아이템 인벤 구분.
            if (container[i].amount != 0)
            {
                if (itemDisplayed.ContainsKey(container[i])) //이미 들어가있는 경우.
                {
                    itemDisplayed[container[i]].GetComponent<ItemBoxDisplay>().itemboxText[0].text = container[i].item.name;
                    switch (container[i]._itemType) // 해당 아이템의 타입의 텍스트를 출력해줌<<<<<<<<<<<<<<<<<<<<<<<<< 추후 조금더 세련되게 수정할것.
                    {
                        case 0: //장비 아이템의 경우
                            EquipItem equipItem = (EquipItem)container[i].item;
                            itemDisplayed[container[i]].GetComponent<ItemBoxDisplay>().itemboxText[1].text = equipItem.equipType.ToString();
                            break;
                        case 1:
                            ConsumeItem consumeItem = (ConsumeItem)container[i].item;
                            itemDisplayed[container[i]].GetComponent<ItemBoxDisplay>().itemboxText[1].text = consumeItem.consumeType.ToString();
                            break;
                        case 2:
                            itemDisplayed[container[i]].GetComponent<ItemBoxDisplay>().itemboxText[1].text = "";
                            break;
                    }
                    itemDisplayed[container[i]].GetComponent<ItemBoxDisplay>().itemboxText[2].text = container[i].amount.ToString("n0"); //inventoy.container 키를 gameobject를 가져왓음.
                    //이미 들어가있는 경우 해당아이템의 인벤토리 정보만 업데이트 해주는 작업.
                }
                else //inventory에 새로 아이템이 추가됬을 경우.
                {
                    var obj = Instantiate(container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<의 경우 본체의item 정보가 저장되어있음.
                    //여기있는 prefab으로 된 방식말고, item 정보의 sprite + text 정보를 가져와서 생성하는 방식으로 바꿀것.
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = container[i].amount.ToString("n0");
                    obj.GetComponent<IsGone>().itemID = i; //처음 만들때 itemID를 저장. 해서 자체적으로 아이템이 0개가 되면 삭제되게 유도.
                    itemDisplayed.Add(container[i], obj);
                    itemInShop.Add(obj); //현재 display되고있는 아이템들을 저장.                   
                }
            }
        }
    }
    void itemReplace() //어떤 아이템이0개가 되면(상점에서 다 팔리면) 상점 리스트에서 삭제된것을 display에 업데이트 해주는 함수.
    {
        for (int i = itemInShop.Count - 1; i >= 0; i--) //뒤에서부터 진행하여 정방향으로 진행할때 발생할 수 있는 오류 방지.
        {
            if (itemInShop[i] != null && itemInShop[i].GetComponent<IsGone>().isGoneinShop)
            {
                itemDisplayed.Remove(container[itemInShop[i].GetComponent<IsGone>().itemID]);
                Destroy(itemInShop[i].gameObject);
                itemInShop.RemoveAt(i);
                shopTotal = itemInShop.Count;
                shopNumber = 0;
            }
        }
        //이전에 같은 위치에 getposition 해준 아이템이 있을경우 겹쳐서 표시되는 문제가 발생하지 않도록 아래의 코드를 추가.
        for (int i = 0; i < shopTotal; i++)
        {

            if (i >= itemPerPage * invenPage && i < itemPerPage * (invenPage + 1))
            {
                itemInShop[i].gameObject.SetActive(true);
            }
            else
            {
                itemInShop[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < itemPerPage; i++)//itemininven의 리스트 순서대로 아이템의 위치를 세팅 해주는 함수
        {
            if (i + (itemPerPage) * invenPage <= shopTotal - 1) //itemInInven의 범위를 넘어가지 않도록 설정.
            {
                itemInShop[i + (itemPerPage) * invenPage].gameObject.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
            else
            {
                break;//반복문 종료
                // return;의 경우 함수를 종료 시키기에 break를 사용.
            }

        }

    }

    public void CreateDisplay()
    {
        for (int i = 0; i < container.Count; i++)
        {
                var obj = Instantiate(container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = container[i].amount.ToString("n0");
                obj.GetComponent<IsGone>().itemID = i;
                itemDisplayed.Add(container[i], obj);
                obj.SetActive(false); //처음에는 모든 아이템을 비활성화 시키고, itemReplace에서 활성화 시킬것.
                itemInShop.Add(obj); //현재 display되고있는 아이템들을 저장.
                //itemReplace();
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(0, Y_Start + (-Y_SpaceBetweenItems * i), 0f);
        //위의 방식이 아닌 새로운 방식으로. 세로 방향으로만 나열되는 방식으로 바꿀것.
    }

}
