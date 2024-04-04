using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "monster", menuName = "Monster/normal/testmob1")]
public class TestMob1 : Monster
{
    
    protected override void UseSkill()
    {
        throw new System.NotImplementedException();
    }
    protected override void DropItem()
    {
        throw new System.NotImplementedException();
    }
}
