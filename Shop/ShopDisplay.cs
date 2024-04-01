using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopDisplay : MonoBehaviour
{
    
    public Inventory inventory;
    public List<shopSlot> container;//shop�� ������������ ����� ����Ʈ, ó�� npc�� ��ȭ�� , shopdate�� ���� �޾� �ʱ�ȭ��
    public InfoText infoText;
    public Item selectedItem;//�κ��������� ����Ű�� ���� �����̵� ������.

    public GameObject itemBuyUI; //������ ���� ��Ÿ���� ui
    public UISelectingSystem uISelectingSystem;

    public int Y_Start;
    public int Y_SpaceBetweenItems;
    public Dictionary<InvenSlot, GameObject> itemDisplayed = new Dictionary<InvenSlot, GameObject>();
    public List<GameObject> itemInShop;//���� shop�� �ִ� (���� inventype��)��� ������   
    int shopNumber; // itemInInven�� [i] ��°�� �����ϴ� ����. //���� ���õ� �������� ��ȣ. >>0���� ����
    int shopTotal; // itemInInven�� �� ������ �����ϴ� ����. //1���� ���� ����

    int itemPerPage = 9; //���������� ǥ�õǴ� �������� ������ �����ϴ� ����. 
    int invenPage; // ���� �κ��� �������� �����ϴ� ����, �ѹ��� â�� ǥ�õǴ� �������� ������ ������ ����. >> �ϴ��� ���������� 9���� ����������.



    private void Start()
    {
        CreateDisplay();

    }

    private void Update()
    {
        infoText.gameObject.SetActive(true);
        selectedItem = inventory.Container[itemInShop[shopNumber].GetComponent<IsGone>().itemID].item;
        itemReplace();
        UpdateDisplay();
        shopTotal = itemInShop.Count;
        invenPage = shopNumber / (itemPerPage);
        useSelectedItem();
        SelectingItem();//���⿡ �κ��丮 ������ ���� Ű���� �Է��� �޾Ƽ� ������ �����ϴ� �Լ��� �ֱ�.



    }
    private void OnEnable()
    {
        shopNumber = 0;
        invenPage = 0;
        //itemBuyUI �� displayinventory�� this�� �־���.
        itemBuyUI.GetComponent<itemBuyUI>().displayShop = this;
    }


    public void SelectingItem()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) //�� ����Ű�� ������
        {
            if (shopNumber > 0)
            {
                shopNumber--;
            }
            else
            {
                shopNumber = shopTotal - 1; //inventotal�� 1���� �����̹Ƿ� -1�� ����.
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) //�Ʒ� ����Ű�� ������
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
        for (int i = 0; i < shopTotal; i++)
        {
            if (i == shopNumber) //���õ� �������� ���� ���
            {
                itemInShop[i].GetComponent<Image>().color = new Color(0f, 66f, 0f);
                infoText.selectedItem = inventory.Container[itemInShop[i].GetComponent<IsGone>().itemID].item; //���� ���õ� �������� ������ infoText�� ����.
            }
            else//���õ��� ���� �����۵�
            {
                itemInShop[i].GetComponent<Image>().color = new Color(0f, 66f, 255f);
            }
        }
        if (shopTotal == 0) //�κ��丮�� �������� �������
        {
            infoText.selectedItem = null;
        }

        //������ �������� ����ϴ� �Լ� �߰�. useSelectedItem();        
    }

    public void useSelectedItem() //�ش� �������� ����ϴ� �Լ���, ������ �����ϵ���,  ���� ���/�Һ� �� ���� ������ ���� �޼��带 �̿��� ����Ұ�.
    {
        if (Input.GetKeyDown(KeyCode.Return) && selectedItem != null)
        {
            itemBuyUI.SetActive(true);

        }
    }

    public void buyItem() //itemuseUI���� ����ư�� ������� ����Ǵ� �Լ�.
    {
        
    }

    public void UpdateDisplay()
    {

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) ���� ������ ������ ������ ������ �κ� ����.
            if (inventory.Container[i].amount != 0)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //�̹� ���ִ� ���.
                {
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[0].text = inventory.Container[i].item.name;
                    switch (inventory.Container[i]._itemType) // �ش� �������� Ÿ���� �ؽ�Ʈ�� �������<<<<<<<<<<<<<<<<<<<<<<<<< ���� ���ݴ� ���õǰ� �����Ұ�.
                    {
                        case 0: //��� �������� ���
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
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[2].text = inventory.Container[i].amount.ToString("n0"); //inventoy.container Ű�� gameobject�� ��������.
                    //�̹� ���ִ� ��� �ش�������� �κ��丮 ������ ������Ʈ ���ִ� �۾�.
                }
                else //inventory�� ���� �������� �߰����� ���.
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<�� ��� ��ü��item ������ ����Ǿ�����.
                    //�����ִ� prefab���� �� ��ĸ���, item ������ sprite + text ������ �����ͼ� �����ϴ� ������� �ٲܰ�.
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    obj.GetComponent<IsGone>().itemID = inventory.Container[i].ID; //ó�� ���鶧 itemID�� ����. �ؼ� ��ü������ �������� 0���� �Ǹ� �����ǰ� ����.
                    itemDisplayed.Add(inventory.Container[i], obj);
                    itemInShop.Add(obj); //���� display�ǰ��ִ� �����۵��� ����.                   
                }
            }
        }
    }
    void itemReplace() //� ��������0���� �Ǹ� ���濡�� �����Ȱ��� display�� ������Ʈ ���ִ� �Լ�.
    {


        for (int i = itemInShop.Count - 1; i >= 0; i--) //�ڿ������� �����Ͽ� ���������� �����Ҷ� �߻��� �� �ִ� ���� ����.
        {
            if (itemInShop[i] != null && itemInShop[i].GetComponent<IsGone>().isGone)
            {
                itemDisplayed.Remove(inventory.Container[itemInShop[i].GetComponent<IsGone>().itemID]);
                Destroy(itemInShop[i].gameObject);
                itemInShop.RemoveAt(i);
                shopTotal = itemInShop.Count;
                shopNumber = 0;
            }
        }
        //������ ���� ��ġ�� getposition ���� �������� ������� ���ļ� ǥ�õǴ� ������ �߻����� �ʵ��� �Ʒ��� �ڵ带 �߰�.
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

        for (int i = 0; i < itemPerPage; i++)//itemininven�� ����Ʈ ������� �������� ��ġ�� ���� ���ִ� �Լ�
        {
            if (i + (itemPerPage) * invenPage <= shopTotal - 1) //itemInInven�� ������ �Ѿ�� �ʵ��� ����.
            {
                itemInShop[i + (itemPerPage) * invenPage].gameObject.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
            else
            {
                break;//�ݺ��� ����
                // return;�� ��� �Լ��� ���� ��Ű�⿡ break�� ���.
            }

        }

    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                //obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                obj.GetComponent<IsGone>().itemID = inventory.Container[i].ID;
                itemDisplayed.Add(inventory.Container[i], obj);
                obj.SetActive(false); //ó������ ��� �������� ��Ȱ��ȭ ��Ű��, itemReplace���� Ȱ��ȭ ��ų��.
                itemInShop.Add(obj); //���� display�ǰ��ִ� �����۵��� ����.


        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(0, Y_Start + (-Y_SpaceBetweenItems * i), 0f);
        //���� ����� �ƴ� ���ο� �������. ���� �������θ� �����Ǵ� ������� �ٲܰ�.
    }

}
