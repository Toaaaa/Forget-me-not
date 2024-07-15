using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnTimeUsedShow : MonoBehaviour
{
    public GameObject TurntimeUsed;
    public GameObject[] AllUsedTurnTimeUI;//����� �Ͻð��� �����ִ� UI
    public GameObject turnTime;//��Ÿ�� ���� ��ü
    public GameObject playerTurntime;//(��ü) ��Ÿ�� ǥ�����ִ� ������

    private void Awake()
    {
        AllUsedTurnTimeUI = new GameObject[6];
        Generate();
    }
    private void Generate()
    {
        for (int i = 0; i < AllUsedTurnTimeUI.Length; i++)
        {
            AllUsedTurnTimeUI[i] = Instantiate(TurntimeUsed, turnTime.gameObject.transform);
            AllUsedTurnTimeUI[i].SetActive(false);
        }
    }

    public void PrintUsedTime(float usedTime)
    {
        for (int i = 0; i < AllUsedTurnTimeUI.Length; i++)
        {
            if (!AllUsedTurnTimeUI[i].activeInHierarchy)
            {
                AllUsedTurnTimeUI[i].transform.position = playerTurntime.transform.position + new Vector3(0, 0.2f, 0);
                AllUsedTurnTimeUI[i].GetComponentInChildren<TextMeshProUGUI>().text = usedTime.ToString();
                AllUsedTurnTimeUI[i].SetActive(true);
                break;
            }
        }
    }
}
