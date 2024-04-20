using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatSlot : MonoBehaviour
{
    //주로 slot에있는 캐릭터의 sfx나 애니메이션을 제어하는 스크립트.
    //이곳에 play의 정보도 저장되어 있어서 이곳으로 접근하여 정보를 다루는것도 가능.
    public PlayableC player;
    public CombatSelection combatSelection;
    public CombatDisplay combatDisplay;

    private void Update()
    {
        if (player != null)
        {
            this.GetComponent<Image>().sprite = player.characterImage;
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
