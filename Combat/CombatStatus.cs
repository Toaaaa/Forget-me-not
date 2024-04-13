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
        }
        playerImage.sprite = player.characterImage;

        if (playerHpBar.fillAmount != player.hp / player.maxHp)
        {
            PlayerhpUpdate();
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

    public void OnPlayerHpSet()
    {
        //ó�� ������ ���۵ǰ� �÷��̾ ����� �Ǹ� �÷��̾��� ü�¹ٸ� ��������.
        playerHpBar.fillAmount = player.hp / player.maxHp; //>> ����/���� �� �ϸ� 0�Ǵ� 1�� ��µǴ� ������ �־� float�� ����ȯ ����.
        Debug.Log(player.hp / player.maxHp);
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
}
