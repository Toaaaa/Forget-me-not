using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatStatus : MonoBehaviour
{
    public PlayableC player;
    public Image playerImage;
    public Image playerHpBar;
    public TextMeshProUGUI playerHpText;
    public Image playerMpBar;
    public TextMeshProUGUI playerMpText;
    public List<CombatBuffs> combatBuffs;

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
            for(int i = 0; i < combatBuffs.Count; i++)
            {
                combatBuffs[i].player = player;
            }
            buffcheck();
        }

    }

    private void buffcheck()
    {
        if(player.isBuffed == true)
        {
            //버프 이미지 변경.
            //버프 이미지를 보여주는 패널을 활성화.
            combatBuffs[0].BuffCheck(1);
        }
        if(player.isStunned == true)
        {
            combatBuffs[1].BuffCheck(2);
        }
        if(player.isPoisoned == true) //중독은 3번째 칸
        {
            combatBuffs[2].BuffCheck(3);
        }
        if(player.isSkillSealed == true) //스킬잠금도 3번째 칸 ??스킬잠금과 중독을 둘다 쓰는 적은 없도록 할것.
        {
            combatBuffs[2].BuffCheck(4);
        }
        if(player.isBuffed == false && player.isStunned == false && player.isPoisoned == false && player.isSkillSealed ==false)
        {
            combatBuffs[0].BuffCheck(0);
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
