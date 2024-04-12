using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDisplay : MonoBehaviour
{
    public List<PlayableC> playerList;
    public List<CombatSlot> slotList;

    public CombatSlot selectedSlot; //선택된 슬롯. 스킬 사용시 or 아이템 사용시 이 슬롯을 대상으로 함.

    
}
