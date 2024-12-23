using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamagePrintManager : MonoBehaviour
{
    public GameObject damagePrintPrefab;
    public GameObject[] damagePrint;

    private void Awake()
    {
        damagePrint = new GameObject[12];
        Generate();
    }
    private void Generate()
    {
        for(int i =0; i < damagePrint.Length; i++)
        {
            damagePrint[i] = Instantiate(damagePrintPrefab,CombatManager.Instance.damagePrintManager.gameObject.transform);
            damagePrint[i].SetActive(false);
            //데미지 출력은, 데미지 계산 할때, 해당 게임 오브젝트를 setactive해주며, 위치를 설정해주고, 텍스트를 변경해주면 됨.
        }
    }

    public void PrintDamage(GameObject MobPos, float damage,bool iscrit,bool isheal)
    {
        for(int i = 0; i < damagePrint.Length; i++)
        {
            if (!damagePrint[i].activeInHierarchy)
            {
                damagePrint[i].gameObject.GetComponent<RectTransform>().anchoredPosition = MobPos.GetComponent<RectTransform>().anchoredPosition + new Vector2(0,40f);
                if (iscrit)
                {
                    damagePrint[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = 60;
                }//폰트 사이즈
                else
                {
                    damagePrint[i].GetComponentInChildren<TextMeshProUGUI>().fontSize = 40;
                }
                if(isheal)
                {
                    damagePrint[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.green;
                }
                else
                {
                    damagePrint[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                }
                damagePrint[i].GetComponentInChildren<TextMeshProUGUI>().text =Math.Round(damage,MidpointRounding.AwayFromZero).ToString();
                damagePrint[i].SetActive(true);
                break;
            }
        }
    }
}
