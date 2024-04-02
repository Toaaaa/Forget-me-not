using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGoneShop : MonoBehaviour
{
    [SerializeField]
    ShopDisplay shopDisplay;
    public int itemID;
    public bool isGone;


    private void Update()
    {
        if (shopDisplay.container[itemID].item != null &&shopDisplay.container[itemID].amount == 0)
        {
            isGone = true;
        }
        else
        {
            isGone = false;
        }
    }
}
