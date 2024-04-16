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
        if(player == null) //�÷��̾ ������� slot�� ��Ȱ��ȭ.
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
        //ó�� ������ ���۵ǰ� �÷��̾ ����� �Ǹ� �÷��̾��� ü�¹ٸ� ��������.
        playerHpBar.fillAmount = player.hp / player.maxHp; //>> ����/���� �� �ϸ� 0�Ǵ� 1�� ��µǴ� ������ �־� float�� ����ȯ ����.
    }
    public void OnPlayerMpSet()
    {
        playerMpBar.fillAmount = player.mp / player.maxMp;
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
