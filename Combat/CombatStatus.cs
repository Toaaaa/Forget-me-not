using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatus : MonoBehaviour
{
    public PlayableC player;
    public Image playerImage;
    public Image playerHpBar;
    public Image playerMpBar;

    private void Update()
    {
        if(player == null) //플레이어가 없을경우 slot의 비활성화.
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
            playerImage.sprite = player.characterImage;

            if (playerHpBar.fillAmount != player.hp / player.maxHp)
            {
                PlayerhpUpdate();
            }
            if (playerMpBar.fillAmount != player.mp / player.maxMp)
            {
                PlayermpUpdate();
            }
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
    private void PlayermpUpdate()
    {
        if(playerMpBar.fillAmount > player.mp / player.maxMp)
        {
            StartCoroutine(mpreduce());
        }
        else if(playerMpBar.fillAmount < player.mp / player.maxMp)
        {
            StartCoroutine(mpIncrease());
        }

    }

    public void OnPlayerHpSet()
    {
        //처음 전투가 시작되고 플레이어가 등록이 되면 플레이어의 체력바를 세팅해줌.
        playerHpBar.fillAmount = player.hp / player.maxHp; //>> 정수/정수 를 하면 0또는 1만 출력되는 현상이 있어 float로 형변환 해줌.
    }
    public void OnPlayerMpSet()
    {
        playerMpBar.fillAmount = player.mp / player.maxMp;
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

    IEnumerator mpreduce()
    {
        while(playerMpBar.fillAmount > player.mp / player.maxMp)
        {
            playerMpBar.fillAmount -= 0.001f;
            if(playerMpBar.fillAmount < player.mp / player.maxMp)
            {
                playerMpBar.fillAmount = player.mp / player.maxMp;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator mpIncrease()
    {
        while(playerMpBar.fillAmount < player.mp / player.maxMp)
        {
            playerMpBar.fillAmount += 0.001f;
            if(playerMpBar.fillAmount > player.mp / player.maxMp)
            {
                playerMpBar.fillAmount = player.mp / player.maxMp;
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
