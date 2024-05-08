using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour //모든 플레이어 스킬에 상속시켜줄 것.
{
    public PlayableC player;
    public PlayableC targetPlayer;
    public CombatSlot targetplayerPlace;
    public TestMob targetMob;
}
