using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatTimer : MonoBehaviour
{
    public StoryScriptable story;
    public CombatManager combatManager;
    public Image timerBar;
    public TextMeshProUGUI timerText;


    public GameObject FrozenTimer;//스테이지 2에서 서브 퀘스트 미완료시 speed가 줄어드는 효과가 적용 중을 표시하는 타이머
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

        if (story.isOnStage2 && !story.Stage2Extra3)//얼어붙었을때
        {
            FrozenTimer.SetActive(true);
            this.gameObject.GetComponent<Image>().color = new Color(47, 239, 255, 255);//파란 테두리.
            timerBar.GetComponent<Image>().color = new Color(47, 239, 255, 255);//파란 게이지.
            timerText.color = new Color(0, 255, 178, 255);//약간 파란 텍스트.
        }
        else
        {
            FrozenTimer.SetActive(false);
            this.gameObject.GetComponent<Image>().color = new Color(242, 255, 47, 255);//평소의 노란 테두리.
            timerBar.GetComponent<Image>().color = new Color(242, 255, 47, 255);//평소의 노란 게이지.
            timerText.color = new Color(255, 239, 0, 255);//평소의 노란 텍스트.
        }
    }
    private void TimerUpdate()
    {
        timerBar.fillAmount = currentTime / maxTime;
    }
}
