using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayableManager : MonoBehaviour
{
    public List<SlotManager> playerSlot; //4개의 플레이어 슬롯.
    public List<PlayableC> playableCharacter; //플레이어 캐릭터를 직접접근 가능한 리스트.
    public List<PlayableC> joinedPlayer; //이거 private으로 하니깐 버그가 발생하네...
    public InParty inParty; //파티에 참가중인 캐릭터들.

    
    void Update()
    {
        prioritySet(); 
        SetSlot();
        //isjoin 을 토대로 joinedPlayer리스트에 넣어주는 코드
        for (int i = 0; i < inParty.inPartySlots.Count; i++)
        {
            inParty.inPartySlots[i].inSlot = inParty.inPartySlots[i].isJoin;
            if (inParty.inPartySlots[i].isJoin && !joinedPlayer.Contains(inParty.inPartySlots[i].thisCharacter)) //이미 파티에 있는 캐릭터는 추가하지 않도록 함.
            {
                joinedPlayer.Add(inParty.inPartySlots[i].thisCharacter);
            }
            else if (!inParty.inPartySlots[i].isJoin && joinedPlayer.Contains(inParty.inPartySlots[i].thisCharacter)) //파티에서 빠진 캐릭터는 리스트에서 제거.
            {
                joinedPlayer.Remove(inParty.inPartySlots[i].thisCharacter);
            }
        }


        



        if(Input.GetKeyDown(KeyCode.F1))// 각 캐릭터를 파티에 넣어주는(inparty 에서 isjoin을 true로 해주는) 테스트용 코드.
        {
            if (inParty.inPartySlots[0].isJoin)
            {
                Debug.Log("이미 캐릭터가 존재합니다.");
                inParty.inPartySlots[0].isJoin = false; //임시로 캐릭터를 빼주는 기능 넣음.
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
                Debug.Log("이미 캐릭터가 존재합니다.");
                inParty.inPartySlots[1].isJoin = false; //임시로 캐릭터를 빼주는 기능 넣음.
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
                Debug.Log("이미 캐릭터가 존재합니다.");
                inParty.inPartySlots[2].isJoin = false; //임시로 캐릭터를 빼주는 기능 넣음.
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
                Debug.Log("이미 캐릭터가 존재합니다.");
                inParty.inPartySlots[3].isJoin = false; //임시로 캐릭터를 빼주는 기능 넣음.
            }
            else
            {
                inParty.inPartySlots[3].isJoin = true;
            }
        }
    }

    void prioritySet() //우선순위에 따라 joinedPlayer 리스트를 정렬해주는 코드.
    {
        List<PlayableC> temp = joinedPlayer.OrderBy(x => x.priority).ToList();
        joinedPlayer = temp;
    }

    void SetSlot() //슬롯에 캐릭터를 넣어주는 코드.
    {
        for(int i = 0; i < joinedPlayer.Count; i++)
        {
            playerSlot[i].currentCharacter = joinedPlayer[i];
        }
        if(joinedPlayer.Count < 4) //파티에 캐릭터가 4명 미만일 경우 나머지 슬롯은 null로 초기화.
        {
            for(int i = joinedPlayer.Count; i < 4; i++)
            {
                playerSlot[i].currentCharacter = null;
            }
        }
    }
}
