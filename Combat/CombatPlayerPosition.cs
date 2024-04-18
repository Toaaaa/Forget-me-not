using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPlayerPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Player.Instance.CombatPositioning();
    }

}
