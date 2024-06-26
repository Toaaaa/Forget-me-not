using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSlot : MonoBehaviour
{
    //주로 slot에있는 캐릭터의 sfx나 애니메이션을 제어하는 스크립트.
    //이곳에 play의 정보도 저장되어 있어서 이곳으로 접근하여 정보를 다루는것도 가능.

    public GameObject playerPrefab;//실제 필드에 표시될 플레이어 오브젝트
    public PlayableC player;
    public CombatSelection combatSelection;
    public CombatDisplay combatDisplay;
    public bool isActionPlaying; //모션이 진행 중일때 다른 선택을 막기위한 변수. //추후 캐릭터의 애니메이션이 출력될때 true로 세팅 끝나면 false.


    private void Update()
    {
        combatSelection.isSlotActionPlaying = isActionPlaying;

        if (player != null)
        {
            //마법사의 경우, stage1complete 가 되었을 경우, 고양이가 아니라 마법사의 스프라이트로.
            this.GetComponent<Image>().sprite = player.characterImage;
            combatSelection.player = player;
        }
        else
        {
            this.GetComponent<Image>().sprite = null; //이후 애니메이션 등의 추가가 완료되면 >> 투명화 하는방식으로 변경할것.
        }
        // if(player.isdead) >>죽었으면 죽은 상태의 애니메이션.

        if (!combatDisplay.duringSceneChange)
        {
            if (combatDisplay.selectedSlot == this) //선택된 슬롯인 경우, 캐릭터에게 전투선택 ui를 띄워줌.
            {
                combatSelection.gameObject.SetActive(true);
            }
            else if(combatDisplay.skillForAllPlayer)
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
