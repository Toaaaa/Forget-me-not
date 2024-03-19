using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemZeroDest : MonoBehaviour
{
    Inventory inventory;
    public int thistype;// intantiate 할때 아이템 타입을 저장해줌.

    private void Update()
    {
        /*if (this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text == "0") //만약 아이템의 갯수가 0이 되면.
        {
            Destroy(this.gameObject); //아이템을 삭제.
        }*/

        /*for(int i = 0; i < inventory.Container.Count; i++)
        {
            if (this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text != inventory.Container[i]._itemType.ToString())
            {

            }
        }*/
        
    }
}
