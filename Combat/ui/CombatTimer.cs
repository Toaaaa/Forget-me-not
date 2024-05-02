using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatTimer : MonoBehaviour
{
    public CombatManager combatManager;
    public Image timerBar;
    public TextMeshProUGUI timerText;

    public float maxTime;
    public float currentTime;
    public int currentTimeInt;

    private void Update()
    {
        currentTime = combatManager.playerTurnTime;
        currentTimeInt = (int)combatManager.playerTurnTime;
        timerText.text = currentTimeInt.ToString();
        if(timerBar.fillAmount != currentTime / maxTime)
        {
            TimerUpdate();
        }
    }
    private void TimerUpdate()
    {
        timerBar.fillAmount = currentTime / maxTime;
    }
}
