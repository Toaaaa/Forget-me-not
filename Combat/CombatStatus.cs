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
        if(player == null) //�÷��̾ ������� slot�� ��Ȱ��ȭ.
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
                player.atk = Mathf.Round(player.originalAtk / 2); //�Ƿε��� �ִ�ġ�϶� ���ݷ��� 1/2�� ����.
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
            //ü�¹� ����ȿ��.
            if(playerHpBar.fillAmount > player.hp / player.maxHp)//ü���� ������ ���.
            {
                StartCoroutine(hpreduce());
            }
            else if(playerHpBar.fillAmount < player.hp / player.maxHp)//ü���� ������ ���.
            {
                StartCoroutine(hpIncrease());
            }
            //�ڷ�ƾ���� ü�¹� ���̱�.
    }
    private void PlayerftUpdate()
    {
        if(playerFatique.fillAmount > player.fatigue / player.maxFatigue)//�Ƿε��� ������ ���.
        {
            StartCoroutine(ftreduce());
        }
        else if(playerFatique.fillAmount < player.fatigue / player.maxFatigue)//�Ƿε��� ������ ���.
        {
            StartCoroutine(ftIncrease());
        }

    }

    public void OnPlayerHpSet()
    {
        //ó�� ������ ���۵ǰ� �÷��̾ ����� �Ǹ� �÷��̾��� ü�¹ٸ� ��������.
        playerHpBar.fillAmount = player.hp / player.maxHp; //>> ����/���� �� �ϸ� 0�Ǵ� 1�� ��µǴ� ������ �־� float�� ����ȯ ����.
    }
    public void OnPlayerFtSet()
    {
        playerFatique.fillAmount = player.fatigue / player.maxFatigue;
    }
    IEnumerator hpreduce() //ü�� ���ҽ�
    {
        //ü�¹� ���̱�.
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
    IEnumerator hpIncrease()//ü�� ������
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
    IEnumerator ftreduce() //�Ƿε� ���ҽ�
    {
        //�Ƿε� �� ���̱�.
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
    IEnumerator ftIncrease()//�Ƿε� ������
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
