using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardPageManager : MonoBehaviour
{
    //보상 창에서의 각종 오브젝트들, 변수들.
    public PlayableC[] charactersInParty;//전투에 참여한 캐릭터들 
    public GameObject[] rewardCharacter;//전투에 참여한 캐릭터들의 위치
    public GameObject[] spotLightSingle;//캐릭터들에게 빛을 비추는 스포트라이트
    public GameObject[] spotLightDouble;//레벨업시 해당 캐릭터에게 사용해 줄 더블 스포트 라이트
    public RewardDisplay rewardDisplay;//보상창의 보상들을 표시해 줄 스크립트 (순서대로 경험치,골드,아이템)
    public GameObject[] expShow;//경험치를 표시해 줄 오브젝트

    public GameObject[] levelUpEffect;//레벨업시 사용할 이펙트(텍스트)
    public GameObject[] statIncreaseEffect;//스탯이 증가할때 사용할 이펙트(텍스트)
    public GameObject[] skillUnloackEffect;//스킬이 해금될때 사용할 이펙트(텍스트)

    private void Start()
    {
        charactersInParty = new PlayableC[CombatManager.Instance.playerList.Count];
        charactersInParty = CombatManager.Instance.playerList.ToArray();
    }
}
