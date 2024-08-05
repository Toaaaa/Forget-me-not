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


    public GameObject FrozenTimer;//�������� 2���� ���� ����Ʈ �̿Ϸ�� speed�� �پ��� ȿ���� ���� ���� ǥ���ϴ� Ÿ�̸�
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

        if (story.isOnStage2 && !story.Stage2Extra3)//���پ�����
        {
            FrozenTimer.SetActive(true);
            this.gameObject.GetComponent<Image>().color = new Color(47, 239, 255, 255);//�Ķ� �׵θ�.
            timerBar.GetComponent<Image>().color = new Color(47, 239, 255, 255);//�Ķ� ������.
            timerText.color = new Color(0, 255, 178, 255);//�ణ �Ķ� �ؽ�Ʈ.
        }
        else
        {
            FrozenTimer.SetActive(false);
            this.gameObject.GetComponent<Image>().color = new Color(242, 255, 47, 255);//����� ��� �׵θ�.
            timerBar.GetComponent<Image>().color = new Color(242, 255, 47, 255);//����� ��� ������.
            timerText.color = new Color(255, 239, 0, 255);//����� ��� �ؽ�Ʈ.
        }
    }
    private void TimerUpdate()
    {
        timerBar.fillAmount = currentTime / maxTime;
    }
}
