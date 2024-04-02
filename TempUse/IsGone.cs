using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsGone : MonoBehaviour //�������� ������ 0���϶��� Ȯ���ϴ� ������ ��� �Լ�.
{
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    ShopDisplay shopDisplay;
    public int itemID;
    public bool isGone;
    public bool isGoneinShop;

    private void OnEnable()
    {
        if(GameManager.Instance.shopUI.activeSelf == true)
        {
            shopDisplay = GameManager.Instance.shopUI.GetComponent<ShopDisplay>();
        }
    }

    private void Update()
    {
        if (inventory.Container[itemID].amount ==0)
        {
            isGone = true;
        }
        else
        {
            isGone = false;
        }

        if(shopDisplay != null)
        {
            if (shopDisplay.container[itemID].item != null && shopDisplay.container[itemID].amount == 0)
            {
                isGoneinShop = true;
            }
            else
            {
                isGoneinShop = false;
            }
        }
        
    }
}
