using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatItemInfo : MonoBehaviour
{
    public Item currentItem;
    public TextMeshProUGUI itemDisc;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if(currentItem != null)
        {
            itemDisc.text = currentItem.itemDescription;
        }
    }
}
