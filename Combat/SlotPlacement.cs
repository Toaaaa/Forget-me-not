using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPlacement : MonoBehaviour
{
    private CombatManager combatManager;
    public GameObject[] P_slotPlace;//플레이어가 배치될 위치
    public GameObject[] M_slotPlace;//몬스터가 배치될 위치

    // Start is called before the first frame update
    void Start()
    {
        combatManager = CombatManager.Instance;
        combatManager.slotPlacement = this;
        PlayerSlotPlace();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerSlotPlace()
    {
        for (int i = 0; i < combatManager.playerList.Count; i++)
        {
            if (combatManager.playerList[i] != null)
            {
                var obj = Instantiate(combatManager.playerList[i].Char_Prefab, P_slotPlace[i].transform.position, Quaternion.identity);
                combatManager.combatDisplay.slotList[i].playerPrefab = obj; //p0 slot의 playerPrefab에 해당되는 플레이어의 오브젝트를 등록시켜줌.
                obj.GetComponent<CharacterPrefab>().thisSlot = combatManager.combatDisplay.slotList[i];
            }
        }
    }
}
