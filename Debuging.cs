using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuging : MonoBehaviour
{
    public Inventory inventory;


    // Update is called once per frame
    void Update()
    {
        Debug.Log(inventory.Container[0]._itemType + "," + inventory.Container[1]._itemType + "," + inventory.Container[2]._itemType);
        
    }
}
