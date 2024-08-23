using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatus : MonoBehaviour
{
    public PlayableC player;
    public Image playerSprite;
    public Image playerImage;
    public Image playerHpBar;
    public TextMeshProUGUI playerHpText;
    public Image playerFatique;
    public CombatBuffs combatbuff;
    private void Update()
    {
        if(player == null) //플레이어가 없을경우 slot의 비활성화.
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            if(combatbuff.player == null)
            {
                combatbuff.player = player;
            }
            this.gameObject.SetActive(true);
            playerImage.sprite = player.characterImage;

            if (playerHpBar.fillAmount != player.hp / player.maxHp)
            {
                PlayerhpUpdate();
            }
            if(playerFatique.fillAmount != player.fatigue / player.maxFatigue)
            {
                PlayerftUpdate();
            }
            buffcheck();
            if (player.fatigue == player.maxFatigue)
            {
                player.isTired = true;
                player.atk = Mathf.Round(player.originalAtk / 2); //피로도가 최대치일때 공격력이 1/2로 감소.
            }
            else
            {
                player.isTired = false;
            }
        }
        if(player != null)
        {
            playerSprite.sprite = player.CharIcon;
        }
        else
        {
            playerSprite.sprite = null;
        }
        playerHpText.text = (int)player.hp + "/" + (int)player.maxHp;
    }

    private void buffcheck()
    {
        if(player.isBuffed == true)
        {
            combatbuff.BuffCheck(1);
        }
        else
        {
            combatbuff.BuffCheck(0);
        }

        if(player.isStunned == true)
        {
            combatbuff.BuffCheck(2);
        }
        else
        {
            combatbuff.BuffCheck(5);
        }

        if(player.isPoisoned == true)
        {
            combatbuff.BuffCheck(3);
        }
        else
        {
            combatbuff.BuffCheck(6);
        }


    }

    private void PlayerhpUpdate()
    {
            //체력바 진동효과.
            if(playerHpBar.fillAmount > player.hp / player.maxHp)//체력이 감소한 경우.
            {
                StartCoroutine(hpreduce());
            }
            else if(playerHpBar.fillAmount < player.hp / player.maxHp)//체력이 증가한 경우.
            {
                StartCoroutine(hpIncrease());
            }
            //코루틴으로 체력바 줄이기.
    }
    private void PlayerftUpdate()
    {
        if(playerFatique.fillAmount > player.fatigue / player.maxFatigue)//피로도가 감소한 경우.
        {
            StartCoroutine(ftreduce());
        }
        else if(playerFatique.fillAmount < player.fatigue / player.maxFatigue)//피로도가 증가한 경우.
        {
            StartCoroutine(ftIncrease());
        }

    }

    public void OnPlayerHpSet()
    {
        //처음 전투가 시작되고 플레이어가 등록이 되면 플레이어의 체력바를 세팅해줌.
        playerHpBar.fillAmount = player.hp / player.maxHp; //>> 정수/정수 를 하면 0또는 1만 출력되는 현상이 있어 float로 형변환 해줌.
    }
    public void OnPlayerFtSet()
    {
        playerFatique.fillAmount = player.fatigue / player.maxFatigue;
    }
    IEnumerator hpreduce() //체력 감소시
    {
        //체력바 줄이기.
        while(playerHpBar.fillAmount > player.hp / player.maxHp)
        {
            playerHpBar.fillAmount -= 0.001f;
            if(playerHpBar.fillAmount < player.hp / player.maxHp)
            {
                playerHpBar.fillAmount = player.hp / player.maxHp;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator hpIncrease()//체력 증가시
    {
        while(playerHpBar.fillAmount < player.hp / player.maxHp)
        {
            playerHpBar.fillAmount += 0.001f;
            if(playerHpBar.fillAmount > player.hp / player.maxHp)
            {
                playerHpBar.fillAmount = player.hp / player.maxHp;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator ftreduce() //피로도 감소시
    {
        //피로도 바 줄이기.
        while(playerFatique.fillAmount > player.fatigue / player.maxFatigue)
        {
            playerFatique.fillAmount -= 0.001f;
            if(playerFatique.fillAmount < player.fatigue / player.maxFatigue)
            {
                playerFatique.fillAmount = player.fatigue / player.maxFatigue;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
        
    }
    IEnumerator ftIncrease()//피로도 증가시
    {
        while(playerFatique.fillAmount < player.fatigue / player.maxFatigue)
        {
            playerFatique.fillAmount += 0.001f;
            if(playerFatique.fillAmount > player.fatigue / player.maxFatigue)
            {
                playerFatique.fillAmount = player.fatigue / player.maxFatigue;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
