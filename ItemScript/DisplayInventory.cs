using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public Inventory inventory;
    public int inventype; // 0:weapon+acc, 1:consumable, 2:other

    public int Y_Start;
    public int Y_SpaceBetweenItems;
    public Dictionary<InvenSlot, GameObject> itemDisplayed = new Dictionary<InvenSlot, GameObject>();
    [SerializeField]
    List<GameObject> itemInInven;//���� display�ǰ��ִ� �����۵��� ������ �迭.
    int invenNumber; // itemInInven�� [i] ��°�� �����ϴ� ����.
    int invenTotal; // itemInInven�� �� ������ �����ϴ� ����.
    int invenPage; // ���� �κ��� �������� �����ϴ� ����, �ѹ��� â�� ǥ�õǴ� �������� ������ ������ ����.

    private void Start()
    {
        CreateDisplay(inventype);

    }
    private void Update()
    {
        itemReplace();
        UpdateDisplay(inventype);
        SelectingItem();//���⿡ �κ��丮 ������ ���� Ű���� �Է��� �޾Ƽ� ������ �����ϴ� �Լ��� �ֱ�.

    }
    /*
    public void UpdateDisplay(int inventype)
    {
        
        for (int i =0; i< inventory.Container.Count; i++)
        {
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) ���� ������ ������ ������ ������ �κ� ����.
            if (inventory.Container[i]._itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //�̹� ���ִ� ���.
                {
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                }
                else //inventory�� ���� �������� �߰����� ���.
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<�� ��� ��ü��item ������ ����Ǿ�����.
                    //�����ִ� prefab���� �� ��ĸ���, item ������ sprite + text ������ �����ͼ� �����ϴ� ������� �ٲܰ�.
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    itemDisplayed.Add(inventory.Container[i], obj);
                }
                
            }
            else
            {
                Debug.Log(inventory.Container[i]._itemType);               
            }

            if (inventory.Container[i].amount == 0) //�������� 0���� �Ǹ� �κ����� ����.
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
            // if(inventory.Container[].itemtype ==1) , if(inventory.Container[].itemtype ==1), if(inventory.Container[].itemtype ==2) ���� ������ ������ ������ ������ �κ� ����.
            if (inventory.Container[i].amount !=0 &&inventory.Container[i]._itemType == inventype)
            {
                if (itemDisplayed.ContainsKey(inventory.Container[i])) //�̹� ���ִ� ���.
                {
                    itemDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0"); //inventoy.container Ű�� gameobject�� ��������.
                }
                else //inventory�� ���� �������� �߰����� ���.
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform); //inventory.Container[i].item.<<�� ��� ��ü��item ������ ����Ǿ�����.
                    //�����ִ� prefab���� �� ��ĸ���, item ������ sprite + text ������ �����ͼ� �����ϴ� ������� �ٲܰ�.
                    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
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

        //0���� �Ǹ� remove��destroy ,����Ʈ ���� ������ ����ְ�, ������ ���� �����ִ� �Լ� + itemdisplayed������ �������ִ� �Լ�.
        /*for(int i =0; i< itemInInven.Count; i++)
        {
            if (itemInInven[i] != null && itemInInven[i].GetComponent<IsGone>().isGone)
            {
                itemDisplayed.Remove(inventory.Container[itemInInven[i].GetComponent<IsGone>().itemID]);
                itemInInven.Remove(itemInInven[i]);
                Destroy(itemInInven[i]);
                Debug.Log("destroy");
            }

            if (itemInInven[i] == null && itemInInven.Count != i+1)
            {
                itemInInven[i] = itemInInven[i + 1];
            }
            else if (itemInInven[i] == null && itemInInven.Count == i+1)
            {
                itemInInven[i] = itemInInven[i + 1];
                itemInInven.RemoveAt(i + 1);
                Debug.Log("removeAT");
            }
        }*/
        
        for (int i = 0; i < itemInInven.Count; i++)
        {
            if (itemInInven[i] != null && itemInInven[i].GetComponent<IsGone>().isGone)
            {
                itemDisplayed.Remove(inventory.Container[itemInInven[i].GetComponent<IsGone>().itemID]);
                Destroy(itemInInven[i].gameObject);
                //itemInInven.Remove(itemInInven[i]);
                itemInInven.RemoveAt(i);
                Debug.Log("destroy");//
            }
        }

        //itemininven�� ����Ʈ ������� �������� ��ġ�� ���� ���ִ� �Լ�
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
                obj.GetComponent<IsGone>().itemID = inventory.Container[i].ID;
                itemDisplayed.Add(inventory.Container[i], obj);
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
