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
    int invenNumber; //itemInInven�� [i[]��°�� �����ϴ� ����
    int invenTotal; //itemInInven�� �� ������ �����ϴ� ���� (1���� ����)
    int invenPage; //���� �������� �����ϴ� ����.
    public int itemPerPage = 6; //�� �������� ǥ���� �������� ����.
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
        }//�κ��丮ȭ�鿡�� esc������ firstselection���� ���ư���.
        ItemReplace();
        UpdateDisplay();
        invenTotal = itemInInven.Count;
        invenPage = invenNumber / itemPerPage;
        SelectingItem();
        if(Input.GetKeyDown(KeyCode.Space) && !combatDisplay.itemSelected)//�κ��丮���� �������� ��� �����̽��ٸ� ������.
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
                obj.SetActive(false); //ó������ ��� �������� ��Ȱ��ȭ ��Ű��, itemReplace���� Ȱ��ȭ ��ų��.
                itemInInven.Add(obj); //���� display�ǰ��ִ� �����۵��� ����.
            }
        }
    }
    void ItemReplace()
    {
        for (int i = itemInInven.Count - 1; i >= 0; i--) //�ڿ������� �����Ͽ� ���������� �����Ҷ� �߻��� �� �ִ� ���� ����.
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

            if (i >= itemPerPage * invenPage && i < itemPerPage * (invenPage + 1))
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
            if (i + (itemPerPage) * invenPage <= invenTotal - 1) //itemInInven�� ������ �Ѿ�� �ʵ��� ����.
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
    public Vector3 GetPosition(int i)
    {
        return new Vector3(0, Y_Start + (Y_SpaceBetween * i), 0);
    }
}
