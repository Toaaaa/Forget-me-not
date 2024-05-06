using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePrintManager : MonoBehaviour
{
    public GameObject damagePrintPrefab;
    public GameObject[] damagePrint;

    private void Awake()
    {
        damagePrint = new GameObject[5];
        Generate();
    }
    private void Generate()
    {
        for(int i =0; i < damagePrint.Length; i++)
        {
            damagePrint[i] = Instantiate(damagePrintPrefab,CombatManager.Instance.damagePrintManager.gameObject.transform);
            damagePrint[i].SetActive(false);
            //������ �����, ������ ��� �Ҷ�, �ش� ���� ������Ʈ�� setactive���ָ�, ��ġ�� �������ְ�, �ؽ�Ʈ�� �������ָ� ��.
        }
    }

    public void PrintDamage(Vector3 position, float damage)
    {
        for(int i = 0; i < damagePrint.Length; i++)
        {
            if (!damagePrint[i].activeInHierarchy)
            {
                damagePrint[i].transform.position = position + new Vector3(0,30,0);
                damagePrint[i].GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                damagePrint[i].SetActive(true);
                break;
            }
        }
    }
}
