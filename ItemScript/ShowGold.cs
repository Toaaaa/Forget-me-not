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
        goldText.text = inventory.goldHave.ToString();
    }
    private void Update()
    {
        goldText.text = inventory.goldHave.ToString();
    }
}
