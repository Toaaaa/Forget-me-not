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
    List<GameObject> itemInInven;//���� display�ǰ��ִ� �����۵��� ������ �迭.
    int invenNumber; // itemInInven�� [i] ��°�� �����ϴ� ����.
    int invenTotal; // itemInInven�� �� ������ �����ϴ� ����.

    private void Start()
    {
        CreateDisplay(inventype);

    }
    private void Update()
    {

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
                    itemInInven.Add(obj); //���� display�ǰ��ִ� �����۵��� ����.
                }

            }
            else
            {
                Debug.Log(inventory.Container[i]._itemType);
            }

            if (inventory.Container[i].amount == 0) //�������� 0���� �Ǹ� �κ����� ����.
            {
                Destroy(itemDisplayed[inventory.Container[i]]); //destroy�� �ʿ��Ѱ�? //�ʿ��ѵ�.. ���� ������Ʈ ��ü�� ������� �ϴϱ�. dictionary �� list�� ����Ȱ� �����ϻ� ������Ʈ ��ü�� �ƴϴϱ�.
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
                itemInInven.Add(obj); //���� display�ǰ��ִ� �����۵��� ����.
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
