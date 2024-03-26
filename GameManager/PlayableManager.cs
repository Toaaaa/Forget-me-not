using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableManager : MonoBehaviour
{
    public List<SlotManager> playerSlot; //4���� �÷��̾� ����.
    public List<PlayableC> playableCharacter; //�÷��̾� ĳ���͵�.
    public InParty inParty; //��Ƽ�� �������� ĳ���͵�.

    
    void Update()
    {
        //isjoin �� ���� playerSlot�� ������� ĳ���͸� �־��ִ� �ڵ�.
        //playableCharacter�� ��� ��ũ���ͺ� ������Ʈ��ü�� ���� ���ٰ����ϱ⿡, �� ĳ������ ������ �������ų� ������ �� ����.


        if(Input.GetKeyDown(KeyCode.F1))// �� ĳ���͸� ��Ƽ�� �־��ִ�(inparty ���� isjoin�� true�� ���ִ�) �׽�Ʈ�� �ڵ�.
        {
            if (inParty.inPartySlots[0].isJoin)
            {
                Debug.Log("�̹� ĳ���Ͱ� �����մϴ�.");
                inParty.inPartySlots[0].isJoin = false; //�ӽ÷� ĳ���͸� ���ִ� ��� ����.
            }
            else
            {
                inParty.inPartySlots[0].isJoin = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (inParty.inPartySlots[1].isJoin)
            {
                Debug.Log("�̹� ĳ���Ͱ� �����մϴ�.");
                inParty.inPartySlots[1].isJoin = false; //�ӽ÷� ĳ���͸� ���ִ� ��� ����.
            }
            else
            {
                inParty.inPartySlots[1].isJoin = true;
            }

        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (inParty.inPartySlots[2].isJoin)
            {
                Debug.Log("�̹� ĳ���Ͱ� �����մϴ�.");
                inParty.inPartySlots[2].isJoin = false; //�ӽ÷� ĳ���͸� ���ִ� ��� ����.
            }
            else
            {
                inParty.inPartySlots[2].isJoin = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            if (inParty.inPartySlots[3].isJoin)
            {
                Debug.Log("�̹� ĳ���Ͱ� �����մϴ�.");
                inParty.inPartySlots[3].isJoin = false; //�ӽ÷� ĳ���͸� ���ִ� ��� ����.
            }
            else
            {
                inParty.inPartySlots[3].isJoin = true;
            }
        }
    }
}
