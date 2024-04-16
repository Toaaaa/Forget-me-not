using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    public List<PlayableC> playerList;
    public List<CombatSlot> slotList;
    public List<CombatStatus> statusUI;

    public CombatSlot selectedSlot; //선택된 슬롯. 스킬 사용시 or 아이템 사용시 이 슬롯을 대상으로 함.

    private void Update()
    {
        if(playerList.Count !=0 && slotList[0].player ==null) //조건을 걸고 onenable을 대신함.
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                slotList[i].player = playerList[i];
                statusUI[i].player = playerList[i];
                statusUI[i].OnPlayerHpSet(); //플레이어의 체력바를 세팅해줌.
                statusUI[i].OnPlayerMpSet(); //플레이어의 마나바를 세팅해줌.
            }
        }
        for(int i = 0; i < statusUI.Count; i++)
        {
            if (statusUI[i].player != null)
            {
                statusUI[i].gameObject.SetActive(true);
            }
        }
    }

    private void OnEnable()
    {
        for(int i=0; i< statusUI.Count; i++)
        {
            statusUI[i].gameObject.SetActive(true);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < slotList.Count; i++)
        {
            slotList[i].player = null;
            statusUI[i].player = null;
        }
        statusUI.ForEach(x => x.playerImage.sprite = null);
    }
}
