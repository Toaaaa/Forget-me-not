using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour 
{
    public MenuManager menuManager;
    public PlayableManager playableManager; //inventype�� 0�϶��� �����Ͽ� ����ϵ��� �Ұ�.
    public List<SlotManager> playerslot;
    public Inventory inventory;
    public int inventype; // 0:weapon+acc, 1:consumable, 2:other
    public InfoText infoText;
    public Item selectedItem;//�κ��������� ����Ű�� ���� �����̵� ������.

    public int Y_Start;
    public int Y_SpaceBetweenItems;
    public Dictionary<InvenSlot, GameObject> itemDisplayed = new Dictionary<InvenSlot, GameObject>();
    public List<GameObject> itemInInven;//���� �κ��丮�� �ִ� (���� inventype��)��� ������   
    int invenNumber; // itemInInven�� [i] ��°�� �����ϴ� ����. //���� ���õ� �������� ��ȣ. >>0���� ����
    int invenTotal; // itemInInven�� �� ������ �����ϴ� ����. //1���� ���� ����

    int itemPerPage = 9; //���������� ǥ�õǴ� �������� ������ �����ϴ� ����. 
    int invenPage; // ���� �κ��� �������� �����ϴ� ����, �ѹ��� â�� ǥ�õǴ� �������� ������ ������ ����. >> �ϴ��� ���������� 9���� ����������.
    int p_slotNumber; //player����
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
            SelectingItem();//���⿡ �κ��丮 ������ ���� Ű���� �Է��� �޾Ƽ� ������ �����ϴ� �Լ��� �ֱ�.
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
        if(Input.GetKeyDown(KeyCode.UpArrow)) //�� ����Ű�� ������
        {
            if(invenNumber > 0)
            {
                invenNumber--;
            }
            else
            {
                invenNumber = invenTotal-1; //inventotal�� 1���� �����̹Ƿ� -1�� ����.
            }
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) //�Ʒ� ����Ű�� ������
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
            if (i == invenNumber) //���õ� �������� ���� ���
            {
                itemInInven[i].GetComponent<Image>().color = new Color(0f, 66f, 0f);
                infoText.selectedItem = inventory.Container[itemInInven[i].GetComponent<IsGone>().itemID].item; //���� ���õ� �������� ������ infoText�� ����.
            }
            else//���õ��� ���� �����۵�
            {
                itemInInven[i].GetComponent<Image>().color = new Color(0f, 66f, 255f);
            }
        }
        if(invenTotal == 0) //�κ��丮�� �������� �������
        {
            infoText.selectedItem = null;
        }

        //������ �������� ����ϴ� �Լ� �߰�. useSelectedItem();        
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
                    isp_SlotOn = true;//���⼭ �ڷ�ƾ��..����./.??
                    menuManager.isItemUsing = true;
                    Debug.Log("��� ������ ���");
                    break;
                case 1:
                    isp_SlotOn = true;//���⼭ �ڷ�ƾ��..����./.??
                    menuManager.isItemUsing = true;
                    Debug.Log("�Һ� ������ ���");
                    break;
                case 2:
                    Debug.Log("��Ÿ �������� ����� ���� ���ؿ�");
                    break;
            }
        }
    }


    public void UpdateDisplay(int inventype)
    {

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) ���� ������ ������ ������ ������ �κ� ����.
            if (inventory.Container[i].amount !=0 &&inventory.Container[i]._itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //�̹� ���ִ� ���.
                {
                    itemDisplayed[inventory.Container[i]].GetComponent<ItemBoxDisplay>().itemboxText[0].text = inventory.Container[i].item.name;
                    switch(inventory.Container[i]._itemType) // �ش� �������� Ÿ���� �ؽ�Ʈ�� �������<<<<<<<<<<<<<<<<<<<<<<<<< ���� ���ݴ� ���õǰ� �����Ұ�.
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
                    itemInInven.Add(obj); //���� display�ǰ��ִ� �����۵��� ����.                   
                }
            }
        }
    }
    void itemReplace() //� ��������0���� �Ǹ� ���濡�� �����Ȱ��� display�� ������Ʈ ���ִ� �Լ�.
    {

        
        for (int i = itemInInven.Count -1; i>=0; i--) //�ڿ������� �����Ͽ� ���������� �����Ҷ� �߻��� �� �ִ� ���� ����.
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
        //������ ���� ��ġ�� getposition ���� �������� ������� ���ļ� ǥ�õǴ� ������ �߻����� �ʵ��� �Ʒ��� �ڵ带 �߰�.
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

        for (int i = 0; i < itemPerPage; i++)//itemininven�� ����Ʈ ������� �������� ��ġ�� ���� ���ִ� �Լ�
        {
            if(i + (itemPerPage) * invenPage <= invenTotal -1) //itemInInven�� ������ �Ѿ�� �ʵ��� ����.
            {
                itemInInven[i + (itemPerPage) * invenPage].gameObject.GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
            else
            {
                break;//�ݺ��� ����
                // return;�� ��� �Լ��� ���� ��Ű�⿡ break�� ���.
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
                obj.SetActive(false); //ó������ ��� �������� ��Ȱ��ȭ ��Ű��, itemReplace���� Ȱ��ȭ ��ų��.
                itemInInven.Add(obj); //���� display�ǰ��ִ� �����۵��� ����.
                

            }

            
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(0, Y_Start+(-Y_SpaceBetweenItems*i), 0f);
        //���� ����� �ƴ� ���ο� �������. ���� �������θ� �����Ǵ� ������� �ٲܰ�.
    }

  
}
