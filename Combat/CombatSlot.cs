using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSlot : MonoBehaviour
{
    //�ַ� slot���ִ� ĳ������ sfx�� �ִϸ��̼��� �����ϴ� ��ũ��Ʈ.
    //�̰��� play�� ������ ����Ǿ� �־ �̰����� �����Ͽ� ������ �ٷ�°͵� ����.
    public PlayableC player;

    private void Update()
    {
        if (player != null)
        {
            this.GetComponent<Image>().sprite = player.characterImage;
        }
        // if(player.isdead) >>�׾����� ���� ������ �ִϸ��̼�.
    }
}
