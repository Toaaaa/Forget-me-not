using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public string shopName;
    public ShopDisplay shopDisplay;
    public ShopData shopData;

    private void OnEnable()
    {
        shopDisplay = GetComponent<ShopDisplay>();
        ShopDataUpdate();
    }
    private void Start()
    {
        ShopDataUpdate();
    }
    //���⼭ ���͸� �ϸ� �������� �������� itembuyui�� ������ �Ѱܼ� �ش� ui�� ���������� enter�� ���� ���Ÿ� �Ҽ� �ְ�.
    // Update is called once per frame
    void Update()
    {
        ShopDataUpdate();
        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false); 

    }

    void ShopDataUpdate()//���� ��ȣ�ۿ� ���� shop�� �����͸� �ҷ��� �������ִ� �Լ�.
    {
        switch (shopName)
        {
            case "0":
                shopDisplay.container = shopData.StartStage;
                break;
            case "1":
                shopDisplay.container = shopData.fisrtArea;
                break;
            case "2":
                shopDisplay.container = shopData.secondArea;
                break;
            case "3":
                //@@@@@@
                break;
            case "4":
                //��Ÿ���϶�
                break;
            default:
                Debug.Log("shopName is not valid");
                shopDisplay.container = shopData.StartStage;
                break;
        }
    }
}
