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
        
    }
    //여기서 엔터를 하면 선택중인 아이템을 itembuyui로 정보를 넘겨서 해당 ui가 열려있을때 enter를 통해 구매를 할수 있게.
    // Update is called once per frame
    void Update()
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
                //기타샵일때
                break;
            default:
                Debug.Log("shopName is not valid");
                shopDisplay.container = shopData.StartStage;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false); 

    }
}
