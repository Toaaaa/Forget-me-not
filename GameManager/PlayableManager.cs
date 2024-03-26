using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableManager : MonoBehaviour
{
    public List<SlotManager> playerSlot; //4개의 플레이어 슬롯.
    public List<PlayableC> playableCharacter; //플레이어 캐릭터들.
    public InParty inParty; //파티에 참가중인 캐릭터들.

    
    void Update()
    {
        //isjoin 을 토대로 playerSlot에 순서대로 캐릭터를 넣어주는 코드.
        //playableCharacter의 경우 스크립터블 오브젝트자체의 값에 접근가능하기에, 각 캐릭터의 정보를 가져오거나 수정할 수 있음.


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
}
