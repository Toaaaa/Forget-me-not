using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotPlacement : MonoBehaviour
{
    private CombatManager combatManager;
    public GameObject[] P_slotPlace;//�÷��̾ ��ġ�� ��ġ
    public GameObject[] M_slotPlace;//���Ͱ� ��ġ�� ��ġ

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
                combatManager.combatDisplay.slotList[i].playerPrefab = obj; //p0 slot�� playerPrefab�� �ش�Ǵ� �÷��̾��� ������Ʈ�� ��Ͻ�����.
                obj.GetComponent<CharacterPrefab>().thisSlot = combatManager.combatDisplay.slotList[i];
            }
        }
    }
}
