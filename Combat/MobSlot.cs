using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlot : MonoBehaviour
{
    public GameObject selectingArrow; //표시해주는 화살표.

    // Update is called once per frame
    void Update()
    {
        if(CombatManager.Instance.monsterSelected != null)
        {
            if (CombatManager.Instance.monsterSelected == this.gameObject)
            {
                selectingArrow.SetActive(true);
            }
            else
            {
                selectingArrow.SetActive(false);
            }
        }
        else
        {
            selectingArrow.SetActive(false);
        }
    }
}
