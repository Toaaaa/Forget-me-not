using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlot : MonoBehaviour
{
    public GameObject selectingArrow; //표시해주는 화살표.
    public GameObject monster;
    // Update is called once per frame
    void Update()
    {
        if(CombatManager.Instance.monsterSelected != null)
        {
            if (CombatManager.Instance.monsterSelected == monster)
            {
                selectingArrow.SetActive(true);
            }
            else if(CombatManager.Instance.combatDisplay.skillForAllMob)
            {
                if(monster !=null &&!monster.GetComponent<TestMob>().isDead)
                    selectingArrow.SetActive(true);
            }
            else
            {
                selectingArrow.SetActive(false);
            }
        }
        else if(CombatManager.Instance.combatDisplay.skillForAllMob)
        {
            selectingArrow.SetActive(true);
        }
        else
        {
            selectingArrow.SetActive(false);
        }
    }
}
