using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayableManager : MonoBehaviour
{
    public List<SlotManager> playerSlot; //4���� �÷��̾� ����.
    public List<PlayableC> playableCharacter; //�÷��̾� ĳ���͸� �������� ������ ����Ʈ.
    public List<PlayableC> joinedPlayer; //�̰� private���� �ϴϱ� ���װ� �߻��ϳ�...
    public InParty inParty; //��Ƽ�� �������� ĳ���͵�.

    
    void Update()
    {
        prioritySet(); 
        SetSlot();
        //isjoin �� ���� joinedPlayer����Ʈ�� �־��ִ� �ڵ�
        for (int i = 0; i < inParty.inPartySlots.Count; i++)
        {
            inParty.inPartySlots[i].inSlot = inParty.inPartySlots[i].isJoin;
            if (inParty.inPartySlots[i].isJoin && !joinedPlayer.Contains(inParty.inPartySlots[i].thisCharacter)) //�̹� ��Ƽ�� �ִ� ĳ���ʹ� �߰����� �ʵ��� ��.
            {
                joinedPlayer.Add(inParty.inPartySlots[i].thisCharacter);
            }
            else if (!inParty.inPartySlots[i].isJoin && joinedPlayer.Contains(inParty.inPartySlots[i].thisCharacter)) //��Ƽ���� ���� ĳ���ʹ� ����Ʈ���� ����.
            {
                joinedPlayer.Remove(inParty.inPartySlots[i].thisCharacter);
            }
        }


        



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

    void prioritySet() //�켱������ ���� joinedPlayer ����Ʈ�� �������ִ� �ڵ�.
    {
        List<PlayableC> temp = joinedPlayer.OrderBy(x => x.priority).ToList();
        joinedPlayer = temp;
    }

    void SetSlot() //���Կ� ĳ���͸� �־��ִ� �ڵ�.
    {
        for(int i = 0; i < joinedPlayer.Count; i++)
        {
            playerSlot[i].currentCharacter = joinedPlayer[i];
        }
        if(joinedPlayer.Count < 4) //��Ƽ�� ĳ���Ͱ� 4�� �̸��� ��� ������ ������ null�� �ʱ�ȭ.
        {
            for(int i = joinedPlayer.Count; i < 4; i++)
            {
                playerSlot[i].currentCharacter = null;
            }
        }
    }
}
