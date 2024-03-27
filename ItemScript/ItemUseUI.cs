using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseUI : MonoBehaviour
{
    public DisplayInventory displayInventory;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            displayInventory.useItem();
            gameObject.SetActive(false);
        }
    }
}
