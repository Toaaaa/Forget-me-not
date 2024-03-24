using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShowGold : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;
    TextMeshProUGUI goldText;

    private void Start()
    {
        goldText = GetComponent<TextMeshProUGUI>();
        goldText.text = inventory.goldHave.ToString(); //추후 골드쪽도 아이콘 추가.
    }
    private void Update()
    {
        goldText.text = inventory.goldHave.ToString();
    }
}
