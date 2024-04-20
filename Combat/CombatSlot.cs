using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSlot : MonoBehaviour
{
    //�ַ� slot���ִ� ĳ������ sfx�� �ִϸ��̼��� �����ϴ� ��ũ��Ʈ.
    //�̰��� play�� ������ ����Ǿ� �־ �̰����� �����Ͽ� ������ �ٷ�°͵� ����.
    public PlayableC player;
    public CombatSelection combatSelection;
    public CombatDisplay combatDisplay;

    private void Update()
    {
        if (player != null)
        {
            this.GetComponent<Image>().sprite = player.characterImage;
        }
        else
        {
            this.GetComponent<Image>().sprite = null; //���� �ִϸ��̼� ���� �߰��� �Ϸ�Ǹ� >> ����ȭ �ϴ¹������ �����Ұ�.
        }
        // if(player.isdead) >>�׾����� ���� ������ �ִϸ��̼�.

        if (!combatDisplay.duringSceneChange)
        {
            if (combatDisplay.selectedSlot == this) //���õ� ������ ���, ĳ���Ϳ��� �������� ui�� �����.
            {
                combatSelection.gameObject.SetActive(true);
            }
            else
            {
                combatSelection.gameObject.SetActive(false);
            }
        }
        else
        {
            combatSelection.gameObject.SetActive(false);
        }
    }
}
