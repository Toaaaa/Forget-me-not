using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCombatScene : MonoBehaviour
{
    public CombatManager combatManager;
    private void Start()
    {
        combatManager = CombatManager.Instance;
        combatManager.updateMonster();
    }
}
