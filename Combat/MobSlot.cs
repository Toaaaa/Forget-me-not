using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSlot : MonoBehaviour
{
    public GameObject selectingArrow; //ǥ�����ִ� ȭ��ǥ.
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
