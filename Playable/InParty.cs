using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "InParty", menuName = "InParty")]
public class InParty : ScriptableObject
{
    public List<inPartySlot> inPartySlots = new List<inPartySlot>(); //��Ƽ�� �������� ĳ���͵�.



}


[System.Serializable]
public class inPartySlot
{
    public PlayableC thisCharacter;
    public bool isJoin;
    public bool inSlot;
    public bool isDead;

    public inPartySlot(PlayableC playable)
    {
        this.thisCharacter = playable;
    }
}
